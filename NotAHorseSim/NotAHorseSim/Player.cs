using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NotAHorseSim
{
    class Player
    {
        public Texture2D texture;
        public MapNode position;
        public Stack<MapNode> path;

        public bool active;
        public byte health;

        public void Initialize(Texture2D playerTexture, MapNode playerPosition)
        {
            texture = playerTexture;
            position = playerPosition;

            active = true;
            health = 100;
        }

        public void Update(Map map)
        {
            while (path == null || path.Count == 0)
            {
                Random rand = new Random();

                int dx = rand.Next(map.tiles_x);
                int dy = rand.Next(map.tiles_y);

                path = map.getPath(position, map.getNode(dx, dy));
            }

            if (path != null && path.Count > 0)
            {
                position = path.Pop();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 draw_position = new Vector2(position.x * Map.TILESIZE, position.y * Map.TILESIZE);
            spriteBatch.Draw(texture, draw_position);

            if (path != null && path.Count > 0)
            {
                foreach (MapNode node in path)
                {
                    Vector2 dp = new Vector2(node.x * Map.TILESIZE, node.y * Map.TILESIZE);
                    Color c = new Color();
                    c.R = 0;
                    c.G = 0;
                    c.B = 255;
                    spriteBatch.Draw(texture, dp, c);
                }
            }
        }

        public int getWidth()
        {
            return texture.Width;
        }

        public int getHeight()
        {
            return texture.Height;
        }
    }
}
