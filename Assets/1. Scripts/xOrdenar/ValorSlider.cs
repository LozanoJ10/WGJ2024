using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de tener acceso a la UI para trabajar con sliders

public class ValorSlider : MonoBehaviour
{
    public Slider slider; // Referencia al slider
    public float returnSpeed = 0.1f; // Velocidad a la que regresa el valor a 1
    public float delayBeforeReturn = 2f; // Tiempo de espera antes de que comience a regresar

    private float timeSinceLastChange;

    void Start()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        // Desactivar la navegación para evitar control con teclado
        Navigation nav = slider.navigation;
        nav.mode = Navigation.Mode.None;
        slider.navigation = nav;
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        // Si el slider no está en 1 y ha pasado el tiempo de espera
        if (slider.value != 1 && timeSinceLastChange >= delayBeforeReturn)
        {
            // Interpola el valor del slider hacia 1
            slider.value = Mathf.Lerp(slider.value, 1f, returnSpeed * Time.deltaTime);
        }
    }

    // Método para resetear el temporizador cuando el valor del slider cambia
    public void OnSliderValueChanged()
    {
        timeSinceLastChange = 0f;
    }
}
