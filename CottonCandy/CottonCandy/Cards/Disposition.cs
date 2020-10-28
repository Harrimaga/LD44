using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Disposition : Card
    {
        public Disposition() : base(true, "Disposition", "Teleports a targeted enemy to a random position", CardBalance.DISPOSITION_COST, PurchaseMode.ENEMIES, "Cards/SilverEnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if (Globals.grid.grid[x, y].unit != null)
            {
                Vector2 oldPos = Globals.grid.grid[x, y].unit.GetPosition();
                Globals.grid.RandomWarpUnit((int)oldPos.X, (int)oldPos.Y);
                return true;
            }
            return false;            
        }

    }
}
