using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Whirlwind : Card
    {
        public Whirlwind() : base(false, "Whirlwind", "Blast a random enemy around you away with the force of wind", CardBalance.WHIRLWIND_COST, PurchaseMode.ENEMIES, "Cards/SelfCastCard")
        {

        }

        public override bool Play()
        {
            List<Enemy> enemies = new List<Enemy>();

            int playerY = Globals.CurrentPlayer.y / Globals.tileH;
            int playerX = Globals.CurrentPlayer.x / Globals.tileW;

            for (int i = playerX - 1; i < playerX + 2; i++)
            {
                for (int j = playerY - 1; j < playerY + 2; j++)
                {
                    if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                    {
                        IUnit unit = Globals.grid.GetTile(i, j).unit;
                        if (unit != null && unit is Enemy)
                        {
                            enemies.Add((Enemy)unit);
                        }
                    }
                }
            }
            if (enemies.Count > 0)
            {
                int random = Globals.rng.Next(0, enemies.Count);

                Vector2 playerPos = new Vector2(playerX * Globals.tileW, playerY * Globals.tileH);
                Vector2 enemyPos = enemies[random].GetPosition();

                Vector2 d = enemyPos - playerPos;
                d.Normalize();

                for (int i = 0; i < CardBalance.WHIRLWIND_BLOWBACK; i++)
                {
                    enemyPos = enemies[random].GetPosition();
                    int x = (int)Math.Ceiling(enemyPos.X + d.X);
                    int y = (int)Math.Ceiling(enemyPos.Y + d.Y);

                    if (x > 0 && Globals.grid.grid[(x), (y)].colidable == TileType.NOTSOLID && Globals.grid.grid[(x), (y)].unit == null)
                    {
                        Globals.grid.grid[(int)(enemyPos.X), (int)(enemyPos.Y)].unit = null;
                        Globals.grid.grid[(x), (y)].unit = enemies[random];
                        enemies[random].SetPosition(enemyPos + new Vector2((d.X > 0) ? (float)Math.Ceiling(d.X) : (float)Math.Floor(d.X), (d.Y > 0) ? (float)Math.Ceiling(d.Y) : (float)Math.Floor(d.Y)) * new Vector2(Globals.tileW, Globals.tileH));
                    }
                }
            }
            return false;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
