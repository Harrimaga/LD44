using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Allies
{

    public class Demon : Ally
    {

        public Demon() : base(CardBalance.SUMMONDEMON_HP, "Enemies/Demon", 0, "Demon")
        {

        }

        protected override void Attack()
        {
            if (turnCycle % 3 == 0)
            {
                for (int i = X - 2; i < X + 3; i++)
                {
                    for (int j = Y - 2; j < Y + 3; j++)
                    {
                        if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                        {
                            if (i != X || j != Y)
                            {
                                Globals.grid.DealDamage(i, j, CardBalance.SUMMONDEMON_DAMAGE, name);
                            }
                        }
                    }
                }
            }
        }

        protected override void Move()
        {
            float shortestDistance = float.MaxValue;
            IUnit closestUnit = null;
            List<IUnit> enemies = new List<IUnit>();
            foreach (var unit in Globals.grid.units)
            {
                if (unit is Enemy)
                {
                    enemies.Add(unit);
                }
            }
            foreach (var ally in enemies)
            {
                if ((ally.GetPosition() - GetPosition()).Length() < shortestDistance)
                {
                    closestUnit = ally;
                    shortestDistance = (ally.GetPosition() - GetPosition()).Length();
                }
            }
            Vector2 newPos = Globals.grid.MoveTowards(this, closestUnit.GetPosition(), false);
            Globals.grid.AddUnitAtPosition(this, (int)newPos.X, (int)newPos.Y);
        }
    }
}
