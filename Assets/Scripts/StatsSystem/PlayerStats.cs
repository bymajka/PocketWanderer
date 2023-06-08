using System;
using UnityEngine;

namespace StatsSystem
{
    [CreateAssetMenu]
    public class PlayerStats : BaseEntityStats
    {
        private int gold;
        private float manaPoints;
        
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

        public float MaxManaPool;
        public float DefaultSpeed;
        public float spellCastCost;
        public float spellDamage;
        public static event Action<float> OnHealthPointsChanged;
        public static event Action<float> OnManaPointsChanged;
        public static event Action<int> OnGoldAmountChanged;

    }
}