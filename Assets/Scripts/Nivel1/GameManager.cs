using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI vidaText;
    public int vida;
    private bool canVidaActivate = true;
    private float cooldownVidaTime = 3f;
    private bool death;
    


    void Start()
    {
        vida = 5;
        vidaText.text = vida.ToString();
        FindAnyObjectByType<Cronometro>().EmpezarCronometro();
        death = false;
    }

    void Update()
    {
        if (death)
        {
            //Debug.Log("Muerte!");
            /*
             * Antigua, tengo que llamar al objeto espec�fico para que no se equivoque
             * FindAnyObjectByType<GameOver>().TriggerGameOver();
             * 
             */
            GameObject.FindGameObjectWithTag("GestorDeNivel").GetComponent<GameOver>().TriggerGameOver(); //Lo �nico que hace es bloquear el personaje y cambiar la escena
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
                FindAnyObjectByType<HeroKnight>().Muerete();
                FindAnyObjectByType<HeroKnight>().aniMuerte(); //Solo ejecuta la animaci�n de la muerte
                StartCoroutine(Cooldown());
                death = true; 
            }
            else
            {
                vidaText.text = vida.ToString();
            FindAnyObjectByType<HeroKnight>().RecibeDa�o();
            StartCoroutine(Cooldown());
            }
                
        }

    }

    public void MataInstant()
    {
        vidaText.text = 0.ToString();
        FindAnyObjectByType<HeroKnight>().Muerete();
        death = true;
    }




    System.Collections.IEnumerator Cooldown()
    {
        canVidaActivate = false;
        yield return new WaitForSeconds(cooldownVidaTime);
        canVidaActivate = true;
        //Debug.Log("�Acci�n disponible de nuevo!");
    }


}
