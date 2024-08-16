using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RecibirGolpeEnemigo : MonoBehaviour
{
    private Rigidbody rb;
    private bool isVibrating = false;
    public EnemigoStadisticas enemigoEstats;
    [Header("Escudo")]
    public float timer = 0;
    public GameObject escudo;
    [Header("_")]
    public NavMeshAgent enemigoAgent;
    public LU_SoundManager implementacionSonido;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemigoAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isVibrating)
        {
            timer += Time.deltaTime;
            implementacionSonido.SonidoRetenerEnemigo();
            
        }

        if (timer >= 3)
        { 
            escudo.SetActive(false);
            enemigoEstats.tieneEscudo = false;
            implementacionSonido.SonidoRomperEscudo();
            Destroy(this.gameObject);
        }
    }


    void OnMouseDown()
    {
        Debug.Log("Objeto clickeado: " + gameObject.name);
        
        isVibrating = true;        
        StartCoroutine(Vibrar());
        //
        if (enemigoAgent.enabled)
        {
            enemigoAgent.enabled = false;
        }
    }

    void OnMouseUp()
    {
        isVibrating = false;        
        timer = 0;
        //
        enemigoAgent.enabled = true;
    }

    IEnumerator Vibrar()
    {
        float magnitude = 0.1f; // Magnitud de la vibración
        Vector3 originalPosition = transform.position;

        while (isVibrating)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            rb.MovePosition(originalPosition + new Vector3(x, y, z));

            yield return null;
        }

        // Volver a la posición original al final de la vibración
        rb.MovePosition(originalPosition);
    }
}
