using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CottonCandy.Buttons;

namespace CottonCandy
{
    class LevelSelect
    {

        private static List<Button> buttons = new List<Button>();

        public static void Init()
        {
            bool exist = true;
            int at = 1;
            while(exist)
            {
                exist = FileLoader.FileExists("Levels/Level" + at + ".dat");
                if(exist)
                {
                    buttons.Add(new StartLevelButton("Level" + at, Globals.screenW / 2 - 100, 5 + 65 * at, 200, 65));
                }
                at++;
            }
        }

        public static void OnClick()
        {
            foreach (Button b in buttons)
            {
                if (b.MouseOver())
                {
                    b.Click();
                }
            }
        }

        public static void Draw()
        {
            foreach(Button b in buttons)
            {
                b.Draw();
            }
        }

    }
}
