using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Allies
{
    public class LesserCreature : Ally
    {
        public LesserCreature() : base(CardBalance.SUMMONLESSERCREATURE_HP, "Enemies/LesserCreature", 0, "Lesser Creature")
        {

        }

        protected override void Attack()
        {
            List<Enemy> enemies = new List<Enemy>();
            for (int i = X - 1; i < X + 2; i++)
            {
                for (int j = Y - 1; j < Y + 2; j++)
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
            if (enemies.Count < 1)
            {
                Move();
            }
            else
            {
                int random = Globals.rng.Next(0, enemies.Count);
                enemies[random].Damage(1, name);
            }

        }
    }
}
