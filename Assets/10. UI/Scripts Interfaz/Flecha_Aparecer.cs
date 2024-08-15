using UnityEngine;
using UnityEngine.EventSystems; 

public class Flecha_Aparecer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject flecha;

    void Start()
    {
        flecha.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        flecha.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flecha.SetActive(false);
    }
}
