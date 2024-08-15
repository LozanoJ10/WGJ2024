using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionJefeFinal : MonoBehaviour
{
    public Animator jefeAnimator;
    public Transform ovRefjugador; // Referencia al transform del jugador
    public Transform jugador; // Referencia al transform del jugador
    public float velocidadMovimiento = 7f; // Velocidad de movimiento del jefe
    public float distanciaMinima;
    public float distanciaMaxima;

    public float tiempoEspera = 5.0f; // Tiempo en segundos antes de que el mensaje se muestre
    public float velocidadDeReduccion;

    private void Start()
    {
        jefeAnimator = GetComponent<Animator>();
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
        ovRefjugador = GameObject.Find("ov_ReferenciaJugador").GetComponent<Transform>();

        // Iniciar la corrutina del temporizador
        StartCoroutine(Temporizador());
    }

    private void Update()
    {
        MoverHaciaJugador();
    }

    private void MoverHaciaJugador()
    {
        Vector3 direccion = ovRefjugador.position - transform.position;
        direccion.Normalize(); // Normalizar la dirección para obtener una magnitud de 1

        float distancia = Vector3.Distance(transform.position, ovRefjugador.position);
        Debug.Log("La distancia es: " + distancia);

        if (distancia > distanciaMinima && distancia < distanciaMaxima)
        {
            // Calcular una velocidad reducida al acercarse
            float factorReduccion = Mathf.Clamp01((distancia - distanciaMinima) / (distanciaMaxima - distanciaMinima)) * velocidadDeReduccion;
            float velocidadActual = velocidadMovimiento * factorReduccion;

            transform.position += direccion * velocidadActual * Time.deltaTime;
            transform.LookAt(jugador);
        }
    }

    public void SeVaElJefe()
    {
        jefeAnimator.SetBool("seVa", true);
    }

    // Corrutina del temporizador
    private IEnumerator Temporizador()
    {
        // Esperar por el tiempo especificado en tiempoEspera
        yield return new WaitForSeconds(tiempoEspera);

        Debug.Log("TemporizadorTermina_AccionJefeFinal");
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("ObjetoTumbarJefe"))
        {
            Debug.Log(collision.gameObject.name);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;

                // Generar una dirección aleatoria hacia la izquierda o derecha
                float direccionHorizontal = Random.Range(0f, 10f); // Valor aleatorio entre -0.5 y 0.5
                float direccionVertical = Random.Range(15f, 30f); // Valor aleatorio entre -0.5 y 0.5

                // Crear la dirección del empuje: hacia arriba y con una ligera desviación horizontal
                Vector3 direccionEmpuje = new Vector3(direccionHorizontal, direccionVertical, 0f).normalized;

                // Aplicar la fuerza con un valor ajustado
                rb.AddForce(direccionEmpuje * 300f); // Ajusta la magnitud del empuje según sea necesario
            }
        }

        if (collision.gameObject.CompareTag("DespawnerJefe"))
        {
            Debug.Log("HAciendo Colision DESAPARECER");
            SeVaElJefe();
        }
    }


    public void OnCollisionEnter(Collision collision)
    {

    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
