using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    private void OnDrawGizmos()
    {
		Gizmos.color = new Color(0, 1, 0, 0.5f);

		Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f));
	}

    public float distanceToNextNode = 0f;
    private void Start()
    {
        Path path = Path.instance;
        int index = path.path.IndexOf(gameObject);
        if (path.path[path.path.Count - 1] != gameObject)
        {
            GameObject nextNode = path.path[index + 1];
            Vector3 relative = nextNode.transform.position - transform.position;
            distanceToNextNode = relative.magnitude;
        }
    }
}
