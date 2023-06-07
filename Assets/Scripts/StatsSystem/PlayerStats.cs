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
            private set
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

        [FormerlySerializedAs("MaxHitPoints")] public float maxHitPoints;
        [FormerlySerializedAs("MaxManaPool")] public float maxManaPool;
        public float spellCastCost;
        public float spellDamage;

        /*public static event HealthPointsChangedDelegate OnHealthPointsChanged;
        public delegate void HealthPointsChangedDelegate(float health);
        public delegate void ManaPointsChangedDelegate(float health);
        public static event ManaPointsChangedDelegate OnManaPointsChanged;
        public delegate void GoldValueChangedDelegate(int goldValue);
        public static event GoldValueChangedDelegate OnGoldAmountChanged;
        */
        public static event Action<float> OnHealthPointsChanged;
        public static event Action<float> OnManaPointsChanged;
        public static event Action<int> OnGoldAmountChanged;

    }
}