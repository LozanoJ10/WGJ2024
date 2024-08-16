using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLoop : MonoBehaviour
{
    public GameObject loopWarp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador") && loopWarp.activeSelf)
        { 
            loopWarp.SetActive(false);

            // HOLA LU - ANTES DE SABER DE Wwise, ESTA FUE LA FORMA QUE CREE PARA HACER UNA BIBLIOTECA
            // Lo que hace es ir a la carpeta Assets/Resourses y busca el audio por su nombre.
            AudioClip sfxClip = AudioLibrary.instance.GetSFXClip("sfx_checkpoint_01");
            AudioSource.PlayClipAtPoint(sfxClip, Camera.main.transform.position);
            // PARA IMPLEMENTAR LOS SONIDOS. PUEDES // COMENTARIAR // ESTAS DOS LINEAS ANTERIORES. LA VERDAD ESTAN AQUI Y EN LA FLOR.
            Debug.Log("LU PonerAquiSonido de ROMPER LOOP");            
        }
    }
}
