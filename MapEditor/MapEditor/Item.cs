using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DareToEscape;

namespace MapEditor
{
    internal struct Item
    {
        public int? TileID;
        public List<TileCode> Codes;
        public bool? Passable;
        public bool Unique;
        public bool NoOverwrite;

        public static Item GetItemByTileId(int id)
        {
            throw new NotImplementedException();
        }

        public static Item GetItemByEntityId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
