using UnityEngine;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{

    [Tooltip("Nombre de la escena a cargar al presionar cualquier tecla.")]
    
    public string sceneToLoad = "Nivel1";
    private bool hasSwitched = false;

    void Update()
    {
        if (!hasSwitched && Input.anyKeyDown)
        {
            hasSwitched = true; // Evita múltiples cargas
            Destroy(GameObject.FindGameObjectWithTag("GestorDeNivel"));
            Destroy(GameObject.FindGameObjectWithTag("GestorDeNivel2"));
            SceneManager.LoadScene(sceneToLoad);
            
        }
    }
}