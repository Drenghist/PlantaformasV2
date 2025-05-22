using UnityEngine;

public class GameManagerlvl2 : MonoBehaviour
{
    public TMPro.TextMeshProUGUI vidaText;
    public int vida;
    private bool canVidaActivate = true;
    private float cooldownVidaTime = 3f;
    private bool death;
    

    void Start()
    {
        vida = FindAnyObjectByType<GameManager>().vida;
        vidaText.text = vida.ToString();
        FindAnyObjectByType<Cronometrolvl2>().EmpezarCronometro();
        death = false;
    }

    void Update()
    {
        if (death)
        {
            //Debug.Log("Muerte!");
            /*
             * Antigua, tengo que llamar al objeto específico para que no se equivoque
             * FindAnyObjectByType<GameOver>().TriggerGameOver();
             * 
             */
            GameObject.FindGameObjectWithTag("GestorDeNivel2").GetComponent<GameOver>().TriggerGameOver(); //Lanza el cambio de escena
        }
    }


    public void QuitaVida()
    {
        if (canVidaActivate)
        {
            vida--;
            if (vida <= 0)
            {
                vidaText.text = vida.ToString();
                FindAnyObjectByType<HeroKnightlvl2>().Muerete();
                FindAnyObjectByType<HeroKnightlvl2>().aniMuerte();
                StartCoroutine(Cooldown());
                death = true;
            }
            else
            {
                vidaText.text = vida.ToString();
            FindAnyObjectByType<HeroKnightlvl2>().RecibeDaño();
            StartCoroutine(Cooldown());
            }
                
        }

    }

    public void MataInstant()
    {
        vidaText.text = 0.ToString();
        FindAnyObjectByType<HeroKnightlvl2>().Muerete();
        death = true;
    }




    System.Collections.IEnumerator Cooldown()
    {
        canVidaActivate = false;
        yield return new WaitForSeconds(cooldownVidaTime);
        canVidaActivate = true;
    }


}
