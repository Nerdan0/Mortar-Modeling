using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] RectTransform dragRectTransform;
    [SerializeField] Canvas canvas;
    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    private void Awake()
    {
        if (dragRectTransform == null)
        {
            dragRectTransform = transform.GetComponent<RectTransform>();
        }

        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            while (testCanvasTransform != null)
            {
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas!=null)
                {
                    break;
                }
            }
        }
    }

}
