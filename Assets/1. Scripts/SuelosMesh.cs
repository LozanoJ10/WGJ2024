using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuelosMesh : MonoBehaviour
{
    public bool ocultarGYM;

    void Start()
    {
        // Busca todos los objetos en la escena que tienen el script ID_pisoGYM
        PisoGYM[] objetosConPisoGYM = FindObjectsOfType<PisoGYM>();

        // Recorre cada objeto encontrado
        foreach (PisoGYM objeto in objetosConPisoGYM)
        {
            // Verifica si el booleano ocultarGYM está activado
            if (ocultarGYM)
            {
                // Obtiene el MeshRenderer del objeto y lo deshabilita
                MeshRenderer meshRenderer = objeto.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }
}

