using CGClock.Util;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace CGClock
{
    abstract class Arrow: GLObject
    {
        protected int _angle = 0;
        protected Matrix4 _scale;

        private float[] _arrowPoints = new float[] {
            -xOffset, yEnd, zBegin, 0, 0, -1,
            xOffset, yEnd, zBegin, 0, 0, -1,
            -xOffset, yBegin, zBegin, 0, 0, -1,
            -xOffset, yBegin, zBegin, 0, 0, -1,
            xOffset, yBegin, zBegin, 0, 0, -1,
            xOffset, yEnd, zBegin, 0, 0, -1,

            -xOffset, yBegin, zEnd, 0, 0, 1,
            xOffset, yBegin, zEnd, 0, 0, 1,
            -xOffset, yEnd, zEnd, 0, 0, 1,
            -xOffset, yEnd, zEnd, 0, 0, 1,
            xOffset, yEnd, zEnd, 0, 0, 1,
            xOffset, yBegin, zEnd, 0, 0, 1,

            xOffset, yEnd, zBegin, 1, 0, 0,
            xOffset, yEnd, zEnd, 1, 0, 0,
            xOffset, yBegin, zBegin, 1, 0, 0,
            xOffset, yBegin, zBegin, 1, 0, 0,
            xOffset, yBegin, zEnd, 1, 0, 0,
            xOffset, yEnd, zEnd, 1, 0, 0,

            -xOffset, yBegin, zBegin, -1, 0, 0,
            -xOffset, yBegin, zEnd, -1, 0, 0,
            -xOffset, yEnd, zBegin, -1, 0, 0,
            -xOffset, yEnd, zBegin, -1, 0, 0,
            -xOffset, yEnd, zEnd, -1, 0, 0,
            -xOffset, yBegin, zEnd, -1, 0, 0,

            -xOffset, yBegin, zBegin, 0, -1, 0,
            -xOffset, yBegin, zEnd, 0, -1, 0,
            xOffset, yBegin, zEnd, 0, -1, 0,
            xOffset, yBegin, zEnd, 0, -1, 0,
            xOffset, yBegin, zBegin, 0, -1, 0,
            -xOffset, yBegin, zBegin, 0, -1, 0,

            -xOffset, yEnd, zBegin, 0, 1, 0,
            -xOffset, yEnd, zEnd, 0, 1, 0,
            xOffset, yEnd, zEnd, 0, 1, 0,
            xOffset, yEnd, zEnd, 0, 1, 0,
            xOffset, yEnd, zBegin, 0, 1, 0,
            -xOffset, yEnd, zBegin, 0, 1, 0,
        };

        private const float xOffset = 0.04f;
        private const float zBegin = 0f;
        private const float zEnd = 0.1f;
        private const float yBegin = 0f;
        private const float yEnd = 1f;

        private Matrix4 _transform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.CreateTranslation(new Vector3(0, 0.1f, 0));

        protected Arrow(Matrix4 scale, Color4 color) :
            base(true, OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles)
        {
            _objectColor = color;
            _scale = scale;
            _points = new List<float>(_arrowPoints);
        }

        protected Arrow(Matrix4 scale) :
            this(scale, new Color4(0.15f, 0.35f, 0.8f, 1f))
        {   }

        protected void updateTransform()
        {
            _transform = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_angle * -1));
            _model = _scale * _translation * _transform;
        }

        public abstract void setTime(DateTime time);
    }
}
