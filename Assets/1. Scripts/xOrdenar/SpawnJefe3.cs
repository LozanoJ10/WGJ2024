using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJefe3 : MonoBehaviour
{
    public Transform puntoSpawnJefe_3;
    public GameObject jefePrefab; // Variable para el prefab del jefe
    public bool spawneo = false;
    //public GameManager sonidosLu;
    public LU_SoundManager implementacionSonidos;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene la etiqueta "Jugador"
        if (other.CompareTag("Jugador") && !spawneo)
        {
            // Instanciamos el prefab del jefe en el punto de spawn con su rotación original
            Instantiate(jefePrefab, puntoSpawnJefe_3.position, jefePrefab.transform.rotation);
            spawneo = true;
            //
            //if(sonidosLu.mostrarAvisos)
            Debug.Log("AQUI PONER SONIDO DE APARECE MALA");
            implementacionSonidos.MusicaAparicionJefe();
        }

        
    }


}
