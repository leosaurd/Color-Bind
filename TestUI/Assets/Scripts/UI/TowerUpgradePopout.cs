using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradePopout : MonoBehaviour
{
    public bool isOut = false;
    public int slideRate = 1;

    // Start is called before the first frame update
    public static TowerUpgradePopout singleton;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        StartCoroutine(MoveSlider(slideRate));
    }

    // Update is called once per frame
    void Update()
    {
        //move right slider out, based on tower placer.
        //Debug.Log(GetMousePos());
        if (UpgradeMenu.singleton.tower)
        {
            if (!isOut && UpgradeMenu.singleton.tower.isSelected)
            {
                isOut = true;
                StartCoroutine(MoveSlider(-slideRate));
            }
            else if (isOut && !UpgradeMenu.singleton.tower.isSelected)
            {
                isOut = false;
                StartCoroutine(MoveSlider(slideRate));

            }
        }
    }

    public void ClosePopout()
    {
        UpgradeMenu.singleton.tower.isSelected = false;
        UpgradeMenu.singleton.tower.setRangeVisible(false);
        isOut = false;
        StartCoroutine(MoveSlider(slideRate));
    }

    public IEnumerator MoveSlider(int s)
    {
        for (int i = 0; i < (GetComponent<RectTransform>().rect.width / (Mathf.Abs(s))) /* GetComponentInParent<Canvas>().scaleFactor*/; i++)
        {
            transform.position -= new Vector3(s, 0, 0);
            yield return new WaitForSecondsRealtime(0f);
        }
    }
}
