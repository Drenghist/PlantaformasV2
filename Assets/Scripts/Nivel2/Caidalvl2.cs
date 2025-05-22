using UnityEngine;

public class Caidalvl2: MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<GameManagerlvl2>().MataInstant();
    }
}
