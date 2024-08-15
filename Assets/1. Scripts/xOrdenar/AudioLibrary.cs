using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public static AudioLibrary instance;

    private Dictionary<string, AudioClip> audioClips;
    private Dictionary<string, AudioClip> sfxClips; // Diccionario para los SFX

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioClips();
            LoadSFXClips(); // Cargar los SFX
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAudioClips()
    {
        audioClips = new Dictionary<string, AudioClip>();

        // Ruta dentro de la carpeta Resources para los audios generales
        string path = "Audio";

        // Obtén todos los archivos de audio en la carpeta
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);

        // Recorre cada clip y agrégalo al diccionario
        foreach (AudioClip clip in clips)
        {
            string clipName = clip.name;
            audioClips.Add(clipName, clip);
            //Debug.Log("AudioClip cargado: " + clipName); // Aquí se imprime cada archivo cargado
        }

        //Debug.Log("Total de AudioClips cargados: " + audioClips.Count); // Imprime el número total de archivos cargados
    }

    private void LoadSFXClips()
    {
        sfxClips = new Dictionary<string, AudioClip>();

        // Ruta dentro de la carpeta Resources para los SFX
        string path = "SFX";

        // Obtén todos los archivos de SFX en la carpeta
        AudioClip[] clips = Resources.LoadAll<AudioClip>(path);

        // Recorre cada clip y agrégalo al diccionario
        foreach (AudioClip clip in clips)
        {
            string clipName = clip.name;
            sfxClips.Add(clipName, clip);
            //Debug.Log("SFX cargado: " + clipName); // Aquí se imprime cada archivo cargado
        }

        //Debug.Log("Total de SFX cargados: " + sfxClips.Count); // Imprime el número total de SFX cargados
    }

    // Método para obtener un audio clip general
    public AudioClip GetAudioClip(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            return audioClips[clipName];
        }
        else
        {
            //Debug.LogError("El clip de audio no existe: " + clipName);
            return null;
        }
    }

    // Método para obtener un SFX
    public AudioClip GetSFXClip(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            return sfxClips[clipName];
        }
        else
        {
            //Debug.LogError("El SFX no existe: " + clipName);
            return null;
        }
    }
}
