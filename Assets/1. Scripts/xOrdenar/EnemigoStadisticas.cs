using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoStadisticas : MonoBehaviour
{
    public int vida;
    [Header("Checks")]
    public bool golpeando;
    public bool tieneEscudo;

    void Start()
    {

    }

    void Update()
    {
        if (vida <= 0)
        {
            EnemigoMorido();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arma1") && !golpeando)
        {
            Debug.Log("Arma golpeando " + transform.name);
            golpeando = true;

            if (!tieneEscudo)
            {
                vida--;
            }
            else
            {
                Debug.Log("TIENE ESCUDO, NO HAY DAÑO");
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Arma1"))
        {
            Debug.Log("Arma NO GOLPEANDO" + transform.name);
            golpeando = false;
        }
    }

    public void EnemigoMorido()
    {
        Debug.Log("Enemigo " + this.name + " Morido :S");
        this.gameObject.SetActive(false);
    }
}
