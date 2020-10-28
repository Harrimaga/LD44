﻿using CottonCandy.Allies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Enemies
{
    [Serializable]
    class SmallPlant:Enemy
    {

        public SmallPlant():base(2,"Enemies/SmallPlant",1, "Small Plant")
        {

        }

        protected override void Attack()
        {
            List<IUnit> units = new List<IUnit>();
            for (int i = X - 1; i < X + 2; i++)
            {
                for (int j = Y - 1; j < Y + 2; j++)
                {
                    if (i < Globals.grid.w && i > 0 && j < Globals.grid.h && j > 0)
                    {
                        IUnit unit = Globals.grid.GetTile(i, j).unit;
                        if (unit != null && !(unit is Enemy))
                        {
                            units.Add(unit);
                        }
                    }
                }
            }
            if (units.Count < 1)
            {
                Move();
            }
            else
            {
                int damage = 1;
                int random = Globals.rng.Next(0, units.Count);
                units[random].Damage(damage, name);
            }
        }
        protected override void Move()
        {
            float shortestDistance = float.MaxValue;
            IUnit closestUnit = null;
            List<IUnit> playerAndAllies = new List<IUnit>();
            foreach (var unit in Globals.grid.units)
            {
                if (unit is Wisp)
                {
                    playerAndAllies.Add(unit);
                }
            }
            if (playerAndAllies.Count > 0)
            {
                foreach (var ally in playerAndAllies)
                {
                    if ((ally.GetPosition() - GetPosition()).Length() < shortestDistance)
                    {
                        closestUnit = ally;
                        shortestDistance = (ally.GetPosition() - GetPosition()).Length();
                    }
                }
                Vector2 newPos = Globals.grid.MoveTowards(this, closestUnit.GetPosition(), false);
                Globals.grid.AddUnitAtPosition(this, (int)newPos.X, (int)newPos.Y);
            }
            else
            {
                base.Move();
            }
        }
    }
}
