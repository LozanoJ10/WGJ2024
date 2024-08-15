using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final_Juego : MonoBehaviour
{
    public Animator transicion;
    public string finalEscene;

    void Start()
    {
        transicion = GameObject.Find("Canvas_Menus").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transicion.SetBool("EscenaFinal", true);
            StartCoroutine(iraEscenaFinal());
        }
    }

    IEnumerator iraEscenaFinal()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(finalEscene);
    }
}
