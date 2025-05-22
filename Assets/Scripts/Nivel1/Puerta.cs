using UnityEngine;

public class Puerta : MonoBehaviour
{
    public SpriteRenderer targetRenderer;  // El renderer que mostrará el sprite
    public Sprite[] animationFrames;       // Lista de sprites (frames de animación)
    private int currentFrame = 0;
    private AudioSource audioSource;
    public AudioClip sonidoPuerta;
    private bool abierta = false;

    void Start()
    {
        //creo el audioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (animationFrames.Length == 0 || targetRenderer == null)
            return;



        if (sonidoPuerta != null && audioSource != null && abierta == false)
        {
            audioSource.PlayOneShot(sonidoPuerta);
            abierta = true;
        }

        // Avanza al siguiente frame
        currentFrame = (currentFrame + 1) % animationFrames.Length;
        targetRenderer.sprite = animationFrames[currentFrame];

        //Paramos cronometro
        FindAnyObjectByType<Cronometro>().PararCronometro();
        //paralizamos el keko
        FindAnyObjectByType<HeroKnight>().Paralizate();

        //cAMBIO DE ESCENA
        /* 
         * Versión antigua, da proiblemas al haber 2 game overs
         * FindAnyObjectByType<GameOver>().TriggerNivel2();
         */
        GameObject.FindGameObjectWithTag("GestorDeNivel").GetComponent<GameOver>().TriggerNivel2();
    }
}