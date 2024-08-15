using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoMovilClick_ : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    public float distanciaMaxima = 5f; // Distancia m�xima de arrastre
    public float suavizado = 0.1f; // Ajusta este valor para m�s o menos suavidad

    private Vector3 targetPosition; // Nueva posici�n objetivo para suavizar el movimiento
    private Vector3 pivotPosition; // Posici�n inicial del objeto que act�a como pivote fijo

    public bool puedeMover = true;

    void Start()
    {
        pivotPosition = transform.position; // Establecer la posici�n inicial del objeto como pivote
        targetPosition = transform.position; // Inicializar en la posici�n actual
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;

        if (puedeMover)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        offset = transform.position - GetMouseWorldPos();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                Vector3 mousePosition = GetMouseWorldPos() + offset;

                // Calcular la direcci�n desde la posici�n pivote hasta la posici�n del mouse
                Vector3 direction = (mousePosition - pivotPosition).normalized;

                // Calcular la distancia desde la posici�n pivote hasta la posici�n del mouse
                float distance = Vector3.Distance(pivotPosition, mousePosition);

                // Si la distancia es menor o igual a la distancia m�xima, permitir el movimiento completo
                if (distance <= distanciaMaxima)
                {
                    targetPosition = mousePosition;
                }
                else
                {
                    // Si la distancia es mayor a la m�xima, limitar el movimiento
                    targetPosition = pivotPosition + direction * distanciaMaxima;
                }
            }

            // Suavizado del movimiento
            transform.position = Vector3.Lerp(transform.position, targetPosition, suavizado);
        }

        if (!puedeMover)
        {
            isDragging = false;
            Input.GetMouseButtonUp(0);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(transform);
            puedeMover = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(null);
            puedeMover = true;
        }
    }
}
