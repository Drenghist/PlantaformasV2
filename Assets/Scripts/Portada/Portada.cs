using TMPro;
using UnityEngine;

public class Portada : MonoBehaviour
{
    public static Portada Instance;

    private float record = 9999;

    private GameObject objetoTextoRecord;
    private TextMeshProUGUI textoRecord;  
    

    public void GrabaRecord(float nuevoRecord)
    {
        if (record > nuevoRecord)
        {
            record = nuevoRecord;
        }
        
    }

    public float MuestraRecord()
    {
        return record;

    }


    void Start()
    {

    }


    void Update()
    {
        if (textoRecord == null) //Solo se ejecuta una vez, cuando no existe textoRecord vinculado
        {
            if (GameObject.Find("Record")) // Para que si no estamos en la pantalla ppal, no de errores 
            {
                objetoTextoRecord = GameObject.Find("Record");
                textoRecord = objetoTextoRecord.GetComponent<TextMeshProUGUI>();
                if (record != 9999) //Solo lo cambio cuando sea un valor válido
                {
                    int minutos = Mathf.FloorToInt(record / 60f);
                    int segundos = Mathf.FloorToInt(record % 60f);
                    int decimas = Mathf.FloorToInt(record * 10 % 10);

                    textoRecord.text = string.Format("{0:00}:{1:00}.{2}", minutos, segundos, decimas);
                }
            }
            
            
        }
        
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


}
