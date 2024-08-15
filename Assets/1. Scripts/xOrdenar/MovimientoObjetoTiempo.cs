using UnityEngine;
using UnityEngine.UI;

public class MovimientoObjetoTiempo : MonoBehaviour
{
    public Transform punto1; // Primer punto
    public Transform punto2; // Segundo punto
    public float velocidad = 1f; // Velocidad máxima de movimiento
    public float tiempoModificador = 0f; // Controla la dirección del movimiento, de -1 a 1

    private Vector3 objetivoActual; // Posición del objetivo actual
    public Slider sliderVelocidad;

    public bool detectadoPorPlayer = false;
    public bool objetoActivado = false;
    public bool esObjetoActivable;

    public RengoDeteccion rangoDeteccion;
    
    private static MovimientoObjetoTiempo currentAssignedObject; // Static reference to track the current object with the slider assigned


    void Start()
    {
        sliderVelocidad = null;
        objetivoActual = punto2.position; // Comenzamos moviéndonos hacia punto2
        rangoDeteccion = GameObject.Find("RangoDeteccionJugador").GetComponent<RengoDeteccion>();
    }

    void Update()
    {
        if (sliderVelocidad != null)
        {
            tiempoModificador = sliderVelocidad.value;
        }

        // Calcular la velocidad actual basada en tiempoModificador
        float velocidadActual = velocidad * Mathf.Abs(tiempoModificador);

        // Si tiempoModificador es 0, detener el objeto
        if (tiempoModificador == 0)
        {
            return;
        }

        if (!esObjetoActivable)
        {
            transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidadActual * Time.deltaTime);

            if (Vector3.Distance(transform.position, objetivoActual) < 0.1f)
            {
                CambiarObjetivo();
            }
        }

        if (esObjetoActivable && objetoActivado)
        {            
            transform.position = Vector3.MoveTowards(transform.position, objetivoActual, velocidadActual * Time.deltaTime);
                     
            if (Vector3.Distance(transform.position, objetivoActual) < 0.1f)
            {
                CambiarObjetivo();
            }
        }

        if (!rangoDeteccion.objetoAgregado && !esObjetoActivable)
        {
            //Debug.Log("Desactivando por RangoDeteccion, desde mov objeto tiempo");
            objetoActivado = false;
            detectadoPorPlayer = false;
            sliderVelocidad = null;
            velocidadActual = velocidad;
            tiempoModificador = 1;
        }

        if (!rangoDeteccion.objetoAgregado && esObjetoActivable)
        {
            //Debug.Log("Desactivando OBJETO ACTIVABLE, desde mov objeto tiempo");
            //sliderVelocidad = null;
            detectadoPorPlayer = false;
            tiempoModificador = 1;
        }
    }

    void CambiarObjetivo()
    {
        // Cambiar el objetivo dependiendo del valor de tiempoModificador
        if (tiempoModificador > 0)
        {
            // Si el modificador es positivo, el siguiente objetivo es punto2
            objetivoActual = (objetivoActual == punto1.position) ? punto2.position : punto1.position;
        }
        else if (tiempoModificador < 0)
        {
            // Si el modificador es negativo, el siguiente objetivo es punto1
            objetivoActual = (objetivoActual == punto2.position) ? punto1.position : punto2.position;
        }
    }

    private void OnMouseDown()
    {
        // Verificar si el clic pasó a través del objeto transparente
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != gameObject) // Ignorar el objeto actual
            {
                Debug.Log("Objeto clickeado detrás del transparente: " + hit.collider.name);

                // Aquí puedes manejar el clic en el objeto detrás del transparente
                MovimientoObjetoTiempo clickedObject = hit.collider.GetComponent<MovimientoObjetoTiempo>();
                if (clickedObject != null)
                {
                    clickedObject.AsignarSliderAlObjeto(); // Asignar el slider al objeto que está detrás
                }

                return; // Salir del método una vez que se haya encontrado y manejado el clic en el objeto detrás
            }
        }

        Debug.Log("Dando Click en: " + transform.name);
        AsignarSliderAlObjeto();
        objetoActivado = true;
        if (sliderVelocidad != null) sliderVelocidad.value = 0.5f;
    }


    public void AsignarSliderAlObjeto()
    {
        if (detectadoPorPlayer)
        {
            // If there is already an object with the slider assigned, set its slider to null
            if (currentAssignedObject != null && currentAssignedObject != this)
            {
                currentAssignedObject.DesasignarSlider();
            }

            // Assign the slider to this object
            sliderVelocidad = GameObject.Find("SliderTiempo").GetComponent<Slider>();
            currentAssignedObject = this; // Set this object as the current assigned object
        }
        else
        {
            Debug.Log("No se puede Asignar, no esta en rango");
        }
    }

    public void DesasignarSlider()
    {
        sliderVelocidad = null;        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            other.transform.SetParent(null);
        }
    }
}
