using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy
{
    [Serializable]
    class Tilegrid
    {

        public int w, h, reward, condition = 0;
        public Tile[,] grid;
        public List<IUnit> units = new List<IUnit>();
        public List<IUnit> killedUnits = new List<IUnit>();
        public Player p;

        public Tilegrid(int w, int h)
        {
            this.w = w;
            this.h = h;
            grid = new Tile[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    grid[i, j] = new Tile("Tiles/Ground", i * Globals.tileW, j * Globals.tileH, TileType.NOTSOLID);
                }
            }
        }

        public void DealDamage(int x, int y, int amount, string name)
        {
            if (grid[x, y].unit != null)
            {
                grid[x, y].unit.Damage(amount, name);
            }
        }

        public List<Vector2> EmptyTiles()
        {
            List<Vector2> emptyTiles = new List<Vector2>();

            for (int x = 0; x < Globals.grid.grid.GetLength(0); x++)
            {
                for (int y = 0; y < Globals.grid.grid.GetLength(1); y++)
                {
                    if (Globals.grid.grid[x, y].unit is null && Globals.grid.grid[x, y].colidable == TileType.NOTSOLID)
                    {
                        emptyTiles.Add(new Vector2(x, y));
                    }
                }
            }

            return emptyTiles;
        }

        public Vector2 RandomEmtpyTile()
        {
            List<Vector2> tiles = EmptyTiles();
            return tiles[Globals.rng.Next(tiles.Count)];
        }

        public void RandomWarpUnit(int x, int y)
        {
            Vector2 loc = RandomEmtpyTile();
            IUnit u = grid[x, y].unit;
            AddUnitAtPosition(u, (int)loc.X, (int)loc.Y);

        }

        public Tile GetTile(int x, int y)
        {
            return grid[x, y];
        }

        public void SetTile(int x, int y, Tile t)
        {
            grid[x, y] = t;
        }

        public void KillUnits()
        {
            foreach (var e in killedUnits)
            {
                RemoveUnit(e, (int)e.GetPosition().X, (int)e.GetPosition().Y);
            }
            killedUnits.Clear();
        }

        public void Draw()
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    grid[i, j].Draw();
                }
            }
        }

        public bool RemoveUnit(IUnit unit, int x, int y)
        {
            grid[x, y].unit = null;
            return units.Remove(unit);
        }

        public void MoveUnit(int ox, int oy, int nx, int ny)
        {
            if (!Globals.hasMoved)
            {
                IUnit u = grid[ox, oy].unit;
                if (u != null && grid[nx, ny].colidable != TileType.SOLID && grid[nx, ny].unit == null)
                {
                    AddUnitAtPosition(u, nx, ny);
                    Globals.hasMoved = true;
                    if (grid[nx, ny].colidable == TileType.FINISH)
                    {
                        Globals.gameState = GameStates.WON;
                        Globals.CurrentPlayer.Reset(Globals.LastDeck);
                        if (!Globals.CurrentPlayer.CompletedLevels.Contains(Globals.currentLevel))
                        {
                            Globals.CurrentPlayer.CompletedLevels.Add(Globals.currentLevel);
                            int re = reward - Globals.CurrentPlayer.CardsPlayed / 2;
                            if (re < 2)
                            {
                                re = 2;
                            }
                            Globals.CurrentPlayer.MaxHealth += re;
                        }
                        return;
                    }
                }
            }
        }

        public void AddUnitAtPosition(IUnit unit, int gridX, int gridY)
        {
            if (!units.Contains(unit))
            {
                units.Add(unit);
            }
            else
            {
                GetTile((int)unit.GetPosition().X, (int)unit.GetPosition().Y).unit = null;
            }
            GetTile(gridX, gridY).unit = unit;
            unit.ChangePosition(gridX, gridY);
        }

        public void ResetDamaged()
        {
            foreach (IUnit u in units)
            {
                if (u is Enemy)
                {
                    ((Enemy)u).damaged = false;
                }
            }
        }

        private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
        {
            List<Vector2> totalPath = new List<Vector2>()
            {
                current
            };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            return totalPath;
        }

        private List<Vector2> GetNeighbourEmptyNodes(Vector2 node, bool diagonal)
        {
            List<Vector2> res = new List<Vector2>();

            if (diagonal)
            {
                int range = 1;
                for (int i = (int)node.X - range; i <= (int)node.X + range; i++)
                {
                    for (int j = (int)node.Y - range; j <= (int)node.Y + range; j++)
                    {
                        if (i >= Globals.grid.w || i < 0 || j >= Globals.grid.h || j < 0) { continue; }
                        if (Globals.grid.grid[i, j].colidable == TileType.NOTSOLID)
                        {
                            res.Add(new Vector2(i, j));
                        }
                    }
                }
            }
            else
            {
                Vector2[] directions = new Vector2[4]
                {
                    new Vector2(0, 1),
                    new Vector2(0, -1),
                    new Vector2(1, 0),
                    new Vector2(-1, 0)
                };

                foreach (Vector2 direction in directions)
                {
                    if ((node + direction).X > 0 && (node + direction).X < Globals.grid.w && (node + direction).Y > 0 && (node + direction).Y < Globals.grid.h && Globals.grid.grid[(int)(node + direction).X, (int)(node + direction).Y].colidable == TileType.NOTSOLID)
                    {
                        res.Add(node + direction);
                    }
                }
            }
            
            return res;
        }

        public Vector2 MoveTowards(IUnit unit, Vector2 goal, bool diagonal)
        {
            float heuristic = (unit.GetPosition() - goal).Length();
            Vector2 start = unit.GetPosition();
            List<Vector2> visitedNodes = new List<Vector2>();
            List<Vector2> openNodes = new List<Vector2>()
            {
                start
            };
            Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

            Dictionary<Vector2, float> cost = new Dictionary<Vector2, float>
            {
                { start, 0 }
            };

            Dictionary<Vector2, float> estCost = new Dictionary<Vector2, float>
            {
                { start, heuristic }
            };

            while (openNodes.Count > 0)
            {
                Dictionary<Vector2, float> tempCost = new Dictionary<Vector2, float>();
                foreach (Vector2 vector2 in cost.Keys)
                {
                    if (openNodes.Contains(vector2))
                    {
                        tempCost.Add(vector2, cost[vector2]);
                    }
                }

                var orderedCost = tempCost.OrderBy(key => key.Value);

                Vector2 currentNode = orderedCost.First().Key;

                if (currentNode == goal)
                {
                    List<Vector2> path = ReconstructPath(cameFrom, currentNode);
                    if (path.Count > 2)
                    {
                        if (Globals.grid.grid[(int)path[path.Count - 2].X, (int)path[path.Count - 2].Y].unit == null)
                        {
                            return path[path.Count - 2];
                        }
                        return path[path.Count - 1];
                    }
                    else
                    {
                        return path[1];
                    }
                }

                openNodes.Remove(currentNode);
                visitedNodes.Add(currentNode);

                List<Vector2> neighbourNodes = GetNeighbourEmptyNodes(currentNode, diagonal);

                foreach (var node in neighbourNodes)
                {
                    if (visitedNodes.Contains(node))
                    {
                        continue;
                    }

                    float tCost = cost[currentNode] + (currentNode - node).Length();

                    if (!openNodes.Contains(node))
                    {
                        openNodes.Add(node);
                    }
                    else if (tCost >= cost[node])
                    {
                        continue;
                    }

                    if (!cameFrom.ContainsKey(node))
                    {
                        cameFrom.Add(node, currentNode);
                        cost.Add(node, tCost);
                        estCost.Add(node, cost[node] + (node - goal).Length());
                    }
                    cameFrom[node] = currentNode;
                    cost[node] = tCost;
                    estCost[node] = cost[node] + (node - goal).Length();
                }
            }
            return unit.GetPosition();
        }

        public void SwapPosition(IUnit unit,IUnit target)
        {
            int ux = (int)unit.GetPosition().X;
            int tx = (int)target.GetPosition().X;
            int uy = (int)unit.GetPosition().Y;
            int ty = (int)target.GetPosition().Y;

            unit.ChangePosition(tx, ty);
            target.ChangePosition(ux, uy);
            GetTile(ux, uy).unit = target;
            GetTile(tx, ty).unit = unit;
        }
    }
}
