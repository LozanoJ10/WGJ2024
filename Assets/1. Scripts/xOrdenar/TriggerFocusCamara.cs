using System.Collections;
using UnityEngine;

public class TriggerFocusCamara : MonoBehaviour
{
    public CamaraController camaraController;    

    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            if (!camaraController.camaraFocus)
            {
                StartCoroutine(SecuenciaCamara());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            camaraController.camaraFocus = true;
        }
    }    

    public IEnumerator SecuenciaCamara()
    {
        // Call ResetCamera method
        camaraController.ResetCamera();

        // Optionally, if you need a short delay, you can uncomment the following line
         yield return new WaitForSeconds(1f); 

        // Call FocusCamaraArea method after ResetCamera has finished
        camaraController.FocusCamaraArea();

        // Yield to continue after this frame
        yield return null;
    }
}
