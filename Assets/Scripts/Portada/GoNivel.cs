using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNivel : MonoBehaviour
{
    private bool reiniciar = false; 

    void Start()
    {
        reiniciar = false;
        StartCoroutine(Cooldown());
    }
    
    public void GoNivel1()
    {
        if (reiniciar)
        {
            SceneManager.LoadScene("Nivel1");
        }
        
    }

    public void GoNivelCreditos()
    {
        if (reiniciar)
        {
            SceneManager.LoadScene("Creditos");
        }

    }


    void Update()
    {
        
    }

    System.Collections.IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        reiniciar = true;
    }
}
