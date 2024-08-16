using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LU_SoundManager : MonoBehaviour
{
    // DEL AMBIENTE
    public void MusicaFondo()
    {
        Debug.Log("LU :: Aqui Musica Fondo");
    }
    public void MisicaMenuPausa()
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
    }

    // DE MECANICAS

    public void SonidoSeleccionObjeto()
    {
        Debug.Log("LU :: Aqui Sonido Seleccion Objetos Moviles");
    }

    public void SonidoUsarSlider()
    {
        Debug.Log("LU :: Aqui Sonido Usar Slider - Objetos Moviles");
    }

    public void SonidoRetrocederTiempo()
    {
        Debug.Log("LU :: Aqui Sonido Retroceder Tiempo");
        // Este funcionara mientras se esta usando la mecancia, supongo que la forma seria darle un STOP cuando deje de usarse
        // En el codigo que ejecuta este comnado, coloque el punto donde se debe pausar. TAMBIEN EN LA CONSOLA te apunta donde.
    }

    // DE ENEMIGO JEFE
    // Estos son unos collider en la escena. El Jugador lo atraviesa y suceden las acciones del JEFE

    public void MusicaAparicionJefe()
    {
        Debug.Log("LU :: Aqui Sonido Encontrarse con Jefe");
    }

    // DE ENEMIGO MINION
    // Sera al darle click y sostenerlo para matarlo.

    public void AtacarEnemigos()
    {
        Debug.Log("LU :: Aqui Sonido Atacar Enemigo / Romper Escudo");
    }

}
