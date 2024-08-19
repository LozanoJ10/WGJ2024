using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class LU_SoundManager : MonoBehaviour
{
   
    [Header("musica")]

   [SerializeField] private AK.Wwise.Event musicaMenu;

   [SerializeField] private AK.Wwise.Event playMusicaFondo;
    [SerializeField] private AK.Wwise.Event stopMusicaFondo;

    [Header("sfx")]
    [SerializeField] private AK.Wwise.Event playReboninar;
    [SerializeField] private AK.Wwise.Event stopReboninar;
    [SerializeField] private AK.Wwise.Event seleccionMenu;
    [SerializeField] private AK.Wwise.Event playFader;
    [SerializeField] private AK.Wwise.Event playSelect;
    [SerializeField] private AK.Wwise.Event playSeleccionarObjMov;
    [SerializeField] private AK.Wwise.Event stopSeleccionarObjMov;
    [SerializeField] private AK.Wwise.Event playSeleccionarObjTie;
  


   public void Start ()
   {
    PlayMusicaFondo(); 
   }
   public void Update ()
     {
        if (Input.GetKeyDown(KeyCode.B))
        { 
            Debug.Log("probando");
            PlayFader();    
        }
    }

    // DEL AMBIENTE
    public void PlayMusicaFondo()
    {
        Debug.Log("LU :: Aqui Musica Fondo");
        playMusicaFondo.Post(gameObject);
    }

     public void StopMusicaFondo()
    {
        Debug.Log("LU :: Aqui Musica Fondo");
        stopMusicaFondo.Post(gameObject);
    }
    public void MusicaMenuPausa()
    {
        Debug.Log("LU :: Aqui Musica Pausa");
    }

    // DEL JUGADOR

    public void SonidoPasos()
    {
        Debug.Log("LU :: Aqui Sonido Pasos");
    }

    public void SonidoSalto()
    {
        Debug.Log("LU :: Aqui Sonido Salto");
    }

    public void SonidoManipularObjetos()
    {
        Debug.Log("LU :: Aqui Sonido Manipular Objetos");
    }

    // DEL MENU

    public void SonidoMenuDarClicBotones()
    {
        Debug.Log("LU :: Aqui Sonido SONIDO General para seleccionar opciones Menu o boton de pause");

        seleccionMenu.Post (gameObject);
    }

    public void PlayFader()
    {
        Debug.Log("pruebafader");

        playFader.Post (gameObject);
    }

    public void PlaySelect()
    {
        Debug.Log("pruebaSelect");

        playSelect.Post (gameObject);
    }


    // DE MECANICAS

    public void SonidoSeleccionObjeto()
    {
        Debug.Log("LU :: Aqui Sonido Seleccion Objetos Moviles");
        playSeleccionarObjMov.Post(gameObject);
    }
    public void StopSeleccionObjeto()
    {
        Debug.Log("LU :: Aqui Sonido Seleccion Objetos Moviles");
        stopSeleccionarObjMov.Post(gameObject);
    }
    public void SonidoSeleccionObjetoTiempo()
    {
        playSeleccionarObjTie.Post(gameObject);
    }
   
    public void SonidoUsarSlider()
    {
        Debug.Log("LU :: Aqui Sonido Usar Slider - Objetos Moviles");
    }

    public void PlaySonidoRetrocederTiempo()
    {
        Debug.Log("LU :: Aqui Sonido Retroceder Tiempo");
        // Este funcionara mientras se esta usando la mecancia, supongo que la forma seria darle un STOP cuando deje de usarse
        // En el codigo que ejecuta este comnado, coloque el punto donde se debe pausar. TAMBIEN EN LA CONSOLA te apunta donde.
       
        playReboninar.Post(gameObject);
    }
     public void StopSonidoRetrocederTiempo()
    {
        Debug.Log("LU :: Aqui Sonido Retroceder Tiempo");
       
        stopReboninar.Post(gameObject);
    }

    // DE ENEMIGO JEFE
    // Estos son unos collider en la escena. El Jugador lo atraviesa y suceden las acciones del JEFE

    public void MusicaAparicionJefe()
    {
        Debug.Log("LU :: Aqui Sonido Encontrarse con Jefe");
    }

    public void SonidoRetenerEnemigo()
    {
        Debug.Log("LU :: Aqui Sonido - Al darle click al enemigo lo retiene y si sostiene durante 3 segundos, lo destruye.");
    }
    public void SonidoRomperEscudo()
    {
        Debug.Log("LU :: Aqui Sonido Romper Escudos o burbujas creo?");
    }

    public void SonidoRomperObjetos()
    {
        Debug.Log("LU :: Aqui Sonido Romper objetos (rocas que lanza el jefe)");
    }

    // DE ENEMIGO MINION
    // Sera al darle click y sostenerlo para matarlo.

    public void AtacarEnemigos()
    {
        Debug.Log("LU :: Aqui Sonido Atacar Enemigo / Romper Escudo");
    }

}
