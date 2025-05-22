using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuestraTiempo : MonoBehaviour
{
    private bool hasSwitched = false;
    public TextMeshProUGUI tiempoTextolvl1;
    public TextMeshProUGUI tiempoTextolvl2;
    public TextMeshProUGUI tiempoVida;
    public TextMeshProUGUI tiempoTotal;
    private string sceneToLoad = "Portada";

    void Start()
    {

        tiempoTextolvl1.text = FormateaTiempo(FindAnyObjectByType<Cronometro>().tiempo);
        tiempoTextolvl2.text = FormateaTiempo(FindAnyObjectByType<Cronometrolvl2>().tiempo);

        Destroy(GameObject.FindGameObjectWithTag("GestorDeNivel")); 
        Destroy(GameObject.FindGameObjectWithTag("GestorDeNivel2"));


        //Lógica para sumar la penalización de la vida
        int vida = FindAnyObjectByType<GameManagerlvl2>().vida;
        tiempoVida.text = FormateaTiempo(5 - vida);
        tiempoTotal.text = FormateaTiempo(FindAnyObjectByType<Cronometro>().tiempo + FindAnyObjectByType<Cronometrolvl2>().tiempo + (5 - vida));

        //Con esta función muevo el tiempo del cronómetro al objeto repo de datos
        GameObject.FindGameObjectWithTag("Record").GetComponent<Portada>().GrabaRecord(FindAnyObjectByType<Cronometro>().tiempo + FindAnyObjectByType<Cronometrolvl2>().tiempo + (5 - vida));
    }



    private string FormateaTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int decimas = Mathf.FloorToInt((tiempo *10 ) % 10);
        return string.Format("{0:00}:{1:00}.{2}", minutos, segundos, decimas);

    }

    void Update()
    {
        if (!hasSwitched && Input.anyKeyDown)
        {
            hasSwitched = true; // Evita multiples cargas
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
