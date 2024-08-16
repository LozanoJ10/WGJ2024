using System.Collections.Generic;
using UnityEngine;

public class CircularMovementDetector : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 lastPosition;
    private bool isDragging = false;

    public RectTransform rectTransform;
    public float detectionRadius = 5f; // The radius within which to detect objects
    public GameObject player; // The player GameObject, assign in the inspector
    public List<TimeBody> controlTiempos = new List<TimeBody>();

    public bool grupoAgregado;
    public bool ejecutandoAccionesRecord;
    [Header("Ui Reloj")]
    public GameObject manMin;
    public GameObject manHor;

    // Time tracking variables
    private float minuteAngle = 0f;
    private float hourAngle = 0f;

    // Rotation control variables
    public float minuteSpeed = 6f; // Speed of minute hand rotation
    public float hourSpeed = 0.5f; // Speed of hour hand rotation
    private bool autoRotate = true; // Flag to enable/disable auto rotation

    private float lastTime; // Store the time when dragging starts

    [Header("Vhs")]
    public GameObject vhsEfecto;
    [Header("Implementacion Sonido")]
    public LU_SoundManager implementacionSonido;
    public bool dioClick = false;


    //Juan estuvo aqui

    void Awake()
    {
        vhsEfecto.SetActive(false);
    }
    void Update()
    {
        ScanForGrupoRewind();

        // Stop automatic clock hand movement during dragging
        if (!isDragging && autoRotate)
        {
            UpdateClockHands();
        }

        if (Input.GetMouseButtonDown(0) && IsPointerOverUIObject())
        {
            autoRotate = false; // Disable auto rotation when the user starts dragging

            foreach (var controlTiempo in controlTiempos)
            {                
                    startPosition = rectTransform.anchoredPosition;
                    lastPosition = startPosition;
            }

            isDragging = true;
            lastTime = Time.time; // Store the time when dragging starts
            ////Debug.Log("Arrastre iniciado.");
            ///

            vhsEfecto.SetActive(true);
            //
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            autoRotate = true; // Re-enable auto rotation when the user stops dragging

            // Update angles based on the final position after dragging
            minuteAngle = -manMin.transform.localEulerAngles.z;
            hourAngle = -manHor.transform.localEulerAngles.z;

            lastTime = Time.time; // Reset lastTime to avoid jumps
            ////Debug.Log("Arrastre detenido.");
            ///

            vhsEfecto.SetActive(false);


        }

        if (isDragging)
        {
            if(!dioClick)
                implementacionSonido.SonidoRetrocederTiempo();

            Vector2 currentPosition = rectTransform.anchoredPosition;
            Vector2 direction = currentPosition - lastPosition;

            if (direction.magnitude > 0.1f)  // Detect small movements
            {
                Vector2 vectorA = lastPosition - startPosition;
                Vector2 vectorB = currentPosition - startPosition;

                // Calculate the angle between the two vectors
                float angle = Vector2.SignedAngle(vectorA, vectorB);

                // Rotate the UI elements based on the angle
                RotateUIElements(angle);

                // Detect positive or negative angle to trigger actions
                if (angle > 0)
                {
                    ////Debug.Log("Movimiento en sentido horario (manecillas del reloj). �ngulo: " + angle);
                    foreach (var controlTiempo in controlTiempos)
                    {
                        if (!controlTiempo.isRecording)
                        {
                            controlTiempo.StartRewind();
                        }
                    }
                }
                else if (angle < 0)
                {
                    ////Debug.Log("Movimiento en sentido antihorario (contra las manecillas del reloj). �ngulo: " + angle);
                    foreach (var controlTiempo in controlTiempos)
                    {
                        if (!controlTiempo.isRecording)
                        {
                            controlTiempo.StartForward();
                        }
                    }
                }

                // Update the last position
                lastPosition = currentPosition;
            }
            else
            {
                Debug.Log("LU_Aqui se debe detener el sonido de RETORCEDER EL TIEMPO Colocar el Stop de la pista");
                // Stop actions if there's no significant movement
                foreach (var controlTiempo in controlTiempos)
                {
                    controlTiempo.StopForward();
                    controlTiempo.StopRewind();
                }
            }
        }

        if (grupoAgregado && !ejecutandoAccionesRecord)
        {
            foreach (TimeBody obj in controlTiempos)
            {
                if (!obj.objetoUsado)
                {
                    obj.AddRigidbodyAndStartRecording();
                }
            }
            ejecutandoAccionesRecord = true;
        }
    }

    private void UpdateClockHands()
    {
        // Calculate the rotation based on real time or simulated game time since last update
        float elapsed = Time.time - lastTime; // Time elapsed since last interaction
        float minutesPassed = elapsed / 60f;  // Convert seconds to minutes
        float hoursPassed = minutesPassed / 60f;  // Convert minutes to hours

        minuteAngle += (minutesPassed * minuteSpeed) % 360f; // Rotate based on minute speed
        hourAngle += (hoursPassed * hourSpeed) % 360f; // Rotate based on hour speed

        if (manMin != null)
        {
            manMin.transform.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        }
        if (manHor != null)
        {
            manHor.transform.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        }

        lastTime = Time.time; // Update lastTime after each update
    }

    private void RotateUIElements(float angle)
    {
        // Ensure the rotation is applied correctly to the UI elements
        if (manMin != null)
        {
            manMin.transform.Rotate(0, 0, angle);
        }
        if (manHor != null)
        {
            manHor.transform.Rotate(0, 0, angle * 0.05f);
        }
    }

    private void ScanForGrupoRewind()
    {
        controlTiempos.Clear(); // Clear the list before scanning

        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("GrupoRewind"))
            {
                // Add all TimeBody components from the children of this object
                TimeBody[] timeBodies = hitCollider.GetComponentsInChildren<TimeBody>();
                controlTiempos.AddRange(timeBodies);
            }
        }

        if (controlTiempos.Count > 0)
        {
            grupoAgregado = true;
        }
        else
        {
            grupoAgregado = false;
            ejecutandoAccionesRecord = false; // Reset when no objects are within range
        }
    }

    private bool IsPointerOverUIObject()
    {
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }

    void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(player.transform.position, detectionRadius);
        }
    }
}
