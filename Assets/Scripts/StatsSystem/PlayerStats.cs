using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace StatsSystem
{
    [CreateAssetMenu]
    public class PlayerStats : BaseEntityStats
    {
        public int gold;
        public float manaPoints;
        public int Gold
        {
            get => gold;
            set
            {
                gold = value;
                OnGoldAmountChanged?.Invoke(gold);
            }
        }
        public float ManaPoints
        {
            get => manaPoints;
            set
            {
                manaPoints = value;
                OnManaPointsChanged?.Invoke(manaPoints);
            }
        }
        public new float HitPoints
        {
            get => base.HitPoints;
            set
            {
                base.HitPoints = value;
                OnHealthPointsChanged?.Invoke(HitPoints);
            }
        }

        [FormerlySerializedAs("MaxManaPool")] public float maxManaPool;
        public float DefaultSpeed;
        public float spellCastCost;
        public float spellDamage;
        public static event Action<float> OnHealthPointsChanged;
        public static event Action<float> OnManaPointsChanged;
        public static event Action<int> OnGoldAmountChanged;

    }
}