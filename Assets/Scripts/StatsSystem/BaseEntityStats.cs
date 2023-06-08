using UnityEngine;

namespace StatsSystem
{
	public abstract class BaseEntityStats : ScriptableObject
	{
		public float HitPoints;
		public float Armor;
		public float Damage;
		public float WeaponDamage;
		public float MovementSpeed;
		public float AttackPointDistance;
		public float MaxDamage;
		public float MaxHitPoints;
	}
}