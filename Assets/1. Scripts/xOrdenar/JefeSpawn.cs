using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeSpawn : MonoBehaviour
{
    public Animator jefeAnimator;
    public Transform jugador; // Referencia al transform del jugador
    public float velocidadMovimiento = 2.0f; // Velocidad de movimiento del jefe
    public float distanciaMinima = 1.0f; // Distancia mínima al jugador antes de detenerse
    public float tiempoEspera = 5.0f; // Tiempo en segundos antes de que el mensaje se muestre

    private void Start()
    {
        jefeAnimator = GetComponent<Animator>();
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();

        // Iniciar la corrutina del temporizador
        StartCoroutine(Temporizador());
    }

    private void Update()
    {
        MoverHaciaJugador();
    }

    private void MoverHaciaJugador()
    {
        // Calcular la dirección hacia el jugador
        Vector3 direccion = jugador.position - transform.position;
        direccion.Normalize(); // Normalizar la dirección para obtener una magnitud de 1

        // Calcular la distancia actual al jugador
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // Mover el jefe hacia el jugador solo si la distancia es mayor a la mínima
        if (distancia > distanciaMinima)
        {
            // Mover el jefe hacia el jugador
            transform.position += direccion * velocidadMovimiento * Time.deltaTime;

            // Rotar el jefe para que mire hacia el jugador
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

        // Mostrar mensaje en consola cuando se cumple el tiempo
        jefeAnimator.SetBool("seVa", true);
    }

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
