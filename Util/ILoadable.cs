using System;
using System.Collections.Generic;
using System.Text;

namespace CGClock.Util
{
    interface ILoadable
    {
        abstract void load();

        abstract void unload();
    }
}
