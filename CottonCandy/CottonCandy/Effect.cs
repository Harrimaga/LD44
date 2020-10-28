using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    [Serializable]
    public abstract class Effect
    {
        public string Name { get; set; }
        public int Duration { get; set; }

        protected Effect(int duration)
        {
            this.Duration = duration;
        }

        public abstract void RemoveEffect();

        public abstract void AddEffect();

        public virtual void Update()
        {
            Duration--;
            if (Duration < 1)
            {
                RemoveEffect();
            }
        }



    }
}
