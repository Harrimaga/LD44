using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Enemies
{
    [Serializable]
    public class Angel : Enemy
    {
        public Angel() : base(80, "Enemies/Angel", 12, "Angel")
        {

        }

        protected override void Move()
        {
            if (Health < MaxHealth / 4)
            {
                List<Vector2> emptyTiles = Globals.grid.EmptyTiles();
                float furthestDistance = 0;
                Vector2 furthestNode = GetPosition();
                foreach (Vector2 tile in emptyTiles)
                {
                    if ((tile - GetPosition()).Length() > furthestDistance)
                    {
                        furthestDistance = (tile - GetPosition()).Length();
                        furthestNode = tile;
                    }
                }
                Vector2 newPos = Globals.grid.MoveTowards(this, furthestNode, true);
                Globals.grid.AddUnitAtPosition(this, (int)newPos.X, (int)newPos.Y);
            }
            else
            {
                Vector2 newPos = Globals.grid.MoveTowards(this, Globals.CurrentPlayer.GetPosition(), true);
                Globals.grid.AddUnitAtPosition(this, (int)newPos.X, (int)newPos.Y);
            }
        }

        protected override void Attack()
        {
            int adjturn = turnCycle % 6;
            switch (adjturn)
            {
                case 0:
                    StunAttack();
                    break;
                case 2:
                    AOEAttack();
                    break;
                case 5:
                    EpicSwordSlash();
                    break;
            }
        }

        private void StunAttack()
        {
            if (PlayerInRange(5))
            {
                Globals.CurrentPlayer.Stun(1);
            }
        }

        private void AOEAttack()
        {
            for (int i = X - 2; i < X + 3; i++)
            {
                for (int j = Y - 2; j < Y + 3; j++)
                {
                    if (i < Globals.grid.w && i > 0 && j < Globals.grid.h && j > 0)
                    {
                        if (i != X || j != Y)
                        {
                            if (Globals.grid.GetTile(i, j).unit != null && !(Globals.grid.GetTile(i, j).unit is Enemy))
                            {
                                Globals.grid.DealDamage(i, j, 5, name);
                            }
                        }
                    }
                }
            }
        }

        private void EpicSwordSlash()
        {
            if (PlayerInRange(1))
            {
                Globals.CurrentPlayer.Damage(11, name);
            }
            else
            {
                List<IUnit> units = new List<IUnit>();
                for (int i = X - 1; i < X + 2; i++)
                {
                    for (int j = Y - 1; j < Y + 2; j++)
                    {
                        if (i <= Globals.grid.w && i > 0 && j <= Globals.grid.h && j > 0)
                        {
                            IUnit unit = Globals.grid.GetTile(i, j).unit;
                            if (unit != null && !(unit is Enemy))
                            {
                                units.Add(unit);
                            }
                        }
                    }
                }
                if (units.Count < 1)
                {
                    Heal(4);
                }
                else
                {
                    int damage = 11;
                    int random = Globals.rng.Next(0, units.Count);
                    units[random].Damage(damage, name);
                }
            }
        }

        private bool PlayerInRange(int radius)
        {
            return X > Globals.CurrentPlayer.X - (radius + 1) && X < Globals.CurrentPlayer.X + (radius + 1) && Y > Globals.CurrentPlayer.Y - (radius + 1) && Y < Globals.CurrentPlayer.Y + (radius + 1);
        }
    }
}
