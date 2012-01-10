using System;
using System.Collections.Generic;
using System.Linq;
using BlackDragonEngine.TileEngine;
using DareToEscape;

namespace MapEditor
{
    internal struct Item
    {
        public bool AddToExisting;
        public List<TileCode> Codes;
        public bool? Passable;
        public int? TileID;
        public bool Unique;
        public bool IsTurret;
        public List<Coords> ExtraCells;
        public List<Item> ExtraItems; 

        public static Item GetItemByTileId(int id)
        {
            switch (id)
            {
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
            switch(id)
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
                                   Codes = new List<TileCode>{new TileCode(TileCodes.Spawn, "SmallTurret_")},
                                   IsTurret = true
                               };
                case 2:
                    return new Item
                               {
                                   Codes = new List<TileCode>{new TileCode(TileCodes.Spawn, "MediumTurret_")},
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
                                   Codes = new List<TileCode>{new TileCode(TileCodes.Exit)}
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
            switch(id)
            {
                case 0:
                    return new Item
                               {
                                   AddToExisting = true,
                                   Codes = new List<TileCode> {new TileCode(TileCodes.Trigger, "Boss")}
                               };
                default:
                    return new Item();
            }
        }
    }
}