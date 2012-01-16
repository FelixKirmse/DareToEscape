using System.Collections.Generic;
using BlackDragonEngine.TileEngine;
using DareToEscape;

namespace MapEditor
{
    internal struct Item
    {
        public bool AddToExisting;
        public List<TileCode> Codes;
        public List<Coords> ExtraCells;
        public List<Item> ExtraItems;
        public bool IsTurret;
        public bool? Passable;
        public int? TileID;
        public bool Unique;

        public static Item GetItemByTileId(int id)
        {
            switch (id)
            {
                case 80:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "YELLOW")}
                               };

                case 81:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "RED")}
                               };

                case 82:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "MAGENTA")}
                               };
                case 83:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "CYAN")}
                               };

                case 96:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "YELLOW")}
                               };

                case 97:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "RED")}
                               };

                case 98:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "MAGENTA")}
                               };
                case 99:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "CYAN")}
                               };

                default:
                    return new Item
                               {
                                   TileID = id,
                                   Passable = false
                               };
            }
        }

        public static Item GetItemByEntityId(int id)
        {
            switch (id)
            {
                case 0:
                    return new Item
                               {
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Start)},
                                   Unique = true
                               };
                case 1:
                    return new Item
                               {
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Spawn, "SmallTurret_")},
                                   IsTurret = true
                               };
                case 2:
                    return new Item
                               {
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Spawn, "MediumTurret_")},
                                   IsTurret = true
                               };
                case 3:
                    return new Item
                               {
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Spawn, "Boss1_")},
                                   IsTurret = true
                               };

                case 4:
                    return new Item
                               {
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Exit)}
                               };
                case 5:
                    return new Item
                               {
                                   Codes =
                                       new List<TileCode>
                                           {new TileCode(TileCodes.Checkpoint), new TileCode(TileCodes.Save)},
                                   ExtraCells = new List<Coords> {new Coords(0, 1)},
                                   ExtraItems =
                                       new List<Item>
                                           {new Item {Codes = new List<TileCode> {new TileCode(TileCodes.Save)}}}
                               };


                default:
                    return new Item();
            }
        }

        public static Item GetItemByCodeId(int id)
        {
            switch (id)
            {
                case 0:
                    return new Item
                               {
                                   AddToExisting = true,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Trigger, "BOSS")}
                               };
                default:
                    return new Item();
            }
        }
    }
}