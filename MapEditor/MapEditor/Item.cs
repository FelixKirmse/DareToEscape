using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Entities;
using DareToEscape;
using Microsoft.Xna.Framework;

namespace MapEditor
{
    internal struct Item
    {
        public int? TileID;
        public List<TileCode> Codes;
        public bool? Passable;
        public bool Unique;
        public bool AddToExisting;

        public static Item GetItemByTileId(int id)
        {
            switch(id)
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
            throw new NotImplementedException();
        }

        public void Demo()
        {
            var intList = new List<int>(); //The data is saved in the Heap, the reference in the stack is assigend to intList
            PopulateList(intList); //PopulateList is called with the reference that intList holds
            Console.WriteLine(intList); //intList ist still the same reference, but the Data in the Heap is now modified
        }

        public void PopulateList(List<int> list) //the new variable list contains a reference to Data in the Heap
        {
            for(int i = 0; i < 100; ++i)
            {
                list.Add(i); //This data is now accessed and modified
            }
        }
    }
}
