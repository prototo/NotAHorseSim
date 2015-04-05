using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
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

        protected MapNode[,] map;

        public Map(ContentManager content, int x, int y)
        {
            grass = content.Load<Texture2D>("Graphics/grass");
            mountain = content.Load<Texture2D>("Graphics/mountain");
            mountain_edge = content.Load<Texture2D>("Graphics/mountain_edge");
            tiles_x = x / TILESIZE;
            tiles_y = y / TILESIZE;

            generateMap();
        }

        public MapNode getNode(int x, int y)
        {
            if (x < 0 || x >= tiles_x || y < 0 || y >= tiles_y)
            {
                return null;
            }
            return map[x, y];
        }
        
        protected void generateMap()
        {
            map = new MapNode[tiles_x, tiles_y];
            Random random = new Random();

            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    if (random.Next(15) == 0)
                    {
                        map[x, y] = new MapNode(x, y, true);
                    } else {
                        map[x, y] = new MapNode(x, y, false);
                    }
                }
            }

            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    int chance = 1;
                    int increment = 1;

                    if (y > 0 && getNode(x, y - 1) != null && getNode(x, y - 1).occupied)
                        chance += increment;
                    if (x < tiles_x - 1 && getNode(x + 1, y) != null && getNode(x + 1, y).occupied)
                        chance += increment;
                    if (y < tiles_y - 1 && getNode(x, y + 1) != null && getNode(x, y + 1).occupied)
                        chance += increment;
                    if (x > 0 && getNode(x - 1, y) != null && getNode(x - 1, y).occupied)
                        chance += increment;

                    if (random.Next(chance) > 0)
                    {
                        getNode(x, y).occupied = true;
                    }
                }
            }
        }

        private List<MapNode> getAvailableNeighbours(MapNode current)
        {
            MapNode node;
            List<MapNode> neighbours = new List<MapNode>();

            int x = current.x;
            int y = current.y;

            for (int nx = x - 1; nx <= x + 1; nx++)
            {
                for (int ny = y - 1; ny <= y + 1; ny++)
                {
                    if (nx == x && ny == y) {
                        continue;
                    }

                    node = getNode(nx, ny);
                    if (node != null && !node.occupied)
                    {
                        neighbours.Add(node);
                    }
                }
            }

            return neighbours;
        }

        public int getMovementCost(MapNode start, MapNode goal)
        {
            // arbitrary values!
            if (start.x != goal.x && start.y != goal.y)
            {
                return 3;
            }
            return 2;
        }

        public int getDistance(MapNode start, MapNode goal)
        {
            int x = Math.Abs(start.x - goal.x);
            int y = Math.Abs(start.y - goal.y);
            return x + y;
        }

        public Stack<MapNode> getPath(MapNode start, MapNode goal)
        {
            if (goal.occupied)
            {
                return null;
            }

            MapNode[,] came_from = new MapNode[tiles_x, tiles_y];
            Boolean[,] closed = new Boolean[tiles_x, tiles_y];
            List<MapNode> open = new List<MapNode>();

            MapNode current;

            start.g = 0;
            start.f = getDistance(start, goal);

            open.Add(start);

            MapNodeComparer comparer = new MapNodeComparer();
            while (open.Count > 0)
            {
                // open.Sort(comparer);

                current = open[0];
                open.RemoveAt(0);

                if (current.identifier == goal.identifier)
                {
                    Stack<MapNode> path = new Stack<MapNode>();
                    MapNode from = came_from[current.x, current.y];
                    
                    while (from != null)
                    {
                        Vector2 position = new Vector2(from.x, from.y);

                        path.Push(from);
                        from = came_from[from.x, from.y];
                    }
                    return path;
                }

                closed[current.x, current.y] = true;
                foreach (MapNode neighbour in getAvailableNeighbours(current))
                {
                    if (closed[neighbour.x, neighbour.y])
                    {
                        continue;
                    }

                    int g = current.g + getMovementCost(current, neighbour);
                    int f = getDistance(neighbour, goal);

                    if (!open.Contains(neighbour) || neighbour.g > 0 && g < neighbour.g)
                    {
                        came_from[neighbour.x, neighbour.y] = current;
                        neighbour.g = g;
                        neighbour.f = f;

                        if (!open.Contains(neighbour))
                        {
                            open.Add(neighbour);
                        }
                    }
                }
            }

            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MapNode current;
            Vector2 position;

            for (int x = 0; x < tiles_x; x++)
            {
                for (int y = 0; y < tiles_y; y++)
                {
                    current = getNode(x, y);
                    position = new Vector2(x * TILESIZE, y * TILESIZE);

                    // general grass coverage for under transparent tiles
                    spriteBatch.Draw(grass, position);

                    if (current != null && current.occupied)
                    {
                        MapNode neighbour = getNode(x, y + 1);
                        if (y == tiles_y - 1 || y >= 0 && neighbour != null && neighbour.occupied)
                        {
                            spriteBatch.Draw(mountain, position);
                        }
                        else
                        {
                            spriteBatch.Draw(mountain_edge, position);
                        }
                    }
                }
            }
        }
    }
}
