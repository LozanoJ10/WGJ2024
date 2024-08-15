using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoCanvasController : MonoBehaviour
{
    public GameObject tuto1;
    public GameObject tuto2;
    public GameObject tuto3;
    public GameObject tuto4;
    public GameObject tuto5;

    [Header("Tutorial 1")]
    public bool tuto1MovHecho = false;
    public bool tuto1SaltoHecho = false;
    public bool tuto1Hecho = false;
    [Header("Tutorial 2")]
    public ObjetoMovilClick_ hizoClickMovil;
    public bool tuto2HizoClick =false;
    [Header("Tutorial 3")]
    public MovimientoObjetoTiempo hizoClickEnMovil;
    public ValorSlider hizoSlider;
    public bool tuto3HizoClick =false;
    public bool tuto3Slider = false;
    public bool tuto3Hecho = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Tuto1
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        { 
            tuto1MovHecho = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            tuto1SaltoHecho= true;        
        }

        if (tuto1MovHecho && tuto1SaltoHecho)
        {
            tuto1Hecho = true;        
        }

        if (tuto1Hecho)
            tuto1.gameObject.SetActive(false);
        #endregion

        #region Tuto2
        if (hizoClickMovil.isDragging)
        { 
            tuto2HizoClick = true;
        }

        if (tuto2HizoClick)
        { 
            tuto2.gameObject.SetActive(false);
        }
        #endregion

        #region Tuto3
        if (hizoClickEnMovil.objetoActivado)
        { 
            tuto3HizoClick = true;
        }
        if (hizoSlider.slider.value > 0.8 || hizoSlider.slider.value < 0.2 && tuto3HizoClick)
        {
            tuto3Slider = true;
        }
        if (tuto3HizoClick && tuto3Slider)
        {
            tuto3Hecho = true;
        }
        if (tuto3Hecho)
        { 
            tuto3.gameObject.SetActive(false);        
        }
        #endregion

    }
}
