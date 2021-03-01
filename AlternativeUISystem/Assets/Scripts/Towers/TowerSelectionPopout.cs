using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionPopout : MonoBehaviour
{
    private bool isOut = false;
    public int slideRate = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveSlider(-slideRate));
    }

    // Update is called once per frame
    void Update()
    {
        //move right slider out, based on tower placer.
        //Debug.Log(GetMousePos());
        if (!isOut && (GetMousePos().x > 6f && !TowerPlacer.singleton))
        {
            isOut = true;
            StartCoroutine(MoveSlider(slideRate));
        }
        else if (isOut && (GetMousePos().x <= 4.5f || TowerPlacer.singleton))
        {
            isOut = false;
            StartCoroutine(MoveSlider(-slideRate));
        }
    }

    IEnumerator MoveSlider(int s)
    {
        for (int i = 0; i < (100/(Mathf.Abs(s))) * GetComponentInParent<Canvas>().scaleFactor; i++)
        {
            transform.position -= new Vector3(s,0,0);
            yield return new WaitForSecondsRealtime(0f);
        }
    }

    public Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, 0f));
        float distance = 0f;
        plane.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
