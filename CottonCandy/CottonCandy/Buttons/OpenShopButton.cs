using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Buttons
{
    class OpenShopButton : Button
    {

        public OpenShopButton(string text, int x, int y, int width, int height, GameStates doOnlyIn) : base(text, x, y, width, height)
        {
            this.doOnlyIn = doOnlyIn;
        }

        public override void Click()
        {
            if (Globals.gameState == doOnlyIn)
            {
                Globals.gameState = GameStates.SHOP;
                Globals.currentShop = new Shop(5);
            }
        }


    }
}
