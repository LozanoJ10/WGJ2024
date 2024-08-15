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
    void Awake()
    {
        menuOpciones= GameObject.Find("Panel_MenuOpciones");
    }

    void Start()
    {
        anim=GetComponent<Animator>();
        menuOpciones.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReproducirAnim()
    {
        anim.SetBool("Empieza", true);
    }
    public void Jugar()
    {
        SceneManager.LoadScene(escenaJugar);
    }
    public void Opciones()
    {
        menuOpciones.SetActive(true);
    }
    public void Salir()
    {
        Application.Quit();
    }
}
