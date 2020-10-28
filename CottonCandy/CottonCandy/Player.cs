using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CottonCandy.Effects;
using Microsoft.Xna.Framework;

namespace CottonCandy
{
    [Serializable]
    public class Player : IUnit
    {
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        private List<Card> Deck;
        private readonly List<Effect> Effects;
        public int x;
        public int y;
        public string texname = "BasePlayer";
        public float damageMultiplier;
        public int CardsPlayed { get; set; }
        public int DeathCurrency { get; set; }
        public int SacrificeCurrency { get; set; }
        public int X { get { return x / Globals.tileW; } set { x = value * Globals.tileW; } }
        public int Y { get { return y / Globals.tileH; } set { y = value * Globals.tileH; } }
        public List<int> CompletedLevels { get; set; }

        public Player(int maxHealth, List<Card> Deck)
        {
            this.MaxHealth = maxHealth;
            this.Health = maxHealth;
            this.Deck = new List<Card>(Deck);
            Effects = new List<Effect>();
            DeathCurrency = 0;
            SacrificeCurrency = 0;
            damageMultiplier = 1f;
            CompletedLevels = new List<int>();
        }

        public void StartLevel()
        {

        }

        public void TakeTurn()
        {
            Update();
        }

        public void AddToDeck(Card card)
        {
            Deck.Add(card);
        }

        public void RemoveFromDeck(Card card)
        {
            Deck.Remove(card);
        }

        public void AddEffect(Effect effect)
        {
            Effects.Add(effect);
        }

        public void PutCardBack()
        {
            if (Deck.Count == 0)
            {
                Globals.gameState = GameStates.GAMEOVER;
                CombatLog.AddLine("You had no cards left, you lost");
                return;
            }
            var temp = Deck[0];
            Deck.RemoveAt(0);
            Deck.Add(temp);
        }

        public void Shuffle()
        {
            List<Card> temp = new List<Card>();

            while (Deck.Count > 0)
            {
                int rnd = Globals.rng.Next(Deck.Count);
                temp.Add(Deck[rnd]);
                Deck.RemoveAt(rnd);
            }

            Deck = temp;
        }

        public bool PlayCard()
        {
            if (Deck.Count > 0 && Deck[0].Play())
            {
                CombatLog.AddLine("Used " + Deck[0].Name);
                Deck.RemoveAt(0);
                Globals.grid.KillUnits();
                CardsPlayed++;
                return true;
            }
            return false;
        }

        public bool PlayCard(int x, int y)
        {
            if (Deck.Count > 0 && Deck[0].Play(x, y))
            {
                CombatLog.AddLine("Used " + Deck[0].Name);
                Deck.RemoveAt(0);
                Globals.grid.KillUnits();
                CardsPlayed++;
                return true;
            }
            return false;
        }

        public List<Card> NextCards()
        {
            List<Card> res = new List<Card>
            {
                Deck[0],
                Deck[1],
                Deck[2],
                Deck[3]
            };
            return res;
        }

        public void Damage(int amount, string unitName)
        {
            int damage = (int)Math.Ceiling(damageMultiplier * amount);
            Health -= damage;
            CombatLog.AddLine("Took " + damage + " damage by " + unitName);
            if (Health < 1)
            {
                Globals.gameState = GameStates.GAMEOVER;
            }

            foreach (var e in Effects)
            {
                if (e is Transfigured b)
                {
                    b.damageTaken += damage;
                }
            }
        }
        public void Update()
        {
            UpdateEffects();
            Globals.hasMoved = false;
        }

        private void UpdateEffects()
        {
            List<Effect> removables = new List<Effect>();
            foreach (var e in Effects)
            {
                e.Update();
                if (e.Duration < 1)
                {
                    removables.Add(e);
                }
            }
            foreach (var e in removables)
            {
                Effects.Remove(e);
            }
        }

        public void Draw()
        {
            Globals.sb.Draw(LoadTex.LoadTexture(texname), new Vector2(x - Camera.camX, y - Camera.camY), Color.White);
        }

        public void ChangePosition(int x, int y)
        {
            Globals.grid.RemoveUnit(this, X, Y);
            X = x;
            Y = y;
            Globals.grid.GetTile(x, y).unit = this;
        }

        public string GetTextureName()
        {
            return texname;
        }

        public void DrawCardUI()
        {
            if (Deck.Count >= 4)
            {
                Deck[3].Draw(Globals.screenW - Globals.tileW * 2, Globals.screenH - Globals.tileH * 2 - 50 * 3, new Color(80, 80, 80, 200));
            }
            if (Deck.Count >= 3)
            {
                Deck[2].Draw(Globals.screenW - Globals.tileW * 2, Globals.screenH - Globals.tileH * 2 - 50 * 2, Color.Gray);
            }
            if (Deck.Count >= 2)
            {
                Deck[1].Draw(Globals.screenW - Globals.tileW * 2, Globals.screenH - Globals.tileH * 2 - 50, Color.LightGray);
            }
            if (Deck.Count >= 1)
            {
                Deck[0].Draw(Globals.screenW - Globals.tileW * 2, Globals.screenH - Globals.tileH * 2, Color.White);
            }
            Globals.sb.DrawString(Globals.font, "CARDS IN DECK: " + Deck.Count, new Vector2(Globals.screenW - Globals.tileW * 2, 220), Color.White);
            Globals.sb.DrawString(Globals.bigfont, "Health: \r\n" + Health + "/" + MaxHealth, new Vector2(Globals.screenW - Globals.tileW * 2, 5), Color.White);
            Globals.sb.Draw(LoadTex.LoadTexture("UIElements/HPBarBase"), new Rectangle(Globals.screenW - Globals.tileW * 2, 100, 320, 120), Color.White);
            Globals.sb.Draw(LoadTex.LoadTexture("UIElements/HPBarFiller"), new Rectangle(Globals.screenW - Globals.tileW * 2, 100, 320 * Health / MaxHealth, 120), new Rectangle(0, 0, 640 * Health / MaxHealth, 240), Color.White);
        }

        public void Reset(List<Card> Deck)
        {
            this.Deck = new List<Card>(Deck);
            Shuffle();
            Health = MaxHealth;
            CardsPlayed = 0;
            Globals.hasMoved = false;
            Effects.Clear();
        }

        public void Stun(int time)
        {
            CombatLog.AddLine("You got stunned for " + time + " turns");
            for (int i = 0; i < time; i++)
            {
                for (int j = 0; j < Globals.grid.units.Count; j++)
                {
                    if (!(Globals.grid.units[j] is Player))
                    {
                        Globals.grid.units[j].TakeTurn();
                    }
                }
                UpdateEffects();
            }
        }

        public void Heal(int amount)
        {
            CombatLog.AddLine("You healed for " + amount + " health");
            if (Health + amount > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += amount;
            }
        }

        public Vector2 GetPosition()
        {
            return new Vector2(x / Globals.tileW, y / Globals.tileH);
        }

        public bool IsDead()
        {
            return Health < 1;
        }
    }
}
