using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer_Controller : MonoBehaviour
{
    public Rigidbody playerRb;

    [Header("De Movimiento")]
    private float velocidadActual;
    public float velocidadCaminar; // Velocidad ajustable desde el inspector
    public float horizontalInput;
    public float valorMultiCorrer;
    private Vector3 movement;

    [Header("De Salto")]
    public bool estaEnUnPISO;
    public bool estaEnSuelo;
    public bool estaEnObjetoMovil;
    public bool estaEnObjetoRe;
    public bool estaEnObjetoVel;
    public float fuerzaSalto;

    public bool estaCorriendo;

    // Nueva variable para rotación
    private bool mirandoALaDerecha = true;

    [Header("Configuración del Gizmo")]
    public Vector3 gizmoCenter = Vector3.zero;
    public Vector3 gizmoSize = new Vector3(1, 0.5f, 1);

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        #region Correr con Shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) //Check para el shift
        {
            velocidadActual = velocidadCaminar * valorMultiCorrer;
            estaCorriendo = true;
        }
        else
        {
            velocidadActual = velocidadCaminar;
            estaCorriendo = false;
        }
        #endregion

        #region Movimiento (Uso velocidadActual)
        if (horizontalInput != 0)
        {
            movement = new Vector3(horizontalInput, 0, 0) * velocidadActual;

            // Lógica para cambiar de orientación
            if (horizontalInput > 0 && !mirandoALaDerecha)
            {
                Girar();
            }
            else if (horizontalInput < 0 && mirandoALaDerecha)
            {
                Girar();
            }
        }
        else
        {
            movement = Vector3.zero;
        }
        #endregion

        #region Detección con Gizmo
        DetectarObjetosConGizmo();
        #endregion

        #region Salto
        if (estaEnUnPISO && Input.GetButtonDown("Jump"))
        {
            playerRb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
        #endregion
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y, playerRb.velocity.z);
    }

    void DetectarObjetosConGizmo()
    {
        // Reinicia los estados antes de la detección
        estaEnSuelo = false;
        estaEnObjetoMovil = false;
        estaEnObjetoRe = false;
        estaEnObjetoVel = false;
        estaEnUnPISO = false;

        Collider[] hitColliders = Physics.OverlapBox(transform.position + gizmoCenter, gizmoSize / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("PlataformaEstatica"))
            {
                estaEnSuelo = true;
                estaEnUnPISO = true;
            }
            else if (hitCollider.CompareTag("ObjetoMovil"))
            {
                estaEnObjetoMovil = true;
                estaEnUnPISO = true;
            }
            else if (hitCollider.CompareTag("ReObjeto"))
            {
                estaEnObjetoRe = true;
                estaEnUnPISO = true;
            }
            else if (hitCollider.CompareTag("ObjetoVel"))
            {
                estaEnObjetoVel = true;
                estaEnUnPISO = true;
            }
            else if (hitCollider.CompareTag("ObjetoTumbarJefe"))
            {
                estaEnSuelo = true;
                estaEnUnPISO = true;
            }
        }
    }

    // Dibujar el Gizmo en el Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + gizmoCenter, gizmoSize);
    }

    // Método para girar el objeto
    void Girar()
    {
        mirandoALaDerecha = !mirandoALaDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;  // Invierte la escala en el eje X para girar el objeto
        transform.localScale = escala;
    }
}
