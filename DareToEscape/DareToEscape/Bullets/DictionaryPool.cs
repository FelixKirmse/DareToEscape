using System.Collections.Generic;
using BlackDragonEngine.Helpers;

namespace DareToEscape.Bullets
{
    internal static class DictionaryPool
    {
        private static readonly Dictionary<Dictionary<string, AnimationStripStruct>, Dictionary<string, AnimationStripStruct>> UsedDictionaries = new Dictionary<Dictionary<string, AnimationStripStruct>, Dictionary<string, AnimationStripStruct>>(5000);
        private static readonly Stack<Dictionary<string, AnimationStripStruct>> UnusedDictionaries = new Stack<Dictionary<string, AnimationStripStruct>>(1000); 

        public static Dictionary<string, AnimationStripStruct> GetDictionary()
        {
            lock(UsedDictionaries)
            lock(UnusedDictionaries)
            {
                Dictionary<string, AnimationStripStruct> dict = UnusedDictionaries.Count > 0
                                                                    ? UnusedDictionaries.Pop()
                                                                    : new Dictionary<string, AnimationStripStruct>();
                UsedDictionaries.Add(dict, dict);
                return dict;
            }
        }

        public static void SetInactive(Dictionary<string, AnimationStripStruct> dict)
        {
            lock(UsedDictionaries)
            lock(UnusedDictionaries)
            {
                dict.Clear();
                UsedDictionaries.Remove(dict);
                UnusedDictionaries.Push(dict);
            }
        }
    }
}
