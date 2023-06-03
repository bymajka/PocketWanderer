using UnityEngine;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class WeaponData : ItemData
    {
        /// <summary>
        /// Type of weapon, such as sword, bow, etc.
        /// </summary>
        public WeaponType WeaponType;
        
        /// <summary>
        /// A value indicating how many points of damage the weapon does.
        /// </summary>
        public float Damage;
    }
}