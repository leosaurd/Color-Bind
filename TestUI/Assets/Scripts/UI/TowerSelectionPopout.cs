using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionPopout : MonoBehaviour
{
    private bool isOut = true;
    public int slideRate = 1;
    private bool stayOut = true;
    //you didnt assign it to the tower menu button.... can you please not. 
    public Button TowerMenuButton;

    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(MoveSlider(-slideRate));
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            stayOut = !stayOut;
            TowerMenuButton.gameObject.SetActive(isOut);
        }
        if (!isOut && Input.GetKeyDown(KeyCode.H) && !TowerPlacer.singleton)
        {
            isOut = true;
            StartCoroutine(MoveSlider(slideRate));
        }
        else if(isOut && (Input.GetKeyDown(KeyCode.H)||TowerPlacer.singleton))
        {
            isOut = false;
            StartCoroutine(MoveSlider(-slideRate));
        }

        if (!TowerPlacer.singleton)
        {
            if (stayOut && !isOut)
            {
                isOut = true;
                StartCoroutine(MoveSlider(slideRate));
            }
        }
    }

    public void Close()
    {
            stayOut = !stayOut;
        
        if (!isOut && !TowerPlacer.singleton)
        {
            isOut = true;
            StartCoroutine(MoveSlider(slideRate));
        }
        else if (isOut  || TowerPlacer.singleton)
        {
            isOut = false;
            StartCoroutine(MoveSlider(-slideRate));
        }

        if (!TowerPlacer.singleton)
        {
            if (stayOut && !isOut)
            {
                isOut = true;
                StartCoroutine(MoveSlider(slideRate));
            }
        }
    }

    IEnumerator MoveSlider(int s)
    {
        for (int i = 0; i < (GetComponent<RectTransform>().rect.width/(Mathf.Abs(s))) /* GetComponentInParent<Canvas>().scaleFactor*/; i++)
        {
            transform.position -= new Vector3(s,0,0);
            yield return new WaitForSecondsRealtime(0f);
        }
    }

    //Changed to hotkey because its easier to use compared to mouse position(if we go for fullscreen aspect).
    public Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, 0f));
        float distance = 0f;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
