using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLoop : MonoBehaviour
{
    public GameObject loopWarp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador") && loopWarp.activeSelf)
        { 
            loopWarp.SetActive(false);

            AudioClip sfxClip = AudioLibrary.instance.GetSFXClip("sfx_checkpoint_01");
            AudioSource.PlayClipAtPoint(sfxClip, Camera.main.transform.position);
        }
    }
}
