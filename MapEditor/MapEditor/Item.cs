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
                case 1:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.PushRight)}
                               };
                case 2:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.PushDown)}
                               };
                case 6:
                case 7:
                case 8:
                    goto case 40;

                case 17:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.PushUp)}
                               };
                case 18:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.PushLeft)}
                               };
                case 22:
                case 24:
                case 38:
                case 39:
                case 40:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Deadly)}
                               };
                case 69:
                case 70:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Jumpthrough)},
                                   ExtraCells = new List<Coords> {new Coords(0, -1)},
                                   ExtraItems =
                                       new List<Item>
                                           {
                                               new Item
                                                   {
                                                       Codes =
                                                           new List<TileCode> {new TileCode(TileCodes.JumpthroughTop)}
                                                   }
                                           }
                               };
                case 80:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Key, "YELLOW")},
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
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Lock, "YELLOW")},
                                   Passable = false
                               };

                case 97:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Lock, "RED")},
                                   Passable = false
                               };

                case 98:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Lock, "MAGENTA")},
                                   Passable = false
                               };
                case 99:
                    return new Item
                               {
                                   TileID = id,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Lock, "CYAN")},
                                   Passable = false
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
                                           {
                                               new Item
                                                   {
                                                       Codes = new List<TileCode> {new TileCode(TileCodes.Save)}
                                                   }
                                           }
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