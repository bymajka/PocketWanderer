using StatsSystem;
using UnityEngine;

namespace Entity.Behaviour
{
    public class EnemyEntityBehaviour : BaseEntityBehaviour
    {
        [SerializeField] private EnemyStats statsData;
        public EnemyStats Stats { get; private set; }

        public override void Initialize()
        {
            Stats = Instantiate(statsData);
            base.Initialize();
        }
    }
}