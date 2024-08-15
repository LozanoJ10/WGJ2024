using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJefeFinal : MonoBehaviour
{
    public Transform puntoSpawnJefe_Final;
    public GameObject jefeFinalPrefab; // Variable para el prefab del jefe
    public bool spawneo = false;

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene la etiqueta "Jugador" y si aún no se ha hecho el spawn
        if (other.CompareTag("Jugador") && !spawneo)
        {
            // Asegurarnos de que este código solo se ejecuta una vez
            spawneo = true;

            // Instanciamos el prefab del jefe en el punto de spawn con su rotación original
            Instantiate(jefeFinalPrefab, puntoSpawnJefe_Final.position, puntoSpawnJefe_Final.rotation);
        }
    }
}
