﻿namespace BlackDragonEngine.Helpers
{
    public delegate void SaveEvents();

    public delegate void LoadEvents();

    public class EventHelper
    {
        public event SaveEvents OnSave;
        public event LoadEvents OnLoad;

        public void SaveHelp()
        {
            OnSave();
        }

        public void LoadHelp()
        {
            OnLoad();
        }
    }
}