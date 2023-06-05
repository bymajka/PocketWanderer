using System.Linq;
using PlayerSystem;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; private set; }
	public GameObject PlayerObject { get; private set; }

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

		PlayerObject = FindObjectOfType<PlayerBehaviour>().gameObject;
	}
}