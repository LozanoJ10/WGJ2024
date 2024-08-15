using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aniamciones_Niña : MonoBehaviour
{
    // Start is called before the first frame update

    public MovimientoPlayer_Controller playerMove;
    public Animator anim;

    void Awake()
    {
        playerMove= GameObject.FindObjectOfType<MovimientoPlayer_Controller>();
    }
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove.estaCorriendo && playerMove.horizontalInput != 0 )
        {
            anim.SetBool("Run",true);
            
        }
        else{
            anim.SetBool("Run",false);
        }


     if(playerMove.horizontalInput != 0 && !playerMove.estaCorriendo )
        {
            anim.SetBool("Walk",true);
        }
        else
        {
            anim.SetBool("Walk",false);
        }
    }
}
