using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CottonCandy
{
    interface IUnit
    {
        void Damage(int amount, string unitName);
        void Update();
        void Draw();
        void ChangePosition(int x, int y);
        Vector2 GetPosition();
        string GetTextureName();
        void TakeTurn();
        void Stun(int time);
        void Heal(int amount);
        bool IsDead();
    }
}
