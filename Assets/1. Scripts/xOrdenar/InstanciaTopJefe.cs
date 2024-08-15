using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaTopJefe : MonoBehaviour
{
    public Animator jefeAnimator;
    public Transform jugador; // Referencia al transform del jugador
    //
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

    }

    public void SeVaElJefe()
    {
        jefeAnimator.SetBool("SeVa", true);
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
