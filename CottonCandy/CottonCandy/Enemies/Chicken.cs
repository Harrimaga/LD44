using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Enemies
{
    [Serializable]
    public class Chicken : Enemy
    {

        public Chicken() : base(3, "Enemies/Chicken", 1,"Chicken")
        {

        }

        protected override void Attack()
        {
            if (turnCycle % 2 != 0)
            {
                return;
            }

            int playerY = Globals.CurrentPlayer.y / Globals.tileH;
            int playerX = Globals.CurrentPlayer.x / Globals.tileW;


            if (playerX > X - 4 && playerX < X + 4 && playerY > Y - 4 && playerY < Y + 4)
            {
                Globals.CurrentPlayer.Damage(1, name);
            }
        }
    }
}
