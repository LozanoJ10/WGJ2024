using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionFollowCinemachine : MonoBehaviour
{
    public Transform posicionJugador;
    [Header("Posiciones Camara")]
    public Transform puntoIdle;
    public Transform puntoMoverArriba;
    public Transform puntoDerecha;
    public Transform puntoIzquierda;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = posicionJugador.position;
    }
}
