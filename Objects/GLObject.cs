using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace CGClock.Util
{
    abstract class GLObject : IDrawable, ILoadable
    {
        protected List<float> _points = new List<float>();
        protected Matrix4 _model = Matrix4.Identity;
        protected Color4 _objectColor = Color4.White;

        private string _vertexShaderSource = @"
            #version 330

            layout(location = 0) in vec3 position;
            layout(location = 1) in vec3 normal;

            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;
            
            out vec3 fNormal;
            out vec3 fragPos;

            void main(void)
            {
                gl_Position = vec4(position, 1.0) * model * view * projection;
                fragPos = vec3(vec4(position, 1.0) * model);
                fNormal = normal * mat3(transpose(inverse(model)));
            }
        ";

        private string _fragmentShaderSource = @"
            #version 330

            uniform vec3 lightDir;
            uniform vec3 viewPos;
            uniform vec3 objectColor;

            in vec3 fNormal;
            in vec3 fragPos;

            out vec4 outputColor;

            void main(void)
            {
                vec3 norm = normalize(fNormal);
                vec3 normLight = normalize(lightDir);

                vec3 lightColor = vec3(1,1,1); // TODO

                float ambientStrength = 0.2;
                vec3 ambient = ambientStrength * lightColor;

                float diff = max(dot(-norm, normLight), 0.0);
                vec3 diffuse = diff * lightColor;      

                float specularStrength = 0.1;
                vec3 viewDir = normalize(viewPos - fragPos);
                vec3 reflectDir = reflect(normLight, -norm);
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), 48);
                vec3 specular = specularStrength * spec * lightColor;

                vec3 result = (ambient + diffuse + specular) * objectColor;
                outputColor = vec4(result, 1.0);
            }
        ";

        private bool _hasNormals;
        private PrimitiveType _primitiveType;

        private int _bufferObject;
        private int _arrayObject;

        private int _vertexShader;
        private int _fragmentShader;
        private int _shaderProgram;

        protected GLObject(bool hasNormals, PrimitiveType primitiveType)
        {
            _hasNormals = hasNormals;
            _primitiveType = primitiveType;
        }

        protected GLObject(string vertexShader, string fragmentShader, bool hasNormals, PrimitiveType primitiveType) :
            this(hasNormals, primitiveType)
        {
            _vertexShaderSource = vertexShader;
            _fragmentShaderSource = fragmentShader;
        }

        virtual public void load()
        {
            GL.EnableVertexAttribArray(0);
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, _vertexShaderSource);
            GL.CompileShader(_vertexShader);

            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, _fragmentShaderSource);
            GL.CompileShader(_fragmentShader);

            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, _vertexShader);
            GL.AttachShader(_shaderProgram, _fragmentShader);
            GL.LinkProgram(_shaderProgram);

            _bufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _points.Count * sizeof(float), _points.ToArray(), BufferUsageHint.StaticDraw);

            _arrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_arrayObject);

            var size = _hasNormals ? 6 * sizeof(float) : 3 * sizeof(float);

            var positionLocation = GL.GetAttribLocation(_shaderProgram, "position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, size, 0);

            if (_hasNormals)
            {
                var normalLocation = GL.GetAttribLocation(_shaderProgram, "normal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, size, 3 * sizeof(float));
            }
        }

        virtual public void unload()
        {
            GL.DeleteBuffer(_bufferObject);
            GL.DeleteVertexArray(_arrayObject);
        }

        virtual public void draw(Matrix4 view, Matrix4 projection, Vector3 viewPos)
        {
            GL.UseProgram(_shaderProgram);

            setMatrix4("model", _model);
            setMatrix4("view", view);
            setMatrix4("projection", projection);

            setVec3("viewPos", viewPos);
            setVec3("lightDir", new Vector3(1, -1, -1));

            setColor("objectColor", _objectColor);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _bufferObject);
            GL.BindVertexArray(_arrayObject);

            var size = _hasNormals ? 6 : 3;
            GL.DrawArrays(_primitiveType, 0, _points.Count / size);
        }

        private void setMatrix4(string name, Matrix4 matrix) =>
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, name), true, ref matrix);

        private void setVec3(string name, Vector3 vec) =>
            GL.Uniform3(GL.GetUniformLocation(_shaderProgram, name), vec.X, vec.Y, vec.Z);

        private void setColor(string name, Color4 col) =>
            GL.Uniform3(GL.GetUniformLocation(_shaderProgram, name), col.R, col.G, col.B);
    }
}
