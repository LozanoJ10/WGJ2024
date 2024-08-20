using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SonidoSliderTiempo : MonoBehaviour, IDragHandler

{
    [SerializeField] private UnityEvent<PointerEventData> onDragEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void OnDrag(PointerEventData eventData)
    {
        if (onDragEvent != null)
        {
            onDragEvent.Invoke(eventData);
        }
    }


}

