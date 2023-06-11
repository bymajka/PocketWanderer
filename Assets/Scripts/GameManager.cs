using EnemyController;
using Entity.Behaviour;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        var stats = PlayerManager
            .Instance
            .PlayerObject
            .GetComponent<PlayerEntityBehaviour>()
            .Stats;

        stats.ManaPoints = stats.MaxManaPool;
        stats.HitPoints = stats.MaxHitPoints;
        stats.MovementSpeed = stats.DefaultSpeed;
        stats.Damage = stats.MaxDamage;
        stats.Gold = stats.Gold;

        foreach (var enemy in FindObjectsOfType<EnemyEntityBehaviour>())
        {
            enemy.Stats.HitPoints = enemy.Stats.MaxHitPoints;
            enemy.Stats.Damage = enemy.Stats.MaxDamage;
            enemy.GetComponent<EnemyStateMachine>().Target = PlayerManager.Instance.transform;
        }
    }
}
