using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;
    public bool isForwarding = false;
    public bool isRecording = false; // Inicialmente la grabaci�n es falsa
    public float recordTime = 5f; // Tiempo total para grabar
    private float recordTimer = 0f; // Temporizador para rastrear el tiempo de grabaci�n

    List<PointInTime> pointsInTime;
    int currentIndex = 0; // �ndice para rastrear la posici�n actual en forward y rewind

    public bool objetoUsado;

    Rigidbody objetoRb;

    void Start()
    {
        pointsInTime = new List<PointInTime>();
        objetoRb = GetComponent<Rigidbody>();
        recordTimer = 0f; // Inicializar el temporizador de grabaci�n
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            StopRewind();
        }
        if (Input.GetKeyDown(KeyCode.L)) // Iniciar forwarding
        {
            StartForward();
        }
        if (Input.GetKeyUp(KeyCode.L)) // Detener forwarding
        {
            StopForward();
        }
        if (Input.GetKeyDown(KeyCode.Y)) // Llamar al nuevo m�todo cuando se presione Y
        {
            AddRigidbodyAndStartRecording();
        }
    }

    private void FixedUpdate()
    {
        if (isRecording)
        {
            Record();
        }
        else if (isRewinding)
        {
            Rewind();
        }
        else if (isForwarding)
        {
            Forward();
        }
    }

    void Record()
    {
        if (isRecording)
        {
            recordTimer += Time.fixedDeltaTime; // Aumenta el temporizador

            if (recordTimer >= recordTime)
            {
                StopRecording(); // Detiene la grabaci�n si se alcanza el tiempo m�ximo
            }
            else
            {
                pointsInTime.Add(new PointInTime(transform.position, transform.rotation));
                currentIndex = pointsInTime.Count - 1; // Asegura que el �ndice siempre est� al final mientras se graba
            }
        }
    }

    void Rewind()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            PointInTime pointInTime = pointsInTime[currentIndex];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
        }
    }

    void Forward()
    {
        if (currentIndex < pointsInTime.Count - 1)
        {
            currentIndex++;
            PointInTime pointInTime = pointsInTime[currentIndex];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
        }
    }

    public void StopRecording()
    {
        isRecording = false;
        if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.isKinematic = true; // Detener el movimiento del objeto
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        isForwarding = false;
        isRecording = false; // Asegura que no siga grabando

        if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.isKinematic = true;
        }
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.isKinematic = true;
        }
    }

    public void StartForward()
    {
        isForwarding = true;
        isRewinding = false;
        isRecording = false; // Asegura que no siga grabando

        if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.isKinematic = true;
        }
    }

    public void StopForward()
    {
        isForwarding = false;
        if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
        if (objetoRb != null)
        {
            objetoRb.isKinematic = true;
        }
    }

    void AddRigidbody()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            objetoRb = gameObject.AddComponent<Rigidbody>();
            Debug.Log("Rigidbody added to the object.");
        }
        else
        {
            Debug.Log("Rigidbody already exists on the object.");
            objetoRb = GetComponent<Rigidbody>(); // Asegura que objetoRb est� asignado
        }
    }

    void StartRecording()
    {
            isRecording = true;
            recordTimer = 0f; // Reinicia el temporizador al comenzar la grabaci�n
            if (objetoRb == null) objetoRb = GetComponent<Rigidbody>();
            if (objetoRb != null)
            {
                objetoRb.isKinematic = false; // Asegura que el objeto puede moverse si es necesario
            }
            Debug.Log("Recording started.");
    }

    // Nuevo m�todo para agregar Rigidbody y empezar la grabaci�n
    public void AddRigidbodyAndStartRecording()
    {
        AddRigidbody();
        StartRecording();
        objetoUsado = true;
    }
}
