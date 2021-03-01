using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Beacon : MonoBehaviour
{
    public SpriteMask area;
    public SpriteRenderer alert;
    public int cost;
    public int powerUp; // money gained per interval
    public float interval; // interval in which money is gained
    public KeyCode activate = KeyCode.Space;
    bool lit = false;
    float lastTime = 0f;
    public GameObject floatingtext;

    // Start is called before the first frame update
    void Start()
    {
        HUDManager.singleton.beacons++; //Hope this works
    }

    //Lights beacon if player presses space whilst inside trigger area
    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player" && !lit) { //might have to be other.parent.tag
            if(HUDManager.singleton.money < cost)
            {
                return;
            }
            alert.enabled = !lit;
            if (Input.GetKeyDown(KeyCode.Space) && HUDManager.singleton.money >= cost) {
                lit = true;
                HUDManager.singleton.litBeacons++;
                HUDManager.singleton.money -= cost; //subtract cost from total money
                if (HUDManager.singleton.tut)
                {
                    TutorialText.singleton.ChangeText();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        alert.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime >= interval && Time.timeScale != 0f) { //check if at least 1 second has passed since last time
            lastTime = Time.time;
            if (lit && HUDManager.singleton.wavesRunning) {
                floatingtext.GetComponent<TextMeshPro>().text = "+" + powerUp;
                GameObject txt = Instantiate(floatingtext, transform.position + new Vector3(0f, 0.2f), Quaternion.Euler(0, 0, 0), null);
                txt.transform.SetParent(gameObject.transform.parent);
                HUDManager.singleton.money += powerUp;
            }
        }
        area.enabled = lit;
    }
}
