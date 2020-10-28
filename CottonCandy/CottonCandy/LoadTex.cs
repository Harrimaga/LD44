using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace CottonCandy
{

    class Tex
    {

        public string name;
        public Texture2D tex;

        public Tex(string name, Texture2D tex)
        {
            this.name = name;
            this.tex = tex;
        }
    }

    class LoadTex
    {

        private static readonly List<Tex> texs = new List<Tex>();

        public static Texture2D LoadTexture(string name)
        {
            foreach (Tex t in texs)
            {
                if (t.name.Equals(name))
                {
                    return t.tex;
                }
            }

            try
            {
                texs.Add(new Tex(name, Globals.contentmanager.Load<Texture2D>(name)));
            }
            catch (Exception)
            {
                FileLoader.WriteText("[ERROR " + DateTime.Now.ToLongTimeString() + " at LoadTex.cs->loadTex()] Texture " + name + " couldn't be loaded:", "log.txt");
                texs.Add(new Tex(name, LoadTexture("Enemies/BaseEnemy")));

            }

            return texs[texs.Count - 1].tex;
        }

    }
}
