using UnityEngine;

public class Fondo : MonoBehaviour
{
    private float startPosX;
    public Transform cam;
    public float parallaxMultiplier;

    /*
     * Creamos un script para hacer un fondo paralax (el fondo cercano se mueve m�s con nosotros, el lejano
     * se ve m�s lejos y por tanto, se mueve menos respecto a nuestro movimiento
     * 
     */


    void Start()
    {

        /*
         * Fijo la posici�n X inicial (de arranque), y cojo de la c�mara ppal, sus datos de transform
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
         * Aplico el multiplicador (se carga desde propiedades de Unity) a la posici�n que se mueve la c�mara. Valores bajos de este
         * multiplicador implica poco movimiento, valores altos, m�s movimiento. El fondo m�s lejano, lo pongo a 0.1 para que casi no se
         * mueva, el m�s cercano, a 0.9.
         * 
         */

        float distance = cam.position.x * parallaxMultiplier;
        transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);
    }
}