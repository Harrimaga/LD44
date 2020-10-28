using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Warp : Card
    {
        public Warp() : base(false, "Warp", "Warps all enemies to a random position", CardBalance.WARP_COST, PurchaseMode.ALLIES, "Cards/PlatinumSelfCastCard")
        {

        }

        public override bool Play()
        { 
            for (int i = 0; i < Globals.grid.units.Count; i++)
            {
                IUnit enemy = Globals.grid.units[i];
                if (enemy is Enemy e)
                {
                    Vector2 oldPos = e.GetPosition();
                    Globals.grid.RandomWarpUnit((int)oldPos.X, (int)oldPos.Y);
                }
            }

            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

    }
}
