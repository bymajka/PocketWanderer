using UnityEngine;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class ArmorData : ItemData
    {
        /// <summary>
        /// A value indicating how many defense points the armor adds.
        /// </summary>
        public float ArmorRating;
    }
}