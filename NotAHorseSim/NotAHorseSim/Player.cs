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

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
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
