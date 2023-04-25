using UnityEngine;
using UnityEngine.Serialization;

namespace ItemSystem.ItemsData
{
    [CreateAssetMenu]
    public class WeaponData : ItemData
    {
        // Type of weapon, such as sword, bow, etc.
        [FormerlySerializedAs("WeaponType")]
        public WeaponType weaponType;
        
        // A value indicating how many points of damage the weapon does.
        [FormerlySerializedAs("Damage")]
        public float damage;
    }
}