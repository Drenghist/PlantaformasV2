using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;



public class Moneda : MonoBehaviour
{

    public GameObject moneda;


    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<HeroKnight>().GetComponent<HeroKnight>().SonidoMoneda();
        FindAnyObjectByType<EventSystem>().GetComponent<Cronometro>().ReduceTiempo(5f);
        Destroy(moneda);
        
    }




    void Update()
    {
        
    }
}
