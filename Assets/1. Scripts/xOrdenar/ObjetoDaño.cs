using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDaño : MonoBehaviour
{
    public LU_SoundManager implementacionSonido;

    // Start is called before the first frame update
    void Start()
    {
        implementacionSonido = GameObject.Find("LU_SonidosManager").GetComponent<LU_SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            Debug.Log("Soy objeto de nombre: " + this.transform.name);
            implementacionSonido.SonidoRomperObjetos();
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("PlataformaEstatica"))
        {
            Debug.Log("Soy objeto de nombre: " + this.transform.name);
            implementacionSonido.SonidoRomperObjetos();
            Destroy(this.gameObject);
        }
    }
}
