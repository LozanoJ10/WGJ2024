using Cinemachine;
using UnityEngine;
using System.Collections;

public class CamaraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public MovimientoPlayer_Controller movimientoPlayer;

    [Header("Foco Cinemachine")]
    public Transform puntoDeFocoCinemachine;

    [Header("Ovs Follow Cinemachine")]
    public Transform puntoIdle;
    public Transform puntoMoverArriba;
    public Transform puntoDerecha;
    public Transform puntoIzquierda;

    [Header("Ajustes de Transición")]
    public float gradualHorizontal;
    public float transitionSpeed = 0.5f;  // Controla la velocidad de transición del foco
    public float orthoTransitionSpeed = 0.5f; // Controla la velocidad de cambio del tamaño ortográfico
    public float focusChangeSpeed = 0.5f; // Velocidad de cambio de foco entre izquierda y derecha

    [Header("Ajustes de Transición hacia Inicial")]
    public float transitionSpeedToCentro;
    public float transitionSpeedToIdle = 0.5f;  // Controla la velocidad de transición del foco
    public float orthoTransitionSpeedToIdle = 0.5f; // Controla la velocidad de cambio del tamaño ortográfico

    [Header("Tiempo de Cambio de Foco")]
    public float tiempoParaCambiarFoco = 2f; // Tiempo que debe estar en movimiento para cambiar el foco

    private float minOrthoSize = 5f;
    private float maxOrthoSize = 11f;
    private bool isAtMoverArriba = false; // Indica si el foco está en MoverArriba
    private bool isMovingRight = false; // Indica si se está moviendo hacia la derecha
    private bool isMovingLeft = false; // Indica si se está moviendo hacia la izquierda
    private bool isResettingCamera = false; // Bloquea transiciones durante el reset
    public bool camaraFocus = false;
    private Coroutine focusCoroutine; // Referencia a la corrutina de cambio de foco

    // Inicialización
    void Start()
    {
        // Configura la posición inicial del foco en Idle
        puntoDeFocoCinemachine.position = puntoIdle.position;

        // Configura el tamaño ortográfico inicial a 5
        virtualCamera.m_Lens.OrthographicSize = minOrthoSize;

        // Asegura que gradualHorizontal comience en 0
        gradualHorizontal = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ResetCamera();
        }


        if (camaraFocus) return;
        // Si la cámara está siendo reseteada, no permitir otras transiciones
        if (isResettingCamera) return;

        float targetValue = Mathf.Abs(movimientoPlayer.horizontalInput);

        // Usar Lerp para hacer que gradualHorizontal se acerque al targetValue lentamente
        gradualHorizontal = Mathf.Lerp(gradualHorizontal, targetValue, transitionSpeed * Time.deltaTime);

        // Ajustar gradualHorizontal a 0 si está lo suficientemente cerca para evitar que "baile"
        if (gradualHorizontal < 0.01f)
        {
            gradualHorizontal = 0f;
        }

        // Fijar gradualHorizontal a 1 si está cerca de 1, y mantener el foco en MoverArriba
        if (gradualHorizontal >= 0.95f)
        {
            gradualHorizontal = 1f;
            isAtMoverArriba = true;
            // Debug.Log("Foco alcanzó MoverArriba");
        }

        // Si estamos en MoverArriba, iniciar la lógica de cambio de foco a derecha o izquierda
        if (isAtMoverArriba)
        {
            HandleFocusChange();
            return; // Salir del Update para evitar cualquier movimiento adicional
        }

        // Mover el foco gradualmente hacia MoverArriba si no ha alcanzado 1
        if (gradualHorizontal > 0f && !isAtMoverArriba)
        {
            puntoDeFocoCinemachine.position = Vector3.Lerp(puntoIdle.position, puntoMoverArriba.position, gradualHorizontal);

            // Cambiar el tamaño ortográfico gradualmente hacia 11
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(minOrthoSize, maxOrthoSize, gradualHorizontal);
        }
    }

    private void HandleFocusChange()
    {
        if (isResettingCamera) return;

        if (movimientoPlayer.horizontalInput > 0 && !isMovingRight)
        {
            // Si se mueve hacia la derecha y no estaba moviéndose ya hacia la derecha
            if (focusCoroutine != null)
            {
                StopCoroutine(focusCoroutine);
            }
            focusCoroutine = StartCoroutine(ChangeFocusAfterDelay(puntoDerecha));
            isMovingRight = true;
            isMovingLeft = false;
        }
        else if (movimientoPlayer.horizontalInput < 0 && !isMovingLeft)
        {
            // Si se mueve hacia la izquierda y no estaba moviéndose ya hacia la izquierda
            if (focusCoroutine != null)
            {
                StopCoroutine(focusCoroutine);
            }
            focusCoroutine = StartCoroutine(ChangeFocusAfterDelay(puntoIzquierda));
            isMovingLeft = true;
            isMovingRight = false;
        }
        else if (Mathf.Abs(movimientoPlayer.horizontalInput) < 0.01f)
        {
            // Si el jugador se queda quieto, regresar el foco a MoverArriba
            // Debug.Log("Jugador se quedó quieto, regresando a MoverArriba");
            if (focusCoroutine != null)
            {
                StopCoroutine(focusCoroutine);
            }
            focusCoroutine = StartCoroutine(ReturnToMoverArriba());
            isMovingRight = false;
            isMovingLeft = false;
        }
    }

    private IEnumerator ChangeFocusAfterDelay(Transform targetFocusPoint)
    {
        if (isResettingCamera) yield break;

        yield return new WaitForSeconds(tiempoParaCambiarFoco); // Espera el tiempo definido antes de cambiar el foco

        // Cambia el foco gradualmente hacia el nuevo punto
        while (Vector3.Distance(puntoDeFocoCinemachine.position, targetFocusPoint.position) > 0.01f)
        {
            if (isResettingCamera) yield break;

            puntoDeFocoCinemachine.position = Vector3.Lerp(puntoDeFocoCinemachine.position, targetFocusPoint.position, focusChangeSpeed * Time.deltaTime);
            yield return null; // Espera al siguiente frame antes de continuar la interpolación
        }

        // Asegúrate de que el foco esté exactamente en el punto de destino al final
        puntoDeFocoCinemachine.position = targetFocusPoint.position;
        // Debug.Log($"Foco llegó a {targetFocusPoint.name}");
    }

    private IEnumerator ReturnToMoverArriba()
    {
        if (isResettingCamera) yield break;

        // Mueve el foco gradualmente hacia MoverArriba
        while (Vector3.Distance(puntoDeFocoCinemachine.position, puntoMoverArriba.position) > 0.01f)
        {
            if (isResettingCamera) yield break;

            puntoDeFocoCinemachine.position = Vector3.Lerp(puntoDeFocoCinemachine.position, puntoMoverArriba.position, focusChangeSpeed * Time.deltaTime);
            yield return null;
        }

        // Asegúrate de que el foco esté exactamente en MoverArriba
        puntoDeFocoCinemachine.position = puntoMoverArriba.position;
        // Debug.Log("Foco regresó a MoverArriba");
    }

    public void ResetCamera()
    {
        // Activar el flag de reset
        isResettingCamera = true;

        // Stop any ongoing focus transition coroutines
        if (focusCoroutine != null)
        {
            StopCoroutine(focusCoroutine);
            focusCoroutine = null;
        }

        // Reset flags
        isAtMoverArriba = false;
        isMovingRight = false;
        isMovingLeft = false;
        gradualHorizontal = 0f;

        // Comienza la secuencia de movimientos
        StartCoroutine(VolverPuntoInicial());

    }

    private IEnumerator VolverPuntoInicial()
    {
        // Primero mueve la cámara hacia el centro (puntoMoverArriba)
        yield return StartCoroutine(MoviendoACentro());

        // Luego, mueve la cámara desde el centro hacia el punto Idle
        yield return StartCoroutine(MoveToIdle());

        // Desactivar el flag de reset una vez que todo el proceso haya terminado
        isResettingCamera = false;
    }

    private IEnumerator MoviendoACentro()
    {
        while (Vector3.Distance(puntoDeFocoCinemachine.position, puntoMoverArriba.position) > 0.01f)
        {
            // Mueve la posición del foco hacia el punto MoverArriba usando MoveTowards con transitionSpeedToCentro
            puntoDeFocoCinemachine.position = Vector3.MoveTowards(puntoDeFocoCinemachine.position, puntoMoverArriba.position, transitionSpeedToCentro * Time.deltaTime);

            yield return null; // Espera al siguiente frame antes de continuar la interpolación
        }

        // Asegúrate de que el foco esté exactamente en el punto MoverArriba al final
        puntoDeFocoCinemachine.position = puntoMoverArriba.position;
    }

    private IEnumerator MoveToIdle()
    {
        // Define el tamaño ortográfico inicial y final
        float startOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        float targetOrthoSize = minOrthoSize; // 5

        // Mientras la cámara no haya alcanzado la posición Idle y el tamaño ortográfico deseado
        while (Vector3.Distance(puntoDeFocoCinemachine.position, puntoIdle.position) > 0.01f || Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetOrthoSize) > 0.01f)
        {
            // Mueve la posición del foco hacia el punto Idle usando MoveTowards con transitionSpeedToIdle
            puntoDeFocoCinemachine.position = Vector3.MoveTowards(puntoDeFocoCinemachine.position, puntoIdle.position, transitionSpeedToIdle * Time.deltaTime);

            // Ajusta el tamaño ortográfico hacia el objetivo usando MoveTowards con orthoTransitionSpeedToIdle
            virtualCamera.m_Lens.OrthographicSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, orthoTransitionSpeedToIdle * Time.deltaTime);

            yield return null; // Espera al siguiente frame antes de continuar la interpolación
        }

        // Asegúrate de que el foco esté exactamente en el punto Idle y el OrthoSize sea el correcto al final
        puntoDeFocoCinemachine.position = puntoIdle.position;
        virtualCamera.m_Lens.OrthographicSize = targetOrthoSize;
    }

    public void FocusCamaraArea()
    {
        camaraFocus = true;
    }

    public void OutOfFocusCamaraArea()
    {
        camaraFocus = false;
    }
}
