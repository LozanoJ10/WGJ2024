using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickObjetoController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; //
    private Vector3 posicionOriginal;          // Funcionara como el pivote

    public float fuerzaDeAtraccion = 10f;      // Fuerza de atracción hacia el origen
    public float distanciaMaxima = 5f;         // Distancia máxima de arrastre del objeto

    void Start()
    {        
        posicionOriginal = transform.position; // Obtiene la posicion inicial del objeto
    }

    void Update()
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
                
        if (Input.GetMouseButtonUp(0)) // Para devolver el objeto
        {
            isDragging = false;
        }
                
        if (isDragging) // Si se está arrastrando el objeto (Dando Click mantenido sobre el), actualiza su posición
        {
            Vector3 targetPosition = GetMouseWorldPos() + offset;
                        
            if (Vector3.Distance(posicionOriginal, targetPosition) <= distanciaMaxima) // Solo puede moverlo hasta la distancia maxima
            {
                transform.position = targetPosition;
            }
            else // Para restringir el movimiento del objeto
            {                
                Vector3 direction = (targetPosition - posicionOriginal).normalized;
                transform.position = posicionOriginal + direction * distanciaMaxima;
            }
        }
        else
        {           
            transform.position = Vector3.Lerp(transform.position, posicionOriginal, fuerzaDeAtraccion * Time.deltaTime);
        }
    }

    private Vector3 GetMouseWorldPos() // Obtiene la posicion del mouse, segun la camara
    {
        Debug.LogWarning("NOTA2: Posiblemente tengamos problemas al cambiar a cinemachine");

        Vector3 mousePoint = Input.mousePosition;
             
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(null);
        }
    }
}
