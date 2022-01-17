using System;
using System.Collections.Generic;
using System.Text;

namespace MapCreationTool.Rendering
{
    class DeltaTime
    {
        DateTime lastTime;
        DateTime currentTime;

        TimeSpan deltaTimeStamp;

        public double Delta
        {
            get
            {
                return deltaTimeStamp.TotalMilliseconds;
            }
        }

        public void Update()
        {
            lastTime = currentTime;

            currentTime = DateTime.Now;

            deltaTimeStamp = currentTime - lastTime;
        }
    }
}
