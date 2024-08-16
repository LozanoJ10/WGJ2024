using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilvidoController : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    public float loudnessThreshold; // Umbral para considerar que el sonido es "alto"
    public float minimumLoudness; // Umbral mínimo para ignorar valores muy bajos
    public bool isMicrophoneAvailable = false; // Booleano para comprobar si hay dispositivos disponibles
    private string selectedMicrophone; // Micrófono seleccionado para grabar

    private void Start()
    {
        CheckMicrophoneAvailability();

        if (isMicrophoneAvailable)
        {
            MicrophoneToAudioClip();
        }
        else
        {
            Debug.LogWarning("No se detectó ningún micrófono disponible.");
        }
    }

    private void CheckMicrophoneAvailability()
    {
        foreach (string device in Microphone.devices)
        {
            AudioClip testClip = Microphone.Start(device, true, 1, 44100);
            if (testClip != null)
            {
                Debug.Log("Dispositivo disponible: " + device);
                selectedMicrophone = device;
                isMicrophoneAvailable = true;
                Microphone.End(device); // Detenemos la grabación de prueba
                break; // Salimos del bucle después de encontrar el primer dispositivo disponible
            }
            else
            {
                Debug.LogWarning("No se pudo iniciar la grabación con el dispositivo: " + device);
            }
        }
    }

    public void MicrophoneToAudioClip()
    {
        if (isMicrophoneAvailable)
        {
            microphoneClip = Microphone.Start(selectedMicrophone, true, 20, AudioSettings.outputSampleRate);
        }
    }

    public float GetLoudnessFromMicrophone()
    {
        if (!isMicrophoneAvailable)
        {
            Debug.LogWarning("No se puede obtener la intensidad del sonido porque no hay un micrófono disponible.");
            return 0;
        }

        if (microphoneClip == null)
        {
            Debug.LogWarning("No se puede obtener la intensidad del sonido porque no hay un clip de micrófono.");
            return 0;
        }

        float loudness = GetLoudnessFromAudioClip(Microphone.GetPosition(selectedMicrophone), microphoneClip);

        // Ignorar valores de loudness muy bajos
        if (loudness < minimumLoudness)
        {
            loudness = 0;
        }

        // Debug.Log si la intensidad es alta
        if (loudness > loudnessThreshold)
        {
            Debug.Log("Sonido alto detectado: " + loudness);
        }

        return loudness;
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        if (!isMicrophoneAvailable)
        {
            return 0;
        }

        int startPosition = clipPosition - sampleWindow;

        if (startPosition < 0)
            return 0;

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
