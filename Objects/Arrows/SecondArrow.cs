using OpenTK.Mathematics;
using System;

namespace CGClock
{
    sealed class SecondArrow : Arrow
    {
        public SecondArrow() :
            base(Matrix4.CreateScale(new Vector3(0.3f, 0.7f, 1)), new Color4(0.8f, 0.2f, 0.2f, 0.2f))
        {   }

        public override void setTime(DateTime time)
        {
            _angle = secToAngle(time.Second);
            updateTransform();
        }

        private int secToAngle(int sec) => 360 / 60 * sec;
    }
}
