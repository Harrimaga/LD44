using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Buttons
{
    class StartLevelButton : Button
    {

        public StartLevelButton(string text, int x, int y, int width, int height) : base(text, x, y, width, height)
        {
            doOnlyIn = GameStates.LEVELSELECT;
        }

        public override void Click()
        {
            if (Globals.gameState == doOnlyIn)
            {
                LevelEditor.LoadLevel(text);
                if (Globals.grid.condition <= Globals.CurrentPlayer.CompletedLevels.Count)
                {
                    Globals.CurrentPlayer.Reset(Globals.LastDeck);
                    Globals.gameState = GameStates.PLAYING;
                    Globals.currentLevel = int.Parse(text.Substring(5));
                    CombatLog.Init();
                }
            }
        }

    }
}
