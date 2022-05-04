using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using System.Collections.Generic;
using CGClock.Util;
using System;
using OpenTK.Mathematics;
using CGClock.Objects;

namespace CGClock
{
    sealed class ClockWindow : GameWindow
    {
        private List<ILoadable> _loadables = new List<ILoadable>();
        private List<IDrawable> _drawables = new List<IDrawable>();

        private List<Arrow> _arrows = new List<Arrow>();

        private Camera _camera;
        private Vector3 _target = new Vector3(0, 0, 0);
        private const float _walkRadius = 0.8f;

        private Color _backColor = Color.Gray;

        private int _previousSecondCount = -1;
        private double _time = 0d;

        public ClockWindow(GameWindowSettings game, NativeWindowSettings native) :
            base(game, native)
        {
            var circle = new ClockCircle();

            _loadables.Add(circle);
            _drawables.Add(circle);

            _arrows.Add(new SecondArrow());
            _arrows.Add(new MinuteArrow());
            _arrows.Add(new HourArrow());

            _arrows.ForEach(arrow =>
            {
                _loadables.Add(arrow);
                _drawables.Add(arrow);
            });

/*            for(int i = 0; i < 60; i++)
            {
                var date = new DateTime(1, 1, 1, 1, i, 1);
                var sep = new Separator();
                sep.setTime(date);
                _loadables.Add(sep);
                _drawables.Add(sep);
            }*/
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(_backColor);
            GL.Enable(EnableCap.DepthTest);
            _camera = new Camera(Vector3.UnitZ * _walkRadius, Size.X / (float)Size.Y);
            _loadables.ForEach(loadable => loadable.load());
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            _loadables.ForEach(loadable => loadable.unload());

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs args)
        {
            base.OnResize(args);
            GL.Viewport(0, 0, args.Size.X, args.Size.Y);
            _camera.AspectRatio = (float)args.Size.X / args.Size.Y;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (_previousSecondCount != System.DateTime.Now.Second)
            {
                _arrows.ForEach(arrow => arrow.setTime(System.DateTime.Now));
                _previousSecondCount = System.DateTime.Now.Second;
            }

            _drawables.ForEach(drawable => drawable.draw(_camera.GetViewMatrix() * Matrix4.LookAt(_camera.Position, _target, _camera.Up), _camera.GetProjectionMatrix(), _camera.Position));

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _time += args.Time / 10d;

            var pos = new Vector3((float)Math.Sin(_time) * _walkRadius, 0, (float)Math.Cos(_time) * _walkRadius);

            _camera.Position = pos;
        }
    }
}
