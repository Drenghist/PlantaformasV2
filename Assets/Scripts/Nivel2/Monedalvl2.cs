using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;



public class Monedalvl2 : MonoBehaviour
{

    public GameObject moneda;


    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<HeroKnightlvl2>().SonidoMoneda();
        FindAnyObjectByType<Cronometrolvl2>().ReduceTiempo(5f);
        Destroy(moneda);
        
    }




    void Update()
    {
        
    }
}
