using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaDerechaJefe : MonoBehaviour
{
    public Animator jefeAnimator;
    public Transform jugador; // Referencia al transform del jugador
    //
    public float tiempoEspera = 5.0f; // animacion de irse

    private void Start()
    {
        jefeAnimator = GetComponent<Animator>();
        jugador = GameObject.Find("Jugador").GetComponent<Transform>();
                
        StartCoroutine(Temporizador());
    }

    private void Update()
    {

    }

    public void SeVaElJefe() // Por si acaso, llamarlo
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
