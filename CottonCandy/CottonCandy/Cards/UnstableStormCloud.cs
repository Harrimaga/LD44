using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    public class UnstableStormCloud : Card
    {
        public UnstableStormCloud() : base(true, "Unstable Storm Cloud", "Deal " + CardBalance.UNSTABLESTORMCLOUD_DAMAGE + " damage to random enemies in a " + CardBalance.UNSTABLESTORMCLOUD_RANGE + "x" + CardBalance.UNSTABLESTORMCLOUD_RANGE + " square", CardBalance.UNSTABLESTORMCLOUD_COST, PurchaseMode.ENEMIES, "Cards/SilverEnemyAoECastCard")
        {

        }

        public override bool Play()
        {
            return false;
        }

        public override bool Play(int x, int y)
        {
            int range = CardBalance.UNSTABLESTORMCLOUD_RANGE / 2;
            for (int xp = x - range; xp <= x + range; xp++)
            {
                for (int yp = y - range; yp <= y + range; yp++)
                {
                    if (xp > 0 && xp < Globals.grid.w && yp > 0 && yp < Globals.grid.h)
                    {
                        if (Globals.rng.Next(100) > CardBalance.UNSTABLESTORMCLOUD_CHANCE)
                        {
                            Globals.grid.DealDamage(xp, yp, CardBalance.UNSTABLESTORMCLOUD_DAMAGE, "Player");
                        }
                    }
                }
            }
            return true;
        }
    }
}
