using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverPortada: MonoBehaviour
{

    [Tooltip("Nombre de la escena a cargar al presionar cualquier tecla.")]
    
    public string sceneToLoad = "Portada";
    private bool hasSwitched = false;

    void Update()
    {
        if (!hasSwitched && Input.anyKeyDown)
        {
            hasSwitched = true; // Evita múltiples cargas
            Destroy(GameObject.FindGameObjectWithTag("GestorDeNivel"));
            SceneManager.LoadScene(sceneToLoad);
            
        }
    }
}