using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xTile;

namespace BlackDragonEngine.Providers
{
    public static class MapProvider
    {      
        public static Map GetMap(string name)
        {
            Map map = VariableProvider.Game.Content.Load<Map>(@"maps/" + name);
            map.LoadTileSheets(VariableProvider.DisplayDevice);
            return map;
            
        }
        
    }
}
