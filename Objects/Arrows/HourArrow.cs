using OpenTK.Mathematics;
using System;

namespace CGClock
{
    sealed class HourArrow : Arrow
    {
        public HourArrow() :
            base(Matrix4.CreateScale(new Vector3(1, 0.4f, 1)))
        {   }

        public override void setTime(DateTime time)
        {
            _angle = hourToAngle(time.Hour);
            updateTransform();
        }

        private int hourToAngle(int hour) => 360 / 12 * (hour % 12);
    }
}
