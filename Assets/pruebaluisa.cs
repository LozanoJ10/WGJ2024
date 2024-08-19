using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaluisa : MonoBehaviour
{
    // Start is called before the first frame update
    public LU_SoundManager pSSeleccionar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        Debug.Log("pruebadown");
       pSSeleccionar.SonidoSeleccionObjeto();
    }
    void OnMouseUp()
    {
        Debug.Log("pruebaup");
        pSSeleccionar.StopSeleccionObjeto();
    }
}
