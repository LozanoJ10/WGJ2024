using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionSpawnJefe : MonoBehaviour
{
    public Transform posicionJugador;
    [Header("Posiciones Camara")]
    public Transform puntoIzquierda;
    public Transform puntoArriba;
    public Transform puntoDerecha;
    public Transform puntoJefeFinal;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = posicionJugador.position;
    }
}
