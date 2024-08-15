using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RengoDeteccion : MonoBehaviour
{
    public float detectionRadius;
    public GameObject player;
    public List<GameObject> objetosDetectados = new List<GameObject>();

    public bool objetoAgregado;

    void Update()
    {
        ScanForGrupoRewind();

        if (objetosDetectados.Count > 0)
        {
            foreach (GameObject objeto in objetosDetectados)
            {
                objeto.GetComponent<MovimientoObjetoTiempo>().detectadoPorPlayer = true;
            }
        }
    }

    private void ScanForGrupoRewind()
    {
        objetosDetectados.Clear(); // Limpiar antes de escanear

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, detectionRadius);

        foreach (var hitCollider in hitColliders)
        {
            GameObject objetoDetectado = hitCollider.gameObject;
            if (hitCollider.CompareTag("ObjetoVel") && !objetosDetectados.Contains(objetoDetectado))
            {
                objetosDetectados.Add(objetoDetectado); // Agregar si no está ya en la lista
            }
        }

        objetoAgregado = objetosDetectados.Count > 0;
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.transform.position, detectionRadius);
        }
    }
}
