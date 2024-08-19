using UnityEngine;

public class ChangeTransparencyWithTexture : MonoBehaviour
{
    public GameObject targetObject; // El objeto al que quieres cambiar la transparencia
    public float transparency = 0.5f; // Valor de transparencia entre 0 (completamente transparente) y 1 (completamente opaco)

    void Start()
    {
        // Acceder al material del objeto
        Material material = targetObject.GetComponent<Renderer>().material;

        // Si el shader utiliza una textura con alfa
        if (material.HasProperty("_MainTex"))
        {
            Texture2D texture = material.mainTexture as Texture2D;
            if (texture != null)
            {
                Color[] pixels = texture.GetPixels();
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i].a = transparency; // Modificar el valor alfa de cada pixel
                }
                texture.SetPixels(pixels);
                texture.Apply();
            }
        }
        else
        {
            Debug.LogWarning("El material no tiene la propiedad _MainTex.");
        }
    }
}
