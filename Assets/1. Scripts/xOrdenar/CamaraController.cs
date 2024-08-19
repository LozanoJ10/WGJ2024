using Cinemachine;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public MovimientoPlayer_Controller movimientoPlayer;

    public bool estaMoviendo;
    public float targetShoulderOffsetXPositivo; // Valor objetivo cuando horizontalInput > 0
    public float targetShoulderOffsetXNegativo; // Valor objetivo cuando horizontalInput < 0
    public float transitionSpeed = 2.0f; // Velocidad de la transición

    private float currentShoulderOffsetX;

    public float valorProbar;

    private void Start()
    {
        currentShoulderOffsetX = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x;
    }

    private void Update()
    {
        if (movimientoPlayer.horizontalInput != 0)
        {
            estaMoviendo = true;
        }
        else
        {
            estaMoviendo = false;
        }

        // Obtener la referencia al componente Cinemachine3rdPersonFollow
        Cinemachine3rdPersonFollow thirdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (estaMoviendo)
        {
            if (movimientoPlayer.horizontalInput > 0)
            {
                // Transición hacia el valor positivo del offset
                currentShoulderOffsetX = Mathf.Lerp(currentShoulderOffsetX, targetShoulderOffsetXPositivo, Time.deltaTime * transitionSpeed);
            }
            else if (movimientoPlayer.horizontalInput < 0)
            {
                // Transición hacia el valor negativo del offset
                currentShoulderOffsetX = Mathf.Lerp(currentShoulderOffsetX, targetShoulderOffsetXNegativo, Time.deltaTime * transitionSpeed);
            }
        }
        else
        {
            // Transición de vuelta a 0 cuando no se está moviendo
            currentShoulderOffsetX = Mathf.Lerp(currentShoulderOffsetX, 0, Time.deltaTime * transitionSpeed);
        }

        // Aplicar el valor interpolado al Shoulder Offset de la cámara
        thirdPersonFollow.ShoulderOffset = new Vector3(currentShoulderOffsetX, thirdPersonFollow.ShoulderOffset.y, thirdPersonFollow.ShoulderOffset.z);
    }
}
