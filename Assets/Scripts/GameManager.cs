using Entity.Behaviour;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        var stats = PlayerManager.Instance.PlayerObject.GetComponent<PlayerEntityBehaviour>().Stats;
        stats.manaPoints = stats.maxManaPool;
        stats.HitPoints = stats.MaxHitPoints;
        stats.MovementSpeed = stats.DefaultSpeed;

        foreach (var enemy in FindObjectsOfType<EnemyEntityBehaviour>())
        {
            enemy.Stats.HitPoints = enemy.Stats.MaxHitPoints;
        }
    }
}
