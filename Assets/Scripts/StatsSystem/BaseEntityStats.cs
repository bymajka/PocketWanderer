using UnityEngine;
using UnityEngine.Serialization;

namespace StatsSystem
{
	public abstract class BaseEntityStats : ScriptableObject
	{
		public float HitPoints;
		public float Armor;
		public float Damage;
		public float MovementSpeed;
		public float AttackPointRadius;
	}
}