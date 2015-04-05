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

        private Color path_color;
        private Random random;

        public Player(Texture2D playerTexture, MapNode playerPosition, int seed)
        {
            texture = playerTexture;
            position = playerPosition;

            active = true;
            health = 100;

            random = new Random(seed);
            path_color = new Color(random.Next(200) + 50, 100, random.Next(200) + 50);
        }

        public void Update(Map map)
        {
            while (path == null || path.Count == 0)
            {
                int dx = random.Next(map.tiles_x);
                int dy = random.Next(map.tiles_y);

                path = map.getPath(position, map.getNode(dx, dy));
            }
            position = path.Pop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 draw_position = new Vector2(position.x * Map.TILESIZE, position.y * Map.TILESIZE);
            spriteBatch.Draw(texture, draw_position, path_color);

            if (false && path != null && path.Count > 0)
            {
                foreach (MapNode node in path)
                {
                    Vector2 dp = new Vector2(node.x * Map.TILESIZE, node.y * Map.TILESIZE);
                    spriteBatch.Draw(texture, dp, path_color);
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
