using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    class CombatLog
    {

        public static string[] lines { get; set; }
        public const int lineNums = 15;

        public static void Init()
        {
            lines = new string[lineNums];
            for (int i = 0; i < lineNums; i++)
            {
                lines[i] = null;
            }
        }

        public static void AddLine(string line)
        {
            for (int i = lineNums - 1; i > 0; i--)
            {
                lines[i] = lines[i - 1];
            }
            lines[0] = line;
        }

        public static void Draw()
        {
            for (int i = 0; i < lineNums; i++)
            {
                if (lines[i] != null)
                {
                    if (i == 0)
                    {
                        Globals.sb.DrawString(Globals.font, "(now) " + lines[i], new Vector2(Globals.screenW - Globals.tileW * 2, 240 + 20 * i), Color.White);
                    }
                    else
                    {
                        Globals.sb.DrawString(Globals.font, lines[i], new Vector2(Globals.screenW - Globals.tileW * 2, 240 + 20 * i), Color.White);
                    }
                }
            }
        }

    }
}
