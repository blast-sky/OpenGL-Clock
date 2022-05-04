using System;
using System.Collections.Generic;
using CGClock.Util;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CGClock
{
    class ClockCircle : GLObject
    {
        private const float zBegin = 0f;
        private const float zEnd = 0.1f;
        private const float outerCircleRadius = 1f;
        private const float innerCircleRadius = 0.93f;

        private float[] normalOnMe = new float[] { 0, 0, -1 };
        private float[] normalFromMe = new float[] { 0, 0, 1 };

        public ClockCircle() :
            base(true, PrimitiveType.Triangles)
        {
            for (int i = 0; i <= 360; i++)
            {
                var heading = MathHelper.DegreesToRadians(i);
                var nextHeading = MathHelper.DegreesToRadians(i + 1);

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                _points.Add((float)(Math.Cos(nextHeading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * outerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                _points.Add((float)(Math.Cos(heading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * innerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zBegin);
                _points.AddRange(normalOnMe);

                ////

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                _points.Add((float)(Math.Cos(nextHeading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * outerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                _points.Add((float)(Math.Cos(heading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * innerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zEnd);
                _points.AddRange(normalFromMe);

                ////

                _points.Add((float)(Math.Cos(heading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * innerCircleRadius));
                _points.Add(zBegin);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zBegin);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zEnd);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                _points.Add((float)(Math.Cos(nextHeading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * innerCircleRadius));
                _points.Add(zEnd);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                _points.Add((float)(Math.Cos(heading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * innerCircleRadius));
                _points.Add(zEnd);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                _points.Add((float)(Math.Cos(heading) * innerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * innerCircleRadius));
                _points.Add(zBegin);

                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);
                _points.Add(0 - _points[_points.Count - 3]);

                ////

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zBegin);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                _points.Add((float)(Math.Cos(nextHeading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * outerCircleRadius));
                _points.Add(zBegin);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                _points.Add((float)(Math.Cos(nextHeading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * outerCircleRadius));
                _points.Add(zEnd);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                _points.Add((float)(Math.Cos(nextHeading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(nextHeading) * outerCircleRadius));
                _points.Add(zEnd);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zEnd);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                _points.Add((float)(Math.Cos(heading) * outerCircleRadius));
                _points.Add((float)(Math.Sin(heading) * outerCircleRadius));
                _points.Add(zBegin);

                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);
                _points.Add(_points[_points.Count - 3] - 0);

                ////
            }
        }
    }
}
