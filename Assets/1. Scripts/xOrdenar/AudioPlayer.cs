using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void Update()
    {
        // Verifica si la tecla Y ha sido presionada
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Obtener el clip de SFX desde la biblioteca
            AudioClip sfxClip = AudioLibrary.instance.GetSFXClip("Sfx_Selección en el menu");

            if (sfxClip != null)
            {                
                AudioSource.PlayClipAtPoint(sfxClip, Camera.main.transform.position);
            }
            else
            {
                Debug.LogError("El SFX no se pudo encontrar o cargar.");
            }
        }
    }
}

