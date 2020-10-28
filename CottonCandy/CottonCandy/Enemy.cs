using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace CottonCandy
{
    [Serializable]
    public abstract class Enemy : IUnit
    {
        protected int turnCycle;
        private int x;
        private int y;
        protected int Stunned { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public string GraphicName { get; set; }
        public int Value { get; set; }
        protected bool confused;
        protected int X { get { return x / Globals.tileW; } set { x = value * Globals.tileW; } }
        protected int Y { get { return y / Globals.tileH; } set { y = value * Globals.tileH; } }
        public bool damaged;
        public string name;

        protected Enemy(int health, string graphicName, int value, string name)
        {
            Health = health;
            MaxHealth = health;
            this.GraphicName = graphicName;
            turnCycle = 0;
            Value = value;
            damaged = false;
            this.name = name;
        }

        [Obsolete]
        public Vector2 Position()
        {
            return new Vector2(x, y);
        }

        public void SetPosition(Vector2 position)
        {
            x = (int)position.X;
            y = (int)position.Y;
        }

        protected void BaseMove()
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

        public virtual void TakeTurn()
        {
            if (Stunned > 0)
            {
                Stunned--;
            }
            else
            {
                turnCycle++;
                if (confused)
                {
                    confused = false;
                    BaseMove();
                }
                else
                {
                    Move();
                }
                Attack();
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
            if (x > 0 && Globals.grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].unit != null)
            {
                if (Globals.grid.grid[(x / Globals.tileW) - 1, (y / Globals.tileH)].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (y > 0 && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].unit != null)
            {
                if (Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) - 1].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (x < Globals.grid.w * Globals.tileW - Globals.tileW && Globals.grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].unit != null)
            {
                if (Globals.grid.grid[(x / Globals.tileW) + 1, (y / Globals.tileH)].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
            if (y < Globals.grid.h * Globals.tileH - Globals.tileH && Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].unit != null)
            {
                if (Globals.grid.grid[(x / Globals.tileW), (y / Globals.tileH) + 1].unit == Globals.CurrentPlayer)
                {
                    Globals.CurrentPlayer.Damage(1, name);
                }
            }
        }

        public virtual void Damage(int amount, string unitName)
        {
            Health -= amount;
            CombatLog.AddLine(unitName + " dealt " + amount + " damage to " + name);
            if (Health < 1)
            {
                CombatLog.AddLine(name + " died!");
                Globals.grid.killedUnits.Add(this);
                Globals.CurrentPlayer.DeathCurrency += Value;
            }
            damaged = true;
        }

        public void Update()
        {
            
        }

        public void Draw()
        {
            if(damaged)
            {
                Globals.sb.Draw(LoadTex.LoadTexture(GraphicName), new Vector2(x - Camera.camX, y - Camera.camY), Color.Red);
            }
            else
            {
                Globals.sb.Draw(LoadTex.LoadTexture(GraphicName), new Vector2(x - Camera.camX, y - Camera.camY), Color.White);
            }
            Globals.sb.Draw(LoadTex.LoadTexture("UIElements/HPBarBase"), new Rectangle(x - Camera.camX, y - Camera.camY, 160, 60), Color.White);
            if (Health < MaxHealth)
            {
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/HPBarFiller"), new Rectangle(x - Camera.camX, y - Camera.camY, 160 * Health / MaxHealth, 60), new Rectangle(0, 0, 640 * Health / MaxHealth, 240), Color.White);

            }
            else
            {
                Globals.sb.Draw(LoadTex.LoadTexture("UIElements/HPBarFiller"), new Rectangle(x - Camera.camX, y - Camera.camY, 160, 60), Color.White);
            }
        }

        public void ChangePosition(int gridx, int gridy)
        {
            X = gridx;
            Y = gridy;
        }

        public string GetTextureName()
        {
            return GraphicName;
        }

        public void Stun(int time)
        {
            Stunned += time;
        }

        public void SetConfused()
        {
            confused = true;
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
