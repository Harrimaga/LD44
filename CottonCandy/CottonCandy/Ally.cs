using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    public class Ally : IUnit
    {
        protected int turnCycle;
        private int x;
        private int y;
        protected int Stunned { get; set; }
        public int Health { get; set; }
        public string GraphicName { get; set; }
        public int Value { get; set; }
        protected int X { get { return x / Globals.tileW; } set { x = value * Globals.tileW; } }
        protected int Y { get { return y / Globals.tileH; } set { y = value * Globals.tileH; } }
        public string name;

        protected Ally(int health, string graphicName, int value, string name)
        {
            Health = health;
            this.GraphicName = graphicName;
            turnCycle = 0;
            Value = value;
            this.name = name;
        }

        public void ChangePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Damage(int amount, string unitName)
        {
            Health -= amount;
            if (Health < 1)
            {
                Globals.grid.RemoveUnit(this, x / Globals.tileW, y / Globals.tileH);
                Globals.CurrentPlayer.SacrificeCurrency++;
            }
        }

        public void Draw()
        {
            Globals.sb.Draw(LoadTex.LoadTexture(GraphicName), new Vector2(x - Camera.camX, y - Camera.camY), Color.White);
        }

        public string GetTextureName()
        {
            return GraphicName;
        }

        public void TakeTurn()
        {
            if (Stunned > 0)
            {
                Stunned--;
            }
            else
            {
                Move();
                Attack();
                turnCycle++;
                Globals.grid.KillUnits();
            }
        }

        protected virtual void Move()
        {
            switch (Globals.rng.Next(0, 4))
            {
                case 0:
                    if (x > 0 && Globals.grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].colidable == TileType.NOTSOLID && Globals.grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].unit == null)
                    {
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = null;
                        x -= Globals.tileW;
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = this;
                    }
                    break;
                case 1:
                    if (y > 0 && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].colidable == TileType.NOTSOLID && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].unit == null)
                    {
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = null;
                        y -= Globals.tileH;
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = this;
                    }
                    break;
                case 2:
                    if (x < Globals.grid.w * Globals.tileW - Globals.tileW && Globals.grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].colidable == TileType.NOTSOLID && Globals.grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].unit == null)
                    {
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = null;
                        x += Globals.tileW;
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = this;
                    }
                    break;
                case 3:
                    if (y < Globals.grid.h * Globals.tileH - Globals.tileH && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].colidable == TileType.NOTSOLID && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].unit == null)
                    {
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = null;
                        y += Globals.tileH;
                        Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH)].unit = this;
                    }
                    break;
                default:
                    throw new IndexOutOfRangeException("random value is not between 0 and 3!");
            }
        }

        protected virtual void Attack()
        {
            Tilegrid grid = Globals.grid;
            if (x > 0 && grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].unit != null)
            {
                if (grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (y > 0 && grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].unit != null)
            {
                if (grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (x < grid.w * Globals.tileW - Globals.tileW && grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].unit != null)
            {
                if (grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (y < grid.h * Globals.tileH - Globals.tileH && grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].unit != null)
            {
                if (grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            Globals.grid = grid;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Stun(int time)
        {
            Stunned += time;
        }

        public void Heal(int amount)
        {
            Health += amount;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(X, Y);
        }

        public bool IsDead()
        {
            return Health < 1;
        }
    }
}
