using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoveFloatingText : MonoBehaviour
{

    TextMeshPro tmp;    
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        StartCoroutine(killmenow());

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            transform.position += new Vector3(0, 0.01f, 0);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - 0.01f);
        }
        
    }

    IEnumerator killmenow()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
    }

}
