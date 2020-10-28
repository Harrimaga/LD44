using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Cards
{
    [Serializable]
    class Reflex : Card
    {

        bool[] movesChecked;
        public Reflex() : base(false, "Reflex", "Take " + CardBalance.REFLEX_MOVEMENT + " movements in random directions", CardBalance.REFLEX_COST, PurchaseMode.ENEMIES, "Cards/SelfCastCard")
        {

        }

        public override bool Play()
        {
            for (int i = 0; i < CardBalance.REFLEX_MOVEMENT; i++)
            {
                movesChecked = new bool[4];
                MoveRandom();
            }
            return true;
        }

        public override bool Play(int x, int y)
        {
            return false;
        }

        public bool MoveRandom()
        {
            if (movesChecked[0] && movesChecked[1] && movesChecked[2] & movesChecked[3])
            {
                return false;
            }
            switch (Globals.rng.Next(0, 4))
            {
                case 0:
                    if (!movesChecked[0] && Globals.CurrentPlayer.x > 0 && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW) - 1, (Globals.CurrentPlayer.y / Globals.tileH)].colidable == TileType.NOTSOLID && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW) - 1, (Globals.CurrentPlayer.y / Globals.tileH)].unit == null)
                    {
                        Globals.grid.MoveUnit(Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH, Globals.CurrentPlayer.x / Globals.tileW - 1, Globals.CurrentPlayer.y / Globals.tileH);
                        return true;
                    }
                    else
                    {
                        movesChecked[0] = true;
                        return MoveRandom();
                    }
                case 1:
                    if (!movesChecked[1] && Globals.CurrentPlayer.y > 0 && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW), (Globals.CurrentPlayer.y / Globals.tileH) - 1].colidable == TileType.NOTSOLID && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW), (Globals.CurrentPlayer.y / Globals.tileH) - 1].unit == null)
                    {
                        Globals.grid.MoveUnit(Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH, Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH - 1);
                        return true;
                    }
                    else
                    {
                        movesChecked[1] = true;
                        return MoveRandom();
                    }
                case 2:
                    if (!movesChecked[2] && Globals.CurrentPlayer.x < Globals.grid.w * Globals.tileW - Globals.tileW && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW) + 1, (Globals.CurrentPlayer.y / Globals.tileH)].colidable == TileType.NOTSOLID && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW) + 1, (Globals.CurrentPlayer.y / Globals.tileH)].unit == null)
                    {
                        Globals.grid.MoveUnit(Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH, Globals.CurrentPlayer.x / Globals.tileW + 1, Globals.CurrentPlayer.y / Globals.tileH);
                        return true;
                    }
                    else
                    {
                        movesChecked[2] = true;
                        return MoveRandom();
                    }
                case 3:
                    if (!movesChecked[3] && Globals.CurrentPlayer.y < Globals.grid.h * Globals.tileH - Globals.tileH && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW), (Globals.CurrentPlayer.y / Globals.tileH) + 1].colidable == TileType.NOTSOLID && Globals.grid.grid[(Globals.CurrentPlayer.x / Globals.tileW), (Globals.CurrentPlayer.y / Globals.tileH) + 1].unit == null)
                    {
                        Globals.grid.MoveUnit(Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH, Globals.CurrentPlayer.x / Globals.tileW, Globals.CurrentPlayer.y / Globals.tileH + 1);
                        return true;
                    }
                    else
                    {
                        movesChecked[3] = true;
                        return MoveRandom();
                    }
                default:
                    throw new IndexOutOfRangeException("random value is not between 0 and 3!");
            }
        }

    }
}
