using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ArregloOnDrag : MonoBehaviour, IDragHandler
{
    // Este evento aparecerá en el inspector
    [SerializeField] private UnityEvent<PointerEventData> onDragEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
        
    /*public void OnPointerDown(PointerEventData eventData)
    {
        if (onDragEvent != null)
        {
            onDragEvent.Invoke(eventData);
        }
    }*/

    public void OnDrag(PointerEventData eventData)
    {
        if (onDragEvent != null)
        {
            onDragEvent.Invoke(eventData);
        }
    }
}


