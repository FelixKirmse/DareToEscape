using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.HelpMaps
{
    [Serializable]
    public class MapSquare
    {
        #region Declarations        
        public List<string> Codes = new List<string>();
        public bool Passable = true;
        #endregion

        #region Constructor
        private MapSquare()
        { 
        }

        public MapSquare(bool passable)
        {                      
            Passable = passable;
        }
        #endregion

        #region Public Methods
        public void TogglePassable() {
            Passable = !Passable;
        }
        #endregion
    }
}
