using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionEnemigoArma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Tocando Enemigo, Script en el Arma1");
        }
    }
}
