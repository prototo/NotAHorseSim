using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace NotAHorseSim
{
    class Map
    {
        public const byte TILESIZE = 16;

        public int tiles_x;
        public int tiles_y;

        public Texture2D grass;
        public Texture2D mountain;
        public Texture2D mountain_edge;

        public enum TILE {
            NOTHING, GRASS, MOUNTAIN
        };

        protected TILE[,] map;

        public void Initialize(ContentManager content, int x, int y)
        {
            grass = content.Load<Texture2D>("Graphics/grass");
            mountain = content.Load<Texture2D>("Graphics/mountain");
            mountain_edge = content.Load<Texture2D>("Graphics/mountain_edge");
            tiles_x = x / TILESIZE;
            tiles_y = y / TILESIZE;
            generateMap();
        }
        
        public bool isOccupied(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                if (x < tiles_x && y < tiles_y)
                {
                    if (TILE.MOUNTAIN != map[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        protected void generateMap()
        {
            map = new TILE[tiles_x, tiles_y];
            Random random = new Random();
            Array types = Enum.GetValues(typeof(TILE));
            int types_length = types.Length;

            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    // map[x, y] = (TILE)types.GetValue(random.Next(types_length));
                    if (random.Next(15) == 0)
                        map[x, y] = TILE.MOUNTAIN;
                }
            }

            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    int chance = 1;
                    int increment = 1;

                    if (y > 0 && map[x, y - 1] == TILE.MOUNTAIN)
                        chance += increment;
                    if (x < tiles_x - 1 && map[x + 1, y] == TILE.MOUNTAIN)
                        chance += increment;
                    if (y < tiles_y - 1 && map[x, y + 1] == TILE.MOUNTAIN)
                        chance += increment;
                    if (x > 0 && map[x - 1, y] == TILE.MOUNTAIN)
                        chance += increment;

                    if (random.Next(chance) > 0)
                    {
                        map[x, y] = TILE.MOUNTAIN;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    Vector2 position = new Vector2(x * TILESIZE, y * TILESIZE);

                    // general grass coverage for under transparent tiles
                    spriteBatch.Draw(grass, position);

                    switch (map[x, y])
                    {
                        case TILE.NOTHING:
                        case TILE.GRASS:
                            break;
                        case TILE.MOUNTAIN:
                            if (y == tiles_y - 1 || y >= 0 && TILE.MOUNTAIN == map[x, y + 1]) {
                                spriteBatch.Draw(mountain, position);
                            }
                            else
                            {
                                spriteBatch.Draw(mountain_edge, position);
                            }
                            break;
                    }
                }
            }
        }
    }
}
