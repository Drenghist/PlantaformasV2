using UnityEngine;

public class Fondo : MonoBehaviour
{
    private float startPosX;
    public Transform cam;
    public float parallaxMultiplier;

    /*
     * Creamos un script para hacer un fondo paralax (el fondo cercano se mueve más con nosotros, el lejano
     * se ve más lejos y por tanto, se mueve menos respecto a nuestro movimiento
     * 
     */


    void Start()
    {

        /*
         * Fijo la posición X inicial (de arranque), y cojo de la cámara ppal, sus datos de transform
         * 
         * 
         */

        startPosX = transform.position.x;
        if (cam == null)
            cam = Camera.main.transform;
    }

    void Update()
    {
        /*
         * Aplico el multiplicador (se carga desde propiedades de Unity) a la posición que se mueve la cámara. Valores bajos de este
         * multiplicador implica poco movimiento, valores altos, más movimiento. El fondo más lejano, lo pongo a 0.1 para que casi no se
         * mueva, el más cercano, a 0.9.
         * 
         */

        float distance = cam.position.x * parallaxMultiplier;
        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);
    }
}