using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    public class CardBalance
    {
        //Assasinate
        public const int ASSASINATE_DAMAGE = 6;
        public const int ASSASINATE_COST = 12;
        //Barge
        public const int BARGE_RANGE = 3;
        public const int BARGE_DAMAGE = 10;
        public const int BARGE_COST = 15;
        //Blink
        public const int BLINK_RANGE = 3;
        public const int BLINK_COST = 20;
        //BloodRite
        public const int BLOODRITE_SELFDAMAGE = 10;
        public const int BLOODRITE_DAMAGE = 20;
        public const int BLOODRITE_COST = 30;
        //ChainedEnergy
        public const int CHAINEDENERGY_DAMAGE = 5;
        public const int CHAINEDENERGY_CHANCE = 50;
        public const int CHAINEDENERGY_RANGE = 5;//Cube
        public const int CHAINEDENERGY_COST = 8;
        //DemonicPulse
        public const int DEMONICPULSE_DAMAGE = 10;
        public const int DEMONICPULSE_RANGE = 5;//Cube
        public const int DEMONICPULSE_SELFDAMAGE = 5;
        public const int DEMONICPULSE_COST = 5;
        //Disposition
        public const int DISPOSITION_COST = 12;
        //Endeavor
        public const int ENDEAVOR_COST = 31;
        //Ensnare
        public const int ENSNARE_RANGE = 3;//Diamond
        public const int ENSNARE_COST = 15;
        public const int ENSNARE_STUN = 1;
        //Explosion
        public const int EXPLOSION_DICEAMOUNT = 2;
        public const int EXPLOSION_DICESIDES = 6;
        public const int EXPLOSION_RANGE = 5;//Cube
        public const int EXPLOSION_COST = 18;
        //Fireball
        public const int FIREBALL_DAMAGE = 4;
        public const int FIREBALL_RANGE = 3;//Cube
        public const int FIREBALL_COST = 10;
        //Firewave
        public const int FIREWAVE_RANGE = 3;//Diamond
        public const int FIREWAVE_LOWDAMAGE = 1;
        public const int FIREWAVE_HIGHDAMAGE = 3;
        public const int FIREWAVE_COST = 5;
        //Furyswipes
        public const int FURYSWIPES_COST = 10;
        public const int FURYSWIPES_CHANCE = 85;
        public const int FURYSWIPES_DAMAGE = 1;
        //Heal
        public const int HEAL_HEAL = 4;
        public const int HEAL_COST = 10;
        //HealingWord
        public const int HEALINGWORD_LOWHEAL = 1;
        public const int HEALINGWORD_HIGHHEAL = 4;
        public const int HEALINGWORD_COST = 8;
        //HealMinorWounds
        public const int HEALMINORWOUNDS_HEAL = 2;
        public const int HEALMINORWOUNDS_COST = 15;
        //MassConfusion
        public const int MASSCONFUSION_RANGE = 5; //Cube
        public const int MASSCONFUSION_COST = 10;
        //NetherSwap
        public const int NETHERSWAP_RANGE = 2;
        public const int NETHERSWAP_COST = 6;
        //Petrify
        public const int PETRIFY_RANGE = 3; //Cube
        public const int PETRIFY_COST = 10;
        public const int PETRIFY_DURATION = 2;
        //RayOfFrost
        public const int RAYOFFROST_DAMAGE = 2;
        public const int RAYOFFROST_STUN_DURATION = 1;
        public const int RAYOFFROST_COST = 6;
        //Reflex
        public const int REFLEX_MOVEMENT = 2;
        public const int REFLEX_COST = 10;
        //Sacrifice
        public const int SACRIFICE_DAMAGE = 5;
        public const int SACRIFICE_HEAL = 2;
        public const int SACRIFICE_COST = 15;
        //StoneSkin
        public const float STONESKIN_DAMAGE_REDUCTION = 0.25f;
        public const int STONESKIN_DURATION = 2;
        public const int STONESKIN_COST = 5;
        //Strike
        public const int STRIKE_COST = 3;
        public const int STRIKE_DAMAGE = 2;
        //SummonDemon
        public const int SUMMONDEMON_SELFCOST = 10;
        public const int SUMMONDEMON_COST = 21;
        public const int SUMMONDEMON_HP = 20;
        public const int SUMMONDEMON_DAMAGE = 4;
        //SummonLesserCreature
        public const int SUMMONLESSERCREATURE_COST = 5;
        public const int SUMMONLESSERCREATURE_HP = 4;
        public const int SUMMONLESSERCREATURE_DAMAGE = 1;
        //SummonWisp
        public const int SUMMONWISP_COST = 6;
        public const int SUMMONWISP_HEAL = 1;
        public const int SUMMONWISP_TURNS = 2;
        public const int SUMMONWISP_HP = 1;
        //Surge
        public const int SURGE_COST = 7;
        //ThundergodsWrath
        public const int THUNDERGODSWRATH_DAMAGE_LOW = 1;
        public const int THUNDERGODSWRATH_DAMAGE_HIGH = 8;
        public const int THUNDERGODSWRATH_COST = 35;
        //Transfigure
        public const int TRANSFIGURE_DURATION = 2;
        public const int TRANSFIGURE_COST = 10;
        public const float TRANSFIGURE_HEAL = 1.5f;
        //UnstableStormCloud
        public const int UNSTABLESTORMCLOUD_DAMAGE = 3;
        public const int UNSTABLESTORMCLOUD_RANGE = 5; //Cube
        public const int UNSTABLESTORMCLOUD_COST = 12;
        public const int UNSTABLESTORMCLOUD_CHANCE = 65;
        //Warp
        public const int WARP_COST = 13;
        //Whirlwind
        public const int WHIRLWIND_COST = 5;
        public const int WHIRLWIND_BLOWBACK = 2;
        //WildMagic
        public const int WILDMAGIC_DAMAGE_LOW = 1;
        public const int WILDMAGIC_DAMAGE_HIGH = 3;
        public const int WILDMAGIC_COST = 5;
        public const int WILDMAGIC_RANGE = 3; //Cube
    }
}
