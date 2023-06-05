using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace ItemSystem.Generators
{
    [Serializable]
    public class ItemRandomList<T>
    {
        [Serializable]
        public class Pair
        {
            public T Item;
            public float Weight;

            public Pair(T item, float weight)
            {
                Item = item;
                Weight = weight;
            }
        }

        public List<Pair> List = new();

        public int Count => List.Count;

        public void Add(T item, float weight)
        {
            List.Add(new Pair(item, weight));
        }

        public T GetRandom()
        {
            float totalWeight = 0;

            foreach (Pair p in List)
            {
                totalWeight += p.Weight;
            }
            
            var value = Random.Range(0, totalWeight);
            float sumWeight = 0;

            foreach (Pair pair in List)
            {
                sumWeight += pair.Weight;

                if (sumWeight >= value)
                {
                    return pair.Item;
                }
            }

            return default;
        }
    }
}
