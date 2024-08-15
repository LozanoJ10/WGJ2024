using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Pausa : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _PanelPausa,_PanelOpciones,botonPausa;
    [SerializeField] private bool estaPausado;
    [SerializeField] private Animator anim;
    [SerializeField] private string reinicioNivel,menuPrincipal;
    [SerializeField] private Game_Manager _GameManager;

    void Awake()
    {
        _PanelOpciones= GameObject.Find("Panel_MenuOpciones");
        _PanelPausa= GameObject.Find("Panel_Pausa");
         botonPausa= GameObject.Find("BotonPausa");
         _GameManager =GameObject.FindObjectOfType<Game_Manager>();
    }
    void Start()
    {
        anim=GetComponent<Animator>();
        _PanelPausa.SetActive(false);
        _PanelOpciones.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!estaPausado)
            {
                Pausa();
            }
            else
            {
                Reaunudar();
            }
        }
    }

    public void Pausa()
    {
        Debug.Log("Script pausa");
        _GameManager.SePauso_Manager=true;
        estaPausado=true;
        botonPausa.SetActive(false);
        _PanelPausa.SetActive(true);
    }
    public void Reaunudar()
    {
        _GameManager.SePauso_Manager=false;
        estaPausado=false;
        botonPausa.SetActive(true);
        _PanelPausa.SetActive(false);
        _PanelOpciones.SetActive(false);
    }
    public void ReproducirAnimReinicio()
    {
        anim.SetBool("Reinicio", true);
    }
    public void Reinicio()
    {
        SceneManager.LoadScene(reinicioNivel);
    }
    public void Opciones()
    {
        _PanelOpciones.SetActive(true);
    }
    public void VolverDeOpciones()
    {
        _PanelOpciones.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene(menuPrincipal);
    }
    public void ReproducirAnimMenu()
    {
        anim.SetBool("Menu", true);
    }
}
