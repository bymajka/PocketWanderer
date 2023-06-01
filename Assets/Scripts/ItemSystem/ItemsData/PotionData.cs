using UnityEngine;
using UnityEngine.Serialization;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class PotionData : ItemData
    {
        // The effect of potion, like speed boost or etc.
        [FormerlySerializedAs("PotionEffect")]
        public PotionEffect potionEffect;
        
        // A value indicating how strong the effect is.
        [FormerlySerializedAs("EffectValue")]
        public float effectValue;
    }
}