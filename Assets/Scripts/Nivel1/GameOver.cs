using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    
    public Image fadeImage;                            // Imagen negra para el fade
    public float transitionDuration = 3f;              // Duración de la transición

    private bool isTransitioning = false;

    // Llamar este método cuando el jugador muera
    public void TriggerGameOver()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToGameOver("GameOverScene"));
        }
    }

    public void TriggerResults()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToGameOver("SceneResultado"));
        }
    }

    public void TriggerNivel2()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToGameOver("Nivel2"));
        }
    }

    private IEnumerator TransitionToGameOver(string escena)
    {
        isTransitioning = true;

        // Asegúrate de que el alpha inicial sea 0
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        float t = 0f;
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float alpha = t / transitionDuration;
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(escena);
    }
}