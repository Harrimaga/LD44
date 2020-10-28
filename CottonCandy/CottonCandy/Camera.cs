using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    class Camera
    {

        public static int camX, camY;
        private static int camSpeed = 2;

        public static void moveCamR()
        {
            camX += camSpeed * Globals.time.ElapsedGameTime.Milliseconds;
            if(camX > Globals.grid.w*Globals.tileW - Globals.screenW + Globals.tileW*2)
            {
                camX = Globals.grid.w * Globals.tileW - Globals.screenW + Globals.tileW * 2;
            }
            if(camX < 0)
            {
                camX = 0;
            }
        }

        public static void moveCamD()
        {
            camY += camSpeed * Globals.time.ElapsedGameTime.Milliseconds;
            if (camY > Globals.grid.h * Globals.tileH - Globals.screenH)
            {
                camY = Globals.grid.h * Globals.tileH - Globals.screenH;
            }
            if (camY < 0)
            {
                camY = 0;
            }
        }

        public static void moveCamL()
        {
            camX -= camSpeed * Globals.time.ElapsedGameTime.Milliseconds;
            if (camX < 0)
            {
                camX = 0;
            }
        }

        public static void moveCamU()
        {
            camY -= camSpeed * Globals.time.ElapsedGameTime.Milliseconds;
            if (camY < 0)
            {
                camY = 0;
            }
        }

    }
}
