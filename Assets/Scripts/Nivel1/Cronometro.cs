using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI tiempoTexto;
    public float tiempo;
    private bool contando = true;
    private bool canTiempoActivate = true;
    private float cooldownTiempoTime = 0.1f;

    void Update()
    {
        if (contando)
        {
            tiempo += Time.deltaTime;

            int minutos = Mathf.FloorToInt(tiempo / 60f);
            int segundos = Mathf.FloorToInt(tiempo % 60f);
            int decimas = Mathf.FloorToInt((tiempo * 10) % 10);

            tiempoTexto.text = string.Format("{0:00}:{1:00}.{2}", minutos, segundos, decimas);
        }
    }

    public void EmpezarCronometro()
    {
        contando = true;
    }

    public void PararCronometro()
    {
        contando = false;
    }

    public void ReiniciarCronometro()
    {
        tiempo = 0f;
        contando = true;
    }

    public void ReduceTiempo(float tiempoReduc)
    {
        if (canTiempoActivate)
        {
            if (tiempo - tiempoReduc < 0 )
            {
                tiempo = 0f;
            }
            else
            {
                tiempo -= tiempoReduc;
            }
            StartCoroutine(Cooldown());
        }
            
            
    }

    System.Collections.IEnumerator Cooldown()
    {
        canTiempoActivate = false;
        yield return new WaitForSeconds(cooldownTiempoTime);
        canTiempoActivate = true;

    }

    //Mantenemos activo el Cronómetro para poder sacarlo en los resultados
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }
}