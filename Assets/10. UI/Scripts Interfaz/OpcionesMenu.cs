using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OpcionesMenu : MonoBehaviour
{
    [Header("Panel opciones")]
    /*[SerializeField]private */public GameObject _panelOpciones;
   
    [Header("Configuracion Audio")]
    [SerializeField]private AudioMixer audioConfig;

    [Header("Sliders")]
     [SerializeField]private Slider sliderMusica;
    [SerializeField]private Slider sliderEfectos;

    
    void Start()
    {
       _panelOpciones= GameObject.Find("Panel_MenuOpciones");
        if(PlayerPrefs.HasKey("musicaVolumen"))
        {
            CargarVolumen();
        }
        else{
                AñadirVolumenMusica();
                AñadirVolumenEfectos();
        }
        
        
    }
    
    public void AñadirVolumenMusica()
    {
        float volumen = sliderMusica.value;
        audioConfig.SetFloat("MusicS",Mathf.Log10(volumen)*20);
        PlayerPrefs.SetFloat("musicaVolumen", volumen);
    }
    public void AñadirVolumenEfectos()
    {
        float volumen = sliderMusica.value;
        audioConfig.SetFloat("EfectsS",Mathf.Log10(volumen)*20);
        PlayerPrefs.SetFloat("efectosVolumen", volumen);
    }
    public void CargarVolumen()
    {
        sliderMusica.value= PlayerPrefs.GetFloat("musicaVolumen");
        sliderEfectos.value= PlayerPrefs.GetFloat("efectosVolumen");
        AñadirVolumenMusica();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolverOpciones()
    {
        Debug.Log("Aqui va audio de seleccion");
        _panelOpciones.SetActive(false);
    }
}
