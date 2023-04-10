using UnityEngine;
using UnityEngine.Serialization;

namespace InventorySystem
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        [FormerlySerializedAs("DisplayName")] public string displayName;
        [FormerlySerializedAs("Icon")] public Sprite icon;
    }
}