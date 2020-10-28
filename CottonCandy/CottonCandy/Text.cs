using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    public class Text
    {
        public static string BreakUpString(SpriteFont font, string text, float maxwidth)
        {
            string[] words = text.Split(' ');
            string[] lines = { "","","","","","","","","",""};
            int linenr = 0;
            string result = "";
            foreach(string word in words)
            {
                if(font.MeasureString(result+ " " + word).X>maxwidth)
                {
                    lines[linenr++] = result+"\n";
                    result = word;
                }
                else
                {
                    result += " " + word;
                }
            }
            lines[linenr] = result;

            result = "";
            foreach(string line in lines)
            {
                result += line;
            }
            return result;
        }
    }
}
