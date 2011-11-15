using System.Collections.Generic;

namespace DareToEscape.Entities.BulletBehaviors
{
    static class ParameterQueueFactory
    {
        private static readonly Dictionary<int, ParameterQueue> ActivePqs = new Dictionary<int, ParameterQueue>(50000);
        private static readonly Stack<ParameterQueue> InActivePqs = new Stack<ParameterQueue>();
        private static int _idCounter;

        public static ParameterQueue CreateNew()
        {
            lock(ActivePqs)
            {
                lock(InActivePqs)
                {
                    var pq = InActivePqs.Count > 0 ? InActivePqs.Pop() : new ParameterQueue(_idCounter++);
                    ActivePqs.Add(pq.ID, pq);
                    return pq;
                }
            }
        }

        public static void SetInactive(ParameterQueue pq)
        {
            lock(ActivePqs)
            {
                lock(InActivePqs)
                {
                    ActivePqs.Remove(pq.ID);
                    InActivePqs.Push(pq);
                }
            }
        }
    }
}
