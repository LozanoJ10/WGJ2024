using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Codigos")]
    public MovimientoPlayer_Controller moveP;
    public ControlReloj relojCode;
    public ValorSlider sliderTime;
    public CircularMovementDetector _MovimientoReloj;

    public CamaraController  ControladorCamara;

    [Header("Booleanos")] // Este booleano esta tambien en el codigo de pausa

    public bool SePauso_Manager;
     [Header("Personaje")]
     public Rigidbody personaje;
     public Animator anim;

    void Awake()
    {
        anim = GameObject.Find("La niña").GetComponent<Animator>();
        moveP = GameObject.FindObjectOfType<MovimientoPlayer_Controller>();
        personaje = GameObject.Find("Jugador").GetComponent<Rigidbody>();
        relojCode=GameObject.FindObjectOfType<ControlReloj>();
        sliderTime=GameObject.FindObjectOfType<ValorSlider>();
        _MovimientoReloj=GameObject.FindObjectOfType<CircularMovementDetector>();
        ControladorCamara=GameObject.FindObjectOfType<CamaraController>();
        

    }
    void Start()
    {
            SePauso_Manager=true;
        StartCoroutine(Inicio());
    }

    // Update is called once per frame
    
    void Update()
    {
        if(SePauso_Manager)
        {
            
            moveP.horizontalInput = 0;
            moveP.enabled=false;
            relojCode.enabled=false;
            sliderTime.enabled=false;
            _MovimientoReloj.enabled=false;
            ControladorCamara.enabled=false;
            personaje.isKinematic=true;
            

        }
        else{
            moveP.enabled=true;
            relojCode.enabled=true;
            sliderTime.enabled=true;
            _MovimientoReloj.enabled=true;
            ControladorCamara.enabled=true;
            personaje.isKinematic=false;


        }
    }

    IEnumerator Inicio() //Esto se encarga de activar los codigos despues de 1.5 segundos (Por la trasicion)
    {
        Debug.Log("Codigo Game Manager");
        yield return new WaitForSeconds(1.5f);
        SePauso_Manager=false;
    }
}

