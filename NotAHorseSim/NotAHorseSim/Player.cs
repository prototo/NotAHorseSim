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
        public Vector2 position;

        public bool active;
        public byte health;

        public void Initialize(Texture2D playerTexture, Vector2 playerPosition)
        {
            texture = playerTexture;
            position = playerPosition;

            active = true;
            health = 100;
        }

        public void Update(Map map)
        {
            Random rand = new Random();
            int x = (int) position.X + rand.Next(-1, 2);
            int y = (int) position.Y + rand.Next(-1, 2);

            if (!map.isOccupied(x, y))
            {
                position.X = x;
                position.Y = y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 draw_position = new Vector2(position.X * Map.TILESIZE, position.Y * Map.TILESIZE);
            spriteBatch.Draw(texture, draw_position);
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
