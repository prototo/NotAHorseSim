using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotAHorseSim
{
    class MapNodeComparer: IComparer<MapNode>
    {

        public int Compare(MapNode a, MapNode b)
        {
            int ad = a.g + a.f;
            int bd = b.g + b.f;

            if (ad > bd) return 1;
            if (bd > ad) return -1;
            return 0;
        }
    }
}
