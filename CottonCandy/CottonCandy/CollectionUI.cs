using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CottonCandy
{
    class CollectionUI
    {

        private static List<Card> col, deck;
        private static List<int> ColAm, DeckAm;
        private static int colScroll = 0, deckScroll = 0, prevScroll = 0;

        public static void Init()
        {
            col = new List<Card>();
            deck = new List<Card>();
            ColAm = new List<int>();
            DeckAm = new List<int>();
            Globals.Collection.Sort();
            Globals.LastDeck.Sort();
            foreach (Card c in Globals.Collection)//collection
            {
                bool found = false;
                int i = 0;
                foreach (Card cc in col)
                {
                    if (c.Name.Equals(cc.Name))
                    {
                        found = true;
                        ColAm[i]++;
                        break;
                    }
                    i++;
                }
                if (!found)
                {
                    col.Add(c);
                    ColAm.Add(1);
                }
            }
            CreateDeck();
        }
        private static void CreateDeck()
        {
            foreach (Card c in Globals.LastDeck)//deck
            {
                bool found = false;
                int i = 0;
                foreach (Card cc in deck)
                {
                    if (c.Name.Equals(cc.Name))
                    {
                        found = true;
                        DeckAm[i]++;
                        break;
                    }
                    i++;
                }
                if (!found)
                {
                    deck.Add(c);
                    DeckAm.Add(1);
                }
            }
        }

        public static void Update()
        {
            int scrollDelta = prevScroll - Mouse.GetState().ScrollWheelValue;
            prevScroll = Mouse.GetState().ScrollWheelValue;
            if (scrollDelta > 0)
            {
                if ((new Rectangle(0, 0, Globals.screenW / 2, Globals.screenH)).Contains(Globals.mouse))
                {
                    colScroll++;
                    if (colScroll > Math.Ceiling(col.Count / 2.0) - 3)
                    {
                        colScroll = ((int)Math.Ceiling(col.Count / 2.0)) - 3;
                        if (colScroll < 0)
                        {
                            colScroll = 0;
                        }
                    }
                }
                else
                {
                    deckScroll++;
                    if (deckScroll > Math.Ceiling(deck.Count / 2.0) - 3)
                    {
                        deckScroll = (int)Math.Ceiling(deck.Count / 2.0) - 3;
                        if (deckScroll < 0)
                        {
                            deckScroll = 0;
                        }
                    }
                }
            }
            else if (scrollDelta < 0)
            {
                if ((new Rectangle(0, 0, Globals.screenW / 2, Globals.screenH)).Contains(Globals.mouse))
                {
                    colScroll--;
                    if (colScroll < 0)
                    {
                        colScroll = 0;
                    }
                }
                else
                {
                    deckScroll--;
                    if (deckScroll < 0)
                    {
                        deckScroll = 0;
                    }
                }
            }
        }

        public static void OnLeftClick()
        {
            Point mp = Globals.mouse;
            for (int i = 0; i < col.Count; i++)
            {
                Card c = col[i];
                int am = ColAm[i];
                int x = 0;
                int y = 0;
                if (i % 2 == 0)
                {
                    x = 5;
                    y = 5 + (30 + Globals.tileH * 2) * (i / 2 - colScroll);
                }
                else
                {
                    x = 5 + Globals.tileW * 2 + 30;
                    y = 5 + (30 + Globals.tileH * 2) * (i / 2 - colScroll);
                }
                if ((new Rectangle(x, y, Globals.tileW * 2, Globals.tileH * 2)).Contains(mp))
                {
                    for (int j = 0; j < deck.Count; j++)
                    {
                        if (c.Name.Equals(deck[j].Name))
                        {
                            if (DeckAm[j] < am)
                            {
                                DeckAm[j]++;
                                foreach (Card card in Globals.Collection)
                                {
                                    if (card.Name.Equals(c.Name))
                                    {
                                        bool found = false;
                                        foreach (Card deckCard in Globals.LastDeck)
                                        {
                                            if (deckCard == card)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found)
                                        {
                                            Globals.LastDeck.Add(card);
                                            
                                            FileLoader.Write(Globals.LastDeck, "Deck.bin");
                                            break;
                                        }
                                    }
                                }
                            }
                            return;
                        }
                    }

                    Globals.LastDeck.Add(c);
                    FileLoader.Write(Globals.LastDeck, "Deck.bin");
                    deck.Add(c);
                    DeckAm.Add(1);
                    Globals.LastDeck.Sort();
                    deck.Clear();
                    DeckAm.Clear();
                    CreateDeck();

                }
            }
        }

        public static void OnRightClick()
        {
            Point mp = Globals.mouse;
            for (int i = 0; i < deck.Count; i++)
            {
                Card c = deck[i];
                int x = 0;
                int y = 0;
                if (i % 2 == 0)
                {
                    x = Globals.screenW - Globals.tileW * 4 - 35;
                    y = 5 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll);
                }
                else
                {
                    x = Globals.screenW - Globals.tileW * 2 - 5;
                    y = 5 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll);
                }
                if ((new Rectangle(x, y, Globals.tileW * 2, Globals.tileH * 2)).Contains(mp))
                {
                    if (DeckAm[i] == 1)
                    {
                        DeckAm.RemoveAt(i);
                        deck.RemoveAt(i);
                    }
                    else
                    {
                        DeckAm[i]--;
                    }
                    foreach (Card card in Globals.LastDeck)
                    {
                        if (card.Name.Equals(c.Name))
                        {
                            Globals.LastDeck.Remove(card);
                            FileLoader.Write(Globals.LastDeck, "Deck.bin");
                            return;
                        }
                    }
                }
            }
        }

        public static void Draw()
        {
            Globals.sb.DrawString(Globals.font, "Collection (left click to add to deck)", new Vector2(30, 5), Color.White);
            Globals.sb.DrawString(Globals.font, "Deck (Right click to remove from deck)", new Vector2(Globals.screenW - Globals.tileW*4 - 35, 5), Color.White);
            for (int i = 0; i < col.Count; i++)
            {
                Card c = col[i];
                int am = ColAm[i];
                if (i % 2 == 0)
                {
                    c.Draw(25, 25 + (30 + Globals.tileH * 2) * (i / 2 - colScroll), Color.White);
                    Globals.sb.DrawString(Globals.font, "x" + am, new Vector2(25, 25 + (30 + Globals.tileH * 2) * (i / 2 - colScroll) + Globals.tileH * 2), Color.White);
                }
                else
                {
                    c.Draw(25 + Globals.tileW * 2 + 30, 25 + (30 + Globals.tileH * 2) * (i / 2 - colScroll), Color.White);
                    Globals.sb.DrawString(Globals.font, "x" + am, new Vector2(25 + Globals.tileW * 2 + 30, 25 + (30 + Globals.tileH * 2) * (i / 2 - colScroll) + Globals.tileH * 2), Color.White);
                }
            }
            for (int i = 0; i < deck.Count; i++)
            {
                Card c = deck[i];
                int am = DeckAm[i];
                if (i % 2 == 0)
                {
                    c.Draw(Globals.screenW - Globals.tileW * 4 - 35, 25 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll), Color.White);
                    Globals.sb.DrawString(Globals.font, "x" + am, new Vector2(Globals.screenW - Globals.tileW * 4 - 35, 25 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll) + Globals.tileH * 2), Color.White);
                }
                else
                {
                    c.Draw(Globals.screenW - Globals.tileW * 2 - 5, 25 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll), Color.White);
                    Globals.sb.DrawString(Globals.font, "x" + am, new Vector2(Globals.screenW - Globals.tileW * 2 - 5, 25 + (30 + Globals.tileH * 2) * (i / 2 - deckScroll) + Globals.tileH * 2), Color.White);
                }
            }
        }

    }
}
