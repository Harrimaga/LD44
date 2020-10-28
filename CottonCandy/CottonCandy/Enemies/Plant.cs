using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.Enemies
{
    public struct Position
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
    }

    [Serializable]
    public class Plant : Enemy
    {
        public Plant() : base(22, "Enemies/Plant", 1, "Plant")
        {

        }

        protected override void Move()
        {

        }

        protected override void Attack()
        {
            if (turnCycle % 4 != 0)
            {
                return;
            }
            List<Position> squares = new List<Position>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (X + i > 0 && X + i < Globals.grid.w && Y + j > 0 && Y + j < Globals.grid.h)
                    {
                        if (Globals.grid.GetTile(X + i, Y + j).unit == null && Globals.grid.GetTile(X + i, Y + j).colidable == TileType.NOTSOLID)
                        {
                            Position pos = new Position
                            {
                                XPos = X + i,
                                YPos = Y + j
                            };
                            squares.Add(pos);
                        }
                    }
                }
            }
            if (squares.Count < 1)
            {
                return;
            }

            int index = Globals.rng.Next(0, squares.Count);
            int x = squares[index].XPos;
            int y = squares[index].YPos;
            SmallPlant sp = new SmallPlant();
            Globals.grid.AddUnitAtPosition(sp, x, y);

        }
    }
}
