using UnityEngine;
using UnityEngine.Serialization;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class ArmorData : ItemData
    {
        // A value indicating how many defense points the armor adds.
        [FormerlySerializedAs("ArmorRating")]
        public float armorRating;
    }
}