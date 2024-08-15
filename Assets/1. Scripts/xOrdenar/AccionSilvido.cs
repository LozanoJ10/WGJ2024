using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionSilvido : MonoBehaviour
{
    public SilvidoController silvidoController;
    public float loudnessSensibility = 50;
    public float volumenDeAccion;
    public float valorRecibidoSoplido = 0; // Valor que cambia entre 0 y 1
    public float velocidadCambio = 0.5f; // Velocidad a la que el valor cambia

    private bool recibiendoSoplido = false;

    public bool sePuedeActivarVoz;

    void Update()
    {
        if (sePuedeActivarVoz)
        {
            float loudness = silvidoController.GetLoudnessFromMicrophone() * loudnessSensibility;

            if (loudness >= volumenDeAccion)
            {
                Debug.Log("RECIBIENDO VALOR DE MICROFONO");
                recibiendoSoplido = true;
            }
            else
            {
                recibiendoSoplido = false;
            }

            // Actualizar el valor gradualmente
            ActualizarValorRecibidoSoplido();
        }
    }

    void ActualizarValorRecibidoSoplido()
    {
        if (recibiendoSoplido)
        {
            // Incrementar valor gradualmente hacia 1
            valorRecibidoSoplido = Mathf.MoveTowards(valorRecibidoSoplido, 0.3f, velocidadCambio * Time.deltaTime);
        }
        else
        {
            // Decrementar valor gradualmente hacia 0
            valorRecibidoSoplido = Mathf.MoveTowards(valorRecibidoSoplido, 0, velocidadCambio * Time.deltaTime);
        }
    }
}
