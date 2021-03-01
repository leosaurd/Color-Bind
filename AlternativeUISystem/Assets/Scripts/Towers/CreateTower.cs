using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    public GameObject Tower;
    // Update is called once per frame
    public void OnClick()
    {
        if (Tower != null)
        {
            Tower.transform.localScale = new Vector3(3, 3, Tower.transform.localScale.z);
            Instantiate(Tower, Input.mousePosition, Quaternion.identity);
        }
    }

}
