using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotAHorseSim
{
    class MapNode
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Boolean occupied { get; set; }

        public int g { get; set; }
        public int f { get; set; }

        public String identifier { get; private set; }

        public MapNode(int x, int y, Boolean occupied = false)
        {
            this.x = x;
            this.y = y;
            this.occupied = occupied;
            this.identifier = getIdentifier();
        }

        public int[] getCoords()
        {
            return new int[] {x, y};
        }

        private String getIdentifier()
        {
            return String.Format("{0},{1}", x, y);
        }
    }
}
