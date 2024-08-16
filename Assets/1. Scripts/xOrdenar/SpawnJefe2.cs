using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnJefe2 : MonoBehaviour
{
    public Transform puntoSpawnJefe_2;
    public GameObject jefePrefab; // Variable para el prefab del jefe
    public bool spawneo = false;

    public Transform spawnObjetoDaño1;
    public Transform spawnObjetoDaño2;
    public Transform spawnObjetoDaño3;
    public Transform spawnObjetoDaño4;
    public Transform spawnObjetoDaño5;

    public GameObject minionPrefab;

    private IEnumerator minionSpawner;
    private List<Transform> spawnPoints;
    public LU_SoundManager implementacionSonidos;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializamos la lista de puntos de spawn
        spawnPoints = new List<Transform> { spawnObjetoDaño1, spawnObjetoDaño2, spawnObjetoDaño3, spawnObjetoDaño4, spawnObjetoDaño5 };
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Método para detectar colisiones con el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene la etiqueta "Jugador"
        if (other.CompareTag("Jugador") && !spawneo)
        {
            // Instanciamos el prefab del jefe en el punto de spawn con su rotación original
            Instantiate(jefePrefab, puntoSpawnJefe_2.position, jefePrefab.transform.rotation);
            implementacionSonidos.MusicaAparicionJefe();
            spawneo = true;

            // Iniciar el contador para instanciar minions
            minionSpawner = SpawnObjetosDaño();
            StartCoroutine(minionSpawner);
        }
    }

    // Corrutina para instanciar el prefab en cada punto de manera aleatoria
    private IEnumerator SpawnObjetosDaño()
    {
        // Crear una copia de la lista original para modificarla sin afectar la lista original
        List<Transform> tempSpawnPoints = new List<Transform>(spawnPoints);

        while (tempSpawnPoints.Count > 0)
        {
            // Seleccionar un índice aleatorio
            int randomIndex = Random.Range(0, tempSpawnPoints.Count);

            // Instanciar el prefab en el punto de spawn aleatorio
            Instantiate(minionPrefab, tempSpawnPoints[randomIndex].position, tempSpawnPoints[randomIndex].rotation);

            // Eliminar el punto de spawn seleccionado de la lista temporal para que no se repita
            tempSpawnPoints.RemoveAt(randomIndex);

            yield return new WaitForSeconds(0.2f);
        }

        // Después de que todos los puntos hayan sido utilizados, reiniciar la lista y repetir el proceso para instanciar el segundo minion en cada punto
        tempSpawnPoints = new List<Transform>(spawnPoints);

        while (tempSpawnPoints.Count > 0)
        {
            int randomIndex = Random.Range(0, tempSpawnPoints.Count);

            Instantiate(minionPrefab, tempSpawnPoints[randomIndex].position, tempSpawnPoints[randomIndex].rotation);

            tempSpawnPoints.RemoveAt(randomIndex);

            yield return new WaitForSeconds(0.3f);
        }
    }
}
