using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    public class Shop
    {
        public List<Card> CardStock { get; set; }

        public Shop()
        {
            CardStock = new List<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card c = Card.GetRandomCard();
                while (c == null)
                {
                    c = Card.GetRandomCard();
                }
                CardStock.Add(c);
            }
        }

        public Shop(int amountOfCards)
        {
            CardStock = new List<Card>();
            for (int i = 0; i < amountOfCards; i++)
            {
                Card c = Card.GetRandomCard();
                while (c == null)
                {
                    c = Card.GetRandomCard();
                }
                CardStock.Add(c);
            }
        }

        public Shop(List<Card> stock)
        {
            CardStock = stock;
        }

        public Shop(List<Card> stock, int amountExtra)
        {
            CardStock = stock;
            for (int i = 0; i < amountExtra; i++)
            {
                Card c = Card.GetRandomCard();
                while (c == null)
                {
                    c = Card.GetRandomCard();
                }
                CardStock.Add(c);
            }
        }

        public void Purchase(int index)
        {
            Card c = CardStock[index];
            switch (c.Mode)
            {
                case PurchaseMode.ENEMIES:
                    if (Globals.CurrentPlayer.DeathCurrency >= c.Price)
                    {
                        Globals.CurrentPlayer.DeathCurrency -= c.Price;
                        GiveCardToPlayer(c);
                    }
                    break;
                case PurchaseMode.HP:
                    if (Globals.CurrentPlayer.MaxHealth > c.Price)
                    {
                        Globals.CurrentPlayer.MaxHealth -= c.Price;
                        Globals.CurrentPlayer.Damage(c.Price, "The Shop");
                        GiveCardToPlayer(c);
                    }
                    break;
                case PurchaseMode.ALLIES:
                    if (Globals.CurrentPlayer.SacrificeCurrency >= c.Price)
                    {
                        Globals.CurrentPlayer.SacrificeCurrency -= c.Price;
                        GiveCardToPlayer(c);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("This card is not purchaseable through one of the PurchaseMode enum values");
            }
        }

        private void GiveCardToPlayer(Card card)
        {
            CardStock.Remove(card);
            Globals.Collection.Add(card);
            FileLoader.Write(Globals.Collection, "Collection.bin");
            FileLoader.Write(Globals.CurrentPlayer, "Player.bin");
        }

        public void Draw()
        {
            for (int i = 0; i < CardStock.Count; i++)
            {
                Card c = CardStock[i];
                c.Draw(55 + (50 + Globals.tileW * 2) * i, 5, Color.White);
                if (c.Mode == PurchaseMode.ALLIES)
                {
                    Globals.sb.DrawString(Globals.font, c.Price + " Alies", new Vector2(55 + (50 + Globals.tileW * 2) * i, 5 + Globals.tileH * 2), Color.White);
                }
                else if (c.Mode == PurchaseMode.ENEMIES)
                {
                    Globals.sb.DrawString(Globals.font, c.Price + " Enemies", new Vector2(55 + (50 + Globals.tileW * 2) * i, 5 + Globals.tileH * 2), Color.White);
                }
                else if (c.Mode == PurchaseMode.HP)
                {
                    Globals.sb.DrawString(Globals.font, c.Price + " Max Health", new Vector2(55 + (50 + Globals.tileW * 2) * i, 5 + Globals.tileH * 2), Color.White);
                }
                Globals.sb.DrawString(Globals.bigfont, "Enemy Currency: " + Globals.CurrentPlayer.DeathCurrency, new Vector2(55, Globals.tileH * 2 + 55 + 50 * 0), Color.White);
                Globals.sb.DrawString(Globals.bigfont, "Ally Currency: " + Globals.CurrentPlayer.SacrificeCurrency, new Vector2(55, Globals.tileH * 2 + 55 + 50 * 1), Color.White);
                Globals.sb.DrawString(Globals.bigfont, "Max Health: " + Globals.CurrentPlayer.MaxHealth, new Vector2(55, Globals.tileH * 2 + 55 + 50 * 2), Color.White);
            }
        }

        public void OnClick()
        {
            for (int i = 0; i < CardStock.Count; i++)
            {
                int x = 55 + (50 + Globals.tileW * 2) * i;
                int y = 5;
                if ((new Rectangle(x, y, Globals.tileW * 2, Globals.tileH * 2)).Contains(Globals.mouse))
                {
                    Purchase(i);
                    return;
                }
            }
        }

    }
}
