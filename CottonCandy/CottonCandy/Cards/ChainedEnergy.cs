using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class ChainedEnergy : Card
    {
        public ChainedEnergy() : base(true, "Chained Energy", "Deals " + CardBalance.CHAINEDENERGY_DAMAGE + " damage to target enemy, it than has a " + CardBalance.CHAINEDENERGY_CHANCE + "% chance to bounce to anothere enemy within a " + CardBalance.CHAINEDENERGY_RANGE + "x" + CardBalance.CHAINEDENERGY_RANGE + " square and trigger again", CardBalance.CHAINEDENERGY_COST, PurchaseMode.HP, "Cards/GoldEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.CHAINEDENERGY_RANGE / 2;
            Globals.grid.DealDamage(x, y, 5, "Player");
            if (Globals.rng.Next(100) < CardBalance.CHAINEDENERGY_CHANCE)
            {
                List<Enemy> us = new List<Enemy>();
                for (int i = x - range; i <= x + range; i++)
                {
                    for (int j = y - range; j <= y + range; j++)
                    {
                        if (i >= Globals.grid.w || i < 0 || j >= Globals.grid.h || j < 0 || (i == x && i == y)) { continue; }
                        
                        if (Globals.grid.grid[i, j].unit != null && Globals.grid.grid[i, j].unit is Enemy)
                        {
                            us.Add((Enemy)Globals.grid.grid[i, j].unit);
                        }
                    }
                }
                Enemy u = us[Globals.rng.Next(us.Count())];
                Play((int)u.GetPosition().X, (int)u.GetPosition().Y);
            }
            return true;
        }

    }
}
