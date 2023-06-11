using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointScript : MonoBehaviour
{
    private Collider2D _collider2D;
    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
