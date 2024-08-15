using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilvidoController : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    public float loudnessThreshold; // Umbral para considerar que el sonido es "alto"
    public float minimumLoudness; // Umbral mínimo para ignorar valores muy bajos

    private void Start()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        float loudness = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);

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
