using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public AccionSilvido silvido;
    public float animationSpeed = 0; // Velocidad inicial de la animación

    public GameObject loopWarp;
    public bool esTest;

    void Start()
    {
        animator.speed = animationSpeed;
    }

    void Update()
    {

        if (!esTest)
        {
            // Cambia la velocidad de la animación en tiempo de ejecución
            animationSpeed = silvido.valorRecibidoSoplido;
            animator.speed = animationSpeed;
        }
        else
        {
            animator.speed = animationSpeed;
        }


        // Opcional: Controles básicos para cambiar la velocidad usando teclas
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animationSpeed += 0.02f; // Incrementar la velocidad
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animationSpeed -= 0.02f; // Disminuir la velocidad
        }
    }

    public void ActivarSonidoVoz()
    {
        // Obtener el clip de audio desde la biblioteca
        AudioClip sfxClip = AudioLibrary.instance.GetSFXClip("sfx_flor_cantada_larga_01");

        if (sfxClip != null)
        {
            // Crear un GameObject temporal para reproducir el sonido
            GameObject audioObject = new GameObject("GlobalAudio");

            // Añadir un AudioSource al GameObject
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            // Configurar el AudioSource para reproducir el sonido como un sonido global
            audioSource.clip = sfxClip;
            audioSource.volume = 1.0f; // Ajusta el volumen si es necesario
            audioSource.spatialBlend = 0.0f; // 0.0 hace que el sonido no sea 3D, sino 2D (global)
            audioSource.Play();

            // Destruir el GameObject después de que el sonido termine de reproducirse
            Destroy(audioObject, sfxClip.length);
        }
        else
        {
            Debug.LogError("El SFX no se pudo encontrar o cargar.");
        }
    }

    public void DesactivarLoop2()
    { 
        loopWarp.SetActive(false);    
    }
}
