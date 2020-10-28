using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CottonCandy.Buttons
{
    class DiscardButton : Button
    {

        public DiscardButton(string text, int x, int y, int width, int height) : base(text, x, y, width, height)
        {
            doOnlyIn = GameStates.PLAYING;
        }

        public override void Click()
        {
            if (Globals.gameState == doOnlyIn)
            {
                Globals.CurrentPlayer.PutCardBack();
                Globals.TakeTurn();
            }
        }

    }
}
