﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Buttons
{
    class OutDeckBuilderButton : Button
    {

        public OutDeckBuilderButton(string text, int x, int y, int width, int height) : base(text, x, y, width, height)
        {
            doOnlyIn = GameStates.DECKBUIDING;
        }

        public override void Click()
        {
            if (Globals.gameState == doOnlyIn)
            {
                Globals.gameState = GameStates.MAINMENU;
            }
        }

    }
}
