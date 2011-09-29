using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackDragonEngine.Providers;

namespace DareToEscape.Editor
{
    static class MapEditor
    {
        public static void Activate()
        {
            DareToEscape.ChangeResolution(ShortcutProvider.GetMaximumScreenSizePrimary());
            DareToEscape.ToggleFullScreen();
        }
    }
}
