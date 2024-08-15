using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio_EscenaN1 : MonoBehaviour
{
    // Start is called before the first frame update
    public string escena;



    public void Cambio()
    {
        SceneManager.LoadScene(escena);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
