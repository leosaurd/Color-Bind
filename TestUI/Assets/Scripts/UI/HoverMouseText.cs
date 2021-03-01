using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverMouseText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject s;
    private void Start()
    {
        s.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData PED)
    {
        s.SetActive(true);
    }

    public void OnPointerExit(PointerEventData PED)
    {
        s.SetActive(false);
    }
}
