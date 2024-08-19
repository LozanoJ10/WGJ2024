using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    [Header("Mover Nuves Reves")]
    public CircularMovementDetector moverAtras;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        if (moverAtras.dioClick && moverAtras.haciaAtras)
        {
            offset = -velocidadMovimiento * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
        else if (moverAtras.dioClick && !moverAtras.haciaAtras)
        {
            offset = velocidadMovimiento * 2.5f * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
        else if (!moverAtras.dioClick && !moverAtras.haciaAtras)
        {
            offset = velocidadMovimiento * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
    }
}
