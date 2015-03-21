using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NotAHorseSim
{
    class Map
    {
        public const byte TILESIZE = 16;

        public int tiles_x;
        public int tiles_y;

        public Texture2D grass;

        public void Initialize(ContentManager content, int x, int y)
        {
            grass = content.Load<Texture2D>("Graphics/grass");
            tiles_x = x / TILESIZE;
            tiles_y = y / TILESIZE;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    Vector2 position = new Vector2(x * TILESIZE, y * TILESIZE);
                    spriteBatch.Draw(grass, position);
                }
            }
        }
    }
}
