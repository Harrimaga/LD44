using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Enemies
{
    [Serializable]
    class Noob : Enemy
    {
        public Noob() : base(1, "Enemies/BaseEnemy",1, "Noob")
        {

        }
    }
}
