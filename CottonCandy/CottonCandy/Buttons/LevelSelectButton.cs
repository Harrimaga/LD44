using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Buttons
{
    class LevelSelectButton : Button
    {
        public LevelSelectButton(int x, int y, int width, int height, string graphicName) : base(x, y, width, height, graphicName)
        {

        }

        public override void Click()
        {
            if (Globals.gameState == doOnlyIn)
            {
                Globals.gameState = GameStates.LEVELSELECT;
                LevelSelect.Init();
            }
        }
    }
}
