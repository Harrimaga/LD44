using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Allies
{
    class Wisp : Ally
    {
        public Wisp() : base(CardBalance.SUMMONWISP_HP, "Enemies/Wisp", 0, "Wisp")
        {

        }

        protected override void Move()
        {

        }
        protected override void Attack()
        {
            if (turnCycle % CardBalance.SUMMONWISP_TURNS == 0)
            {
                for (int i = X - 1; i < X + 2; i++)
                {
                    for (int j = Y - 1; j < Y + 2; j++)
                    {
                        if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                        {
                            if (Globals.grid.GetTile(i, j).unit != null)
                            {
                                Globals.grid.GetTile(i, j).unit.Heal(CardBalance.SUMMONWISP_HEAL);
                            }
                        }
                    }
                }
            }
        }
    }
}
