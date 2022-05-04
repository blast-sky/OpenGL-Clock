using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGClock
{
    sealed class MinuteArrow : Arrow
    {
        public MinuteArrow() :
            base(Matrix4.CreateScale(new Vector3(1, 0.6f, 1)))
        {   }

        public override void setTime(DateTime time)
        {
            _angle = minuteToAngle(time.Minute);
            updateTransform();
        }

        private int minuteToAngle(int minute) => 360 / 60 * minute;
    }
}
