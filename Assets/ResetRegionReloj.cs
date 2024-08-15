using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRegionReloj : MonoBehaviour
{
    public float valorRegionReset;
    public CircularMovementDetector rangoDeteccion;
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
        if (other.gameObject.CompareTag("Jugador"))
        {
            rangoDeteccion.detectionRadius = valorRegionReset;
        }
    }
}
