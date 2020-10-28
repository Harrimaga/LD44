using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Strike : Card
    {
        public Strike() : base(true, "Strike", "Hit an adjecent enemy for " + CardBalance.STRIKE_DAMAGE + " damage", CardBalance.STRIKE_COST, PurchaseMode.ENEMIES, "Cards/EnemyCastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            if(Globals.grid.grid[x, y].unit == null)
            {
                return false;
            }
            float distance = (Globals.CurrentPlayer.GetPosition() - Globals.grid.grid[x, y].unit.GetPosition()).Length();
            if (distance <= 1.5f)
            {
                Globals.grid.grid[x, y].unit.Damage(CardBalance.STRIKE_DAMAGE, "Player");
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
