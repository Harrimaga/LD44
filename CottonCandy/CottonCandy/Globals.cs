using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CottonCandy
{
    class Globals
    {
        public const int AMOUNTOFCARDS = 4;
        public static ContentManager contentmanager;
        public static Tilegrid grid;
        public static SpriteBatch sb;
        public static Random rng = new Random();
        public static GameTime time;
        public static int tileW = 160, tileH = 160, screenW, screenH;
        public static List<Card> LastDeck { get; set; } = new List<Card>();
        public static List<Card> Collection { get; set; } = new List<Card>();
        public static GameStates gameState = GameStates.MAINMENU;
        public static SpriteFont font, bigfont;
        public static bool hasMoved = false;
        public static Shop currentShop;
        public static int timer = 0;
        public static int currentLevel;
        public static Point mouse;

        public static Player CurrentPlayer { get; set; }

        public static void TakeTurn()
        {
            hasMoved = false;
            timer = 250;
        }

    }

    public enum GameStates
    {
        PLAYING,
        EDITING,
        GAMEOVER,
        MAINMENU,
        WON,
        DECKBUIDING,
        SHOP,
        LEVELSELECT,
        TURORIAL
    }
}
