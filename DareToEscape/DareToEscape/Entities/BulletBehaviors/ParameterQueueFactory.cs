using System.Collections.Generic;

namespace DareToEscape.Entities.BulletBehaviors
{
    static class ParameterQueueFactory
    {
        private static readonly Dictionary<int, ParameterQueue> ActivePqs = new Dictionary<int, ParameterQueue>(50000);
        private static readonly Queue<ParameterQueue> InActivePqs = new Queue<ParameterQueue>();
        private static int _idCounter;

        public static ParameterQueue CreateNew()
        {
            lock(ActivePqs)
            {
                var pq = InActivePqs.Count > 0 ? InActivePqs.Dequeue() : new ParameterQueue(_idCounter++);
                ActivePqs.Add(pq.ID, pq);
                return pq;
            }
        }

        public static void SetInactive(ParameterQueue pq)
        {
            lock(ActivePqs)
            {
                ActivePqs.Remove(pq.ID);
                InActivePqs.Enqueue(pq);
            }
        }
    }
}
