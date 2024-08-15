using UnityEngine;

public class ControlReloj : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 offset;
    private Vector2 posicionOriginal; // Funcionará como el pivote

    public float fuerzaDeAtraccion = 10f; // Fuerza de atracción hacia el origen
    public float distanciaMaxima = 100f;  // Distancia máxima de arrastre del objeto

    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        posicionOriginal = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out localMousePosition
            );

            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, canvas.worldCamera))
            {
                isDragging = true;
                offset = rectTransform.anchoredPosition - localMousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out localMousePosition
            );
            Vector2 targetPosition = localMousePosition + offset;

            if (Vector2.Distance(posicionOriginal, targetPosition) <= distanciaMaxima)
            {
                rectTransform.anchoredPosition = targetPosition;
            }
            else
            {
                Vector2 direction = (targetPosition - posicionOriginal).normalized;
                rectTransform.anchoredPosition = posicionOriginal + direction * distanciaMaxima;
            }
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, posicionOriginal, fuerzaDeAtraccion * Time.deltaTime);
        }
    }
}
