using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    public string escenaJugar;
     [SerializeField] private GameObject menuOpciones;
     [SerializeField] private Animator anim;

     public AudioSource menuJuego, botones;
    void Awake()
    {
        menuOpciones= GameObject.Find("Panel_MenuOpciones");
        menuJuego = GameObject.Find("MenuPrincipalAudio").GetComponent<AudioSource>();//Encontrar audios
        botones = GameObject.Find("BotonesAudio").GetComponent<AudioSource>();//Encontrar audios
       
    }

    void Start()
    {
        Debug.Log("Aqui va sonido de menu");
        menuJuego.Play() ; //Reproducir
        anim=GetComponent<Animator>();
        menuOpciones.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReproducirAnim()
    {
       Debug.Log("Aqui va audio de seleccion");
        botones.Play();
        anim.SetBool("Empieza", true);
    }
    public void Jugar()
    {
        SceneManager.LoadScene(escenaJugar);
    }
    public void Opciones()
    {
        Debug.Log("Aqui va audio de seleccion");
        botones.Play();
        menuOpciones.SetActive(true);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
