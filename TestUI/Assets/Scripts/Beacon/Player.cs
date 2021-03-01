using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Camera Camera;

    // Movement speed for player
    float speed = 100;

    // Specified target for player to move to
    Vector3 goal = new Vector3(0f,0f,0f);

    // Player moves without player input - change to false for any cutscenes
    public bool auto = true;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
    }

    void Target(Vector3 target) {
        target.z = transform.position.z;
        if (Vector3.Distance(transform.position, target) > 0.01) {
            goal = target;
            auto = true;
        } else {
            auto = false;
        }
    }

    // Update is called once per frame
    void Update() {
        float movementInputX = Input.GetAxis("Horizontal");
        float movementInputY = Input.GetAxis("Vertical");

        Target(Camera.ScreenToWorldPoint(Input.mousePosition));

        if (auto) {
            transform.position = Vector3.MoveTowards(transform.position, goal, speed);
        }
    }
}
