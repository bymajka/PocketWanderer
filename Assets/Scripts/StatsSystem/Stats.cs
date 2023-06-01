using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.StatsSystem
{
	[CreateAssetMenu]
	public class Stats : ScriptableObject
	{
		[FormerlySerializedAs("HitPoints")] public float HitPoints;

		[FormerlySerializedAs("Armor")] public float Armor;

		[FormerlySerializedAs("Damage")] public float Damage;

		[FormerlySerializedAs("MovementSpeed")] public float MovementSpeed;

		[FormerlySerializedAs("AttackSpeed")] public float AttackSpeed;

		[FormerlySerializedAs("ReloadTime")] public float ReloadTime;
	}
}
