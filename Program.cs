using OpenTK.Windowing.Desktop;
using System;

namespace CGClock
{
    class Program
    { 
        [STAThread]
        static void Main()
        {
            var gameSettings = new GameWindowSettings()
            {
                RenderFrequency = 10,
                UpdateFrequency = 10
            };

            var nativeSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(600, 600),
                Title = "CGClock",
                StencilBits = 32
            };

            new ClockWindow(gameSettings, nativeSettings).Run();
        }
    }
}