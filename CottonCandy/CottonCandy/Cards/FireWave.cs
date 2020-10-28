using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class FireWave : Card
    {
        public FireWave() : base(true, "Fire Wave", "Deals between " + CardBalance.FIREWAVE_LOWDAMAGE + " and " + CardBalance.FIREWAVE_HIGHDAMAGE + " damage to each unit in a " + CardBalance.FIREWAVE_RANGE + " wide diamond", CardBalance.FIREWAVE_COST, PurchaseMode.ENEMIES, "Cards/AoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int diamondwidth = CardBalance.FIREWAVE_RANGE / 2;
            int row = 0;
            for (int i = diamondwidth; i >= 0; i--)
            {
                for (int j = -i; j <= i; j++)
                {
                    if (x + j > 0 && x + j < Globals.grid.w && y - row > 0 && Globals.grid.GetTile(x + j, y - row).unit != null)
                    {
                        Globals.grid.DealDamage(x + j, y - row, Globals.rng.Next(CardBalance.FIREWAVE_LOWDAMAGE, CardBalance.FIREWAVE_HIGHDAMAGE + 1), "Player");
                    }
                }
                row++;
            }
            row = 1;
            for (int i = diamondwidth - 1; i >= 0; i--)
            {
                for (int j = -i; j <= i; j++)
                {
                    if (x + j > 0 && x + j < Globals.grid.w && y + row < Globals.grid.h && Globals.grid.GetTile(x + j, y + row).unit != null)
                    {
                        Globals.grid.DealDamage(x + j, y + row, Globals.rng.Next(CardBalance.FIREWAVE_LOWDAMAGE, CardBalance.FIREWAVE_HIGHDAMAGE + 1), "Player");
                    }
                }
                row++;
            }
            return true;
        }

    }
}
