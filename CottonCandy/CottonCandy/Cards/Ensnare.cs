using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Ensnare : Card
    {
        public Ensnare() : base(true, "Ensnare", "Stun all units in a "+CardBalance.ENSNARE_RANGE+" tile wide diamond for "+CardBalance.ENSNARE_STUN+" turn", CardBalance.ENSNARE_COST, PurchaseMode.ENEMIES, "Cards/SilverEnemyAoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int diamondwidth = CardBalance.ENSNARE_RANGE / 2;
            int row = 0;
            for (int i = diamondwidth; i >= 0; i--)
            {
                for (int j = -i; j <= i; j++)
                {
                    if (x + j > 0 && x + j < Globals.grid.w && y - row > 0 && Globals.grid.GetTile(x + j, y - row).unit != null)
                    {
                        Globals.grid.GetTile(x + j, y - row).unit.Stun(CardBalance.ENSNARE_STUN);
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
                        Globals.grid.GetTile(x + j, y + row).unit.Stun(CardBalance.ENSNARE_STUN);
                    }
                }
                row++;
            }
            return true;
        }

    }
}
