using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController1 : MonoBehaviour
{
    public Transform tpLlegada;        // Punto de teletransporte
    public float gizmoRadius = 0.5f;   // Radio del gizmo
    public Color tpGizmoColor = Color.red;   // Color del gizmo en el punto de teletransporte
    public Color startGizmoColor = Color.green; // Color del gizmo en el punto de partida

    public Transform jugador;    
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;  // Guardamos la posición inicial del objeto
    }

    void Update()
    {
        /*
        float distancia = Vector3.Distance(jugador.position, initialPosition);

        if (distancia <= gizmoRadius && !dentroDeLaZona)
        {
            // El jugador entra en la zona
            HacerLoop();
            dentroDeLaZona = true;
        }
        else if (distancia > gizmoRadius && dentroDeLaZona)
        {
            // El jugador sale de la zona
            dentroDeLaZona = false;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            HacerLoop();
        }
    }

    public void HacerLoop()
    {
        Debug.Log("HaciendoLoop");
        jugador.position = tpLlegada.position;
    }

    private void OnDrawGizmos()
    {
        // Dibujar gizmo en el punto de teletransporte
        if (tpLlegada != null)
        {
            Gizmos.color = tpGizmoColor;
            Gizmos.DrawSphere(tpLlegada.position, gizmoRadius);
            Gizmos.DrawLine(initialPosition, tpLlegada.position);
        }

        // Dibujar gizmo en el punto de partida
        Gizmos.color = startGizmoColor;
        Gizmos.DrawSphere(initialPosition, gizmoRadius);
    }
}
