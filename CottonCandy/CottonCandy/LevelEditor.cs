using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CottonCandy.Enemies;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace CottonCandy
{
    class LevelEditor
    {

        private static List<IUnit> enemies;
        public static int selected = -1;

        public static void Init()
        {
            Globals.screenW -= 320;
            enemies = new List<IUnit>();
            IUnit e = null;
            int i = 0;
            do
            {
                e = GetEnemyAt(i);
                i++;
                if (e != null)
                {
                    enemies.Add(e);
                }
            } while (e != null);
        }

        public static void Update(bool Lclicked, bool Rclicked)
        {
            Tilegrid grid = Globals.grid;

            Hotkey h = new Hotkey(true).add(Keys.O);

            if (h.IsDown())
            {
                SaveMap("Level1");
            }

            if (!CottonCandy.graphics.GraphicsDevice.Viewport.Bounds.Contains(Globals.mouse))
            {
                return;
            }

            Vector2 mousePos = new Vector2(Globals.mouse.X + Camera.camX, Globals.mouse.Y + Camera.camY);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235)
            {
                if (new Rectangle(Globals.screenW - 350 + Globals.tileW * 2, Globals.screenH - 140, 350, 140).Contains(mousePos - new Vector2(Camera.camX, Camera.camY)))
                {
                    Console.WriteLine("Saved!");
                    SaveMap("Level1");
                }
            }
            if (Lclicked && mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235)
            {
                if (new Rectangle(Globals.screenW - 350 + Globals.tileW * 2, Globals.screenH - 280, 350, 140).Contains(mousePos - new Vector2(Camera.camX, Camera.camY)))
                {
                    Globals.grid.reward--;
                }
            }
            if (Rclicked && mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235)
            {
                if (new Rectangle(Globals.screenW - 350 + Globals.tileW * 2, Globals.screenH - 280, 350, 140).Contains(mousePos - new Vector2(Camera.camX, Camera.camY)))
                {
                    Globals.grid.reward++;
                }
            }
            if (Lclicked && mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235)
            {
                if (new Rectangle(Globals.screenW - 350 + Globals.tileW * 2, Globals.screenH - 420, 350, 140).Contains(mousePos - new Vector2(Camera.camX, Camera.camY)))
                {
                    Globals.grid.condition--;
                }
            }
            if (Rclicked && mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235)
            {
                if (new Rectangle(Globals.screenW - 350 + Globals.tileW * 2, Globals.screenH - 420, 350, 140).Contains(mousePos - new Vector2(Camera.camX, Camera.camY)))
                {
                    Globals.grid.condition++;
                }
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && mousePos.X - Camera.camX >= Globals.screenW && mousePos.X - Camera.camX <= Globals.screenW + Globals.tileW * 2)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        if (new Rectangle(Globals.screenW + Globals.tileW + Camera.camX, (int)(i / 2) * Globals.tileH + Camera.camY, Globals.tileW, Globals.tileH).Contains(mousePos))
                        {
                            selected = i;
                            return;
                        }
                    }
                    else
                    {
                        if (new Rectangle(Globals.screenW + Camera.camX, (int)(i / 2) * Globals.tileH + Camera.camY, Globals.tileW, Globals.tileH).Contains(mousePos))
                        {
                            selected = i;
                            return;
                        }
                    }
                }
            }
            if (!(mousePos.X - Camera.camX >= Globals.screenW + 60 && mousePos.X - Camera.camX <= Globals.screenW + 235))
            {

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && selected == -1)
                {
                    if ((int)(mousePos.X / Globals.tileW) >= Globals.grid.w) return;
                    if (grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).colidable != TileType.NOTSOLID)
                    {
                        grid.SetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH), new Tile("Tiles/Ground", (int)(mousePos.X / Globals.tileW) * Globals.tileW, (int)(mousePos.Y / Globals.tileH) * Globals.tileH, TileType.NOTSOLID));
                    }
                }
                else if (Mouse.GetState().MiddleButton == ButtonState.Pressed && selected == -1)
                {
                    if ((int)(mousePos.X / Globals.tileW) >= Globals.grid.w) return;
                    if (grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).colidable != TileType.FINISH)
                    {
                        grid.SetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH), new Tile("Tiles/GoalTile", (int)(mousePos.X / Globals.tileW) * Globals.tileW, (int)(mousePos.Y / Globals.tileH) * Globals.tileH, TileType.FINISH));
                    }

                }
                else if (Mouse.GetState().RightButton == ButtonState.Pressed && selected == -1)
                {
                    if ((int)(mousePos.X / Globals.tileW) >= Globals.grid.w) return;
                    if (grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).colidable != TileType.SOLID)
                    {
                        grid.SetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH), new Tile("Tiles/Wall", (int)(mousePos.X / Globals.tileW) * Globals.tileW, (int)(mousePos.Y / Globals.tileH) * Globals.tileH, TileType.SOLID));
                    }
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Pressed && selected != -1)
                {
                    if ((int)(mousePos.X / Globals.tileW) >= Globals.grid.w) return;
                    if (GetEnemyAt(selected) == Globals.CurrentPlayer)
                    {
                        grid.p = Globals.CurrentPlayer;
                        grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit = GetEnemyAt(selected);
                        grid.units.Add(grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit);
                        grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit.ChangePosition((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH));
                    }
                    else
                    {
                        grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit = GetEnemyAt(selected);
                        grid.units.Add(grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit);
                        grid.GetTile((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH)).unit.ChangePosition((int)(mousePos.X / Globals.tileW), (int)(mousePos.Y / Globals.tileH));
                    }
                }
                else if ((Mouse.GetState().RightButton == ButtonState.Pressed || Mouse.GetState().MiddleButton == ButtonState.Pressed) && selected != -1)
                {
                    selected = -1;
                }
            }
        }

        public static void Draw()
        {
            Globals.sb.Draw(LoadTex.LoadTexture("UIElements/LevelEditorBaseUI"), new Vector2(148, 0), Color.White);
            Globals.sb.Draw(LoadTex.LoadTexture("UIElements/SaveButton"), new Vector2(Globals.screenW + 60, Globals.screenH - 100), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            Globals.sb.DrawString(Globals.bigfont, "Rew:" + Globals.grid.reward, new Vector2(Globals.screenW + 60, Globals.screenH - 240), Color.White);
            Globals.sb.DrawString(Globals.bigfont, "Con:" + Globals.grid.condition, new Vector2(Globals.screenW + 60, Globals.screenH - 380), Color.White);
            for (int i = 0; i < enemies.Count; i++)
            {
                IUnit e = enemies[i];
                if (i % 2 == 1)
                {
                    Globals.sb.Draw(LoadTex.LoadTexture(e.GetTextureName()), new Vector2(Globals.screenW + Globals.tileW, (int)(i / 2) * Globals.tileH), Color.White);
                }
                else
                {
                    Globals.sb.Draw(LoadTex.LoadTexture(e.GetTextureName()), new Vector2(Globals.screenW, (int)(i / 2) * Globals.tileH), Color.White);
                }
            }
        }

        public static IUnit GetEnemyAt(int i)
        {
            switch (i)
            {
                case (0):
                    return Globals.CurrentPlayer;
                case (1):
                    return new Chicken();
                case (2):
                    return new Snake();
                case (3):
                    return new Plant();
                case (4):
                    return new Angel();
                default:
                    return null;
            }
        }

        public static void SaveMap(string name)
        {
            Globals.grid.units.Clear();
            for (int i = 0; i < Globals.grid.w; i++)
            {
                for (int j = 0; j < Globals.grid.h; j++)
                {
                    if (Globals.grid.grid[i, j].unit != null)
                    {
                        Globals.grid.units.Add(Globals.grid.grid[i, j].unit);
                    }
                }
            }
            FileLoader.Write(Globals.grid, "Levels/LevelX.dat");
            FileLoader.WriteText("[STATUS " + DateTime.Now.ToLongTimeString() + " at LevelEditor.cs->SaveMap()] Saved map!", "log.txt");


        }

        public static void LoadLevel(string name)
        {
            FileStream fs = new FileStream("Levels/" + name + ".dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                Globals.grid = (Tilegrid)formatter.Deserialize(fs);
                if (Globals.grid.p != null)
                {
                    for (int i = Globals.grid.units.Count - 1; i >= 0; i--)
                    {
                        IUnit u = Globals.grid.units[i];
                        if (u is Player)
                        {
                            Globals.grid.RemoveUnit(u, ((Player)u).X, ((Player)u).Y);
                        }
                    }
                    for (int i = 0; i < Globals.grid.w; i++)
                    {
                        for (int j = 0; j < Globals.grid.h; j++)
                        {
                            if(Globals.grid.grid[i, j].unit is Player)
                            {
                                Globals.grid.grid[i, j].unit = null;
                            }
                        }
                    }
                    //Globals.grid.units.Remove(Globals.grid.p);
                    Globals.grid.units.Add(Globals.CurrentPlayer);
                    Globals.CurrentPlayer.ChangePosition(Globals.grid.p.X, Globals.grid.p.Y);
                    Globals.grid.p = Globals.CurrentPlayer;
                }
            }
            catch (SerializationException e)
            {
                FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at LevelEditor.cs->SaveMap()] Failed to deserialize. Reason:" + e.Message, "log.txt");
            }
            finally
            {
                fs.Close();
            }
        }

    }
}
