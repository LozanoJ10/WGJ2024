using UnityEngine;
using System.Collections;

public class spawnJefe1 : MonoBehaviour
{
    public Transform puntoSpawnJefe_1;
    public GameObject jefePrefab; // Variable para el prefab del jefe
    public bool spawneo = false;

    public Transform spawnMinion1;
    public Transform spawnMinion2;
    public GameObject minionPrefab;

    private IEnumerator minionSpawner;
    public LU_SoundManager implementacionSonidos;

    // Start is called before the first frame update
    void Start()
    {

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
            // Instanciamos el prefab del jefe en el punto de spawn
            Instantiate(jefePrefab, puntoSpawnJefe_1.position, puntoSpawnJefe_1.rotation);
            spawneo = true;
            implementacionSonidos.MusicaAparicionJefe();

            // Iniciar el contador para instanciar minions
            minionSpawner = SpawnMinionsWithDelay();
            StartCoroutine(minionSpawner);
        }
    }

    // Corrutina para instanciar minions con un delay
    private IEnumerator SpawnMinionsWithDelay()
    {
        // Esperar 3 segundos y luego instanciar el primer minion
        yield return new WaitForSeconds(3f);
        Instantiate(minionPrefab, spawnMinion1.position, spawnMinion1.rotation);

        // Esperar 3 segundos más (6 segundos en total) y luego instanciar el segundo minion
        yield return new WaitForSeconds(3f);
        Instantiate(minionPrefab, spawnMinion2.position, spawnMinion2.rotation);
    }
}
