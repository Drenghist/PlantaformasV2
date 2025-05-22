using UnityEngine;

public class Estacas : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<GameManager>().QuitaVida();
    }
}
