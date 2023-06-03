using UnityEngine;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class PotionData : ItemData
    {
        /// <summary>
        /// The effect of potion, like speed boost or etc.
        /// </summary>
        public PotionEffect PotionEffect;
        
        /// <summary>
        /// A value indicating how strong the effect is.
        /// </summary>
        public float EffectValue;
    }
}