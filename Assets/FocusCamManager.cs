using Cinemachine;
using UnityEngine;

public class FocusCamManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float transitionSpeed = 3.0f; // Velocidad de la transición
    public float focusOrthoSize = 5.0f;
    public float outOfFocusOrthoSize = 11.0f;
    public Vector3 focusShoulderOffset = new Vector3(0, 0, 0);
    public Vector3 outOfFocusShoulderOffset = new Vector3(0, 3, 0);
    public Vector2 focusDamping = new Vector2(0, 0);
    public Vector2 outOfFocusDamping = new Vector2(0.75f, 3);

    private float targetOrthoSize;
    private Vector3 targetShoulderOffset;
    private Vector2 targetDamping;

    private bool isTransitioning = false;

    public MovimientoPlayer_Controller estaCorriendo;
    public CamaraController camaraController; // Referencia al script CamaraController

    public bool haciendoFocus = false;

    void Start()
    {
        // Inicializa los valores objetivo a los valores de OutOfFocus
        targetOrthoSize = outOfFocusOrthoSize;
        targetShoulderOffset = outOfFocusShoulderOffset;
        targetDamping = outOfFocusDamping;

        // Establecer los valores iniciales de la cámara
        Cinemachine3rdPersonFollow thirdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        thirdPersonFollow.ShoulderOffset = targetShoulderOffset;
        thirdPersonFollow.Damping.x = targetDamping.x;
        thirdPersonFollow.Damping.y = targetDamping.y;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            haciendoFocus = !haciendoFocus;
        }

        // Ajusta la velocidad de transición según el estado del jugador
        transitionSpeed = estaCorriendo.estaCorriendo ? 5.0f : 3.0f;
        transitionSpeed = estaCorriendo.estaEnUnPISO ? 3.0f : 9.0f;

        // Determina si se debe enfocar o desenfocar
        if (haciendoFocus)
        {
            SetFocusTargets();
        }
        else
        {
            SetOutOfFocusTargets();
        }

        // Realiza las transiciones utilizando Lerp
        PerformTransitions();
    }

    private void SetFocusTargets()
    {
        if (!isTransitioning || targetOrthoSize != focusOrthoSize)
        {
            targetOrthoSize = focusOrthoSize;
            targetShoulderOffset = focusShoulderOffset;
            targetDamping = focusDamping;
            isTransitioning = true;
        }

        if (camaraController != null)
        {
            camaraController.targetShoulderOffsetXPositivo = 0;
            camaraController.targetShoulderOffsetXNegativo = 0;
        }
    }

    private void SetOutOfFocusTargets()
    {
        if (!isTransitioning || targetOrthoSize != outOfFocusOrthoSize)
        {
            targetOrthoSize = outOfFocusOrthoSize;
            targetShoulderOffset = outOfFocusShoulderOffset;
            targetDamping = outOfFocusDamping;
            isTransitioning = true;
        }

        if (camaraController != null)
        {
            camaraController.targetShoulderOffsetXPositivo = 2;
            camaraController.targetShoulderOffsetXNegativo = -2;
        }
    }

    private void PerformTransitions()
    {
        Cinemachine3rdPersonFollow thirdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        // Transición suave de Ortho Size
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, Time.deltaTime * transitionSpeed);

        // Transición suave de Shoulder Offset
        thirdPersonFollow.ShoulderOffset = Vector3.Lerp(thirdPersonFollow.ShoulderOffset, targetShoulderOffset, Time.deltaTime * transitionSpeed);

        // Transición suave de Damping
        thirdPersonFollow.Damping.x = Mathf.Lerp(thirdPersonFollow.Damping.x, targetDamping.x, Time.deltaTime * transitionSpeed);
        thirdPersonFollow.Damping.y = Mathf.Lerp(thirdPersonFollow.Damping.y, targetDamping.y, Time.deltaTime * transitionSpeed);

        // Verifica si la transición ha terminado
        if (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetOrthoSize) < 0.01f &&
            Vector3.Distance(thirdPersonFollow.ShoulderOffset, targetShoulderOffset) < 0.01f &&
            Mathf.Abs(thirdPersonFollow.Damping.x - targetDamping.x) < 0.01f &&
            Mathf.Abs(thirdPersonFollow.Damping.y - targetDamping.y) < 0.01f)
        {
            isTransitioning = false;
        }
    }
}
