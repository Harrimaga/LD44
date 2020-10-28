using CottonCandy.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    [Serializable]
    public abstract class Card : IComparable<Card>
    {
        public bool IsTargeted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public PurchaseMode Mode { get; set; }
        public string GraphicName { get; set; }

        protected Card(bool isTargeted, string name, string description, int price, PurchaseMode mode, string graphicName)
        {
            IsTargeted = isTargeted;
            Name = name;
            Description = description;
            Price = price;
            Mode = mode;
            GraphicName = graphicName;
        }
        public abstract bool Play();
        public abstract bool Play(int x, int y);

        public virtual void Draw(int x, int y, Color c)
        {
            Globals.sb.Draw(LoadTex.LoadTexture(GraphicName), new Vector2(x, y), c);
            Globals.sb.DrawString(Globals.font, Name, new Vector2(x + 26, y + 26), Color.Black);
            Globals.sb.DrawString(Globals.font, Text.BreakUpString(Globals.font, Description, Globals.tileW*2 - 52), new Vector2(x + 26, y + 56), Color.Black);
        }

        public static Card GetRandomCard()
        {
            Card result = null;

            if (Globals.rng.Next(100) < 75)
            {
                switch (Globals.rng.Next(0, 12))
                {
                    case 0:
                        result = new HealMinorWounds();
                        break;
                    case 1:
                        result = new StoneSkin();
                        break;
                    case 2:
                        result = new Reflex();
                        break;
                    case 3:
                        result = new Whirlwind();
                        break;
                    case 4:
                        result = new RayOfFrost();
                        break;
                    case 5:
                        result = new MassConfusion();
                        break;
                    case 6:
                        result = new FireWave();
                        break;
                    case 7:
                        result = new Surge();
                        break;
                    case 8:
                        result = new SummonWisp();
                        break;
                    case 9:
                        result = new HealingWord();
                        break;
                    case 10:
                        result = new Strike();
                        break;
                    case 11:
                        result = new WildMagic();
                        break;
                    default:
                        FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at Card.cs->GetRandomCard()] The random value chosen for the card doesn't correlate to a card!", "log.txt");
                        break;
                }
            }
            else if (Globals.rng.Next(100) < 75)
            {
                switch (Globals.rng.Next(0, 10))
                {
                    case 0:
                        result = new Heal();
                        break;
                    case 1:
                        result = new Transfigure();
                        break;
                    case 2:
                        result = new Assassinate();
                        break;
                    case 3:
                        result = new FurySwipes();
                        break;
                    case 4:
                        result = new NetherSwap();
                        break;
                    case 5:
                        result = new UnstableStormCloud();
                        break;
                    case 6:
                        result = new Ensnare();
                        break;
                    case 7:
                        result = new SummonLesserCreature();
                        break;
                    case 8:
                        result = new Transfigure();
                        break;
                    case 9:
                        result = new Disposition();
                        break;
                    default:
                        FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at Card.cs->GetRandomCard()] The random value chosen for the card doesn't correlate to a card!", "log.txt");
                        break;
                }
            }
            else if (Globals.rng.Next(100) < 75)
            {
                switch (Globals.rng.Next(0, 7))
                {
                    case 0:
                        result = new Sacrifice();
                        break;
                    case 1:
                        result = new ChainedEnergy();
                        break;
                    case 2:
                        result = new BloodRite();
                        break;
                    case 3:
                        result = new Fireball();
                        break;
                    case 4:
                        result = new Petrify();
                        break;
                    case 5:
                        result = new Blink();
                        break;
                    case 6:
                        result = new DemonicPulse();
                        break;
                    default:
                        FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at Card.cs->GetRandomCard()] The random value chosen for the card doesn't correlate to a card!", "log.txt");
                        break;
                }
            }
            else if (Globals.rng.Next(100) < 75)
            {
                switch (Globals.rng.Next(0, 6))
                {
                    case 0:
                        result = new Barge();
                        break;
                    case 1:
                        result = new Endeavor();
                        break;
                    case 2:
                        result = new Explosion();
                        break;
                    case 3:
                        result = new SummonDemon();
                        break;
                    case 4:
                        result = new Warp();
                        break;
                    case 5:
                        result = new ThundergodsWrath();
                        break;
                    default:
                        FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at Card.cs->GetRandomCard()] The random value chosen for the card doesn't correlate to a card!", "log.txt");
                        break;
                }
            }
            return result;
        }

        public int CompareTo(Card obj)
        {
            return Name.CompareTo(obj.Name);
        }
    }

    public enum PurchaseMode
    {
        ENEMIES, HP, ALLIES
    }
}
