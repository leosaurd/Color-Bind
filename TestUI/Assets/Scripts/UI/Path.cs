using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Path : MonoBehaviour
{
	// Singleton to self for access without direct variable
	public static Path instance;

	public int routes; // number of routes - set from number of "Start" nodes (tags set manually)

	public List<int> routeStart = new List<int>(); // index in Path of start nodes (tags set manually)

	// The size for the colliders that disallow the towers from being placed on the path
	public float colliderSize = 0.5f;

	// Assign singleton
	private void Awake()
	{
		instance = this;
	}


    // List of nodes that make up the path
    [HideInInspector]
	public List<GameObject> path = new List<GameObject>();


#if (UNITY_EDITOR)
	// Function called by unity to draw the gizmos for displaying the squares and lines
	private void OnDrawGizmos()
	{
		for (int i = 0; i < path.Count; i++)
		{
			if (i != 0)
			{
				// Draw a line from previous point to current point
				Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
			}

		}

		if (path.Count > 0)
		{
			// Display start and finish labels
			Handles.Label(path[0].transform.position, "Start");
			Handles.Label(path[path.Count - 1].transform.position, "Finish");
		}

	}
#endif
}

#if (UNITY_EDITOR)
// Custom inspector
[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
	// Function called by unity to display in inspector
	public override void OnInspectorGUI()
	{
		// Show default inspector
		base.DrawDefaultInspector();

		// Reference to parent script
		Path pathScript = (Path)target;

		// Begin a horizontal row
		GUILayout.BeginHorizontal();

		// Draw a button and if it's clicked
		if (GUILayout.Button("Add Node"))
		{
			// Create a new blank gameObject with the name "Node x"
			GameObject node = new GameObject($"Node {pathScript.path.Count + 1}");

			// If it's not the first node set it's position to the previous nodes position
			if (pathScript.path.Count > 0) node.transform.position = pathScript.path[pathScript.path.Count - 1].transform.position;
			else node.transform.position = Vector3.zero;

			// Add the node script to the node
			node.AddComponent<Node>();
			// Add a circle collider to the node and save it as a variable
			CircleCollider2D circleCollider = node.AddComponent<CircleCollider2D>();
			circleCollider.radius = pathScript.colliderSize / 2;

			node.layer = 9;
			node.transform.SetParent(pathScript.transform);

			// Add it to the list of nodes
			pathScript.path.Add(node);

			// Create a new gameobject for the collider
			GameObject collider = new GameObject("Collider");

			// Same as above pretty much
			if (pathScript.path.Count > 0) collider.transform.position = pathScript.path[pathScript.path.Count - 1].transform.position;
			else collider.transform.position = Vector3.zero;
			BoxCollider2D boxCollider = collider.AddComponent<BoxCollider2D>();
			boxCollider.size = new Vector2(pathScript.colliderSize, pathScript.colliderSize);

			collider.transform.SetParent(node.transform);
			collider.layer = 9;

			//newcode here

		}

		//add code for split node path traversal?

		if (pathScript.path.Count > 0)
		{
			if (GUILayout.Button("Remove Node"))
			{
				// Must use DestroyImmediate rather than Destroy if not called from runtime, because we aren't in play mode no runtime
				DestroyImmediate(pathScript.path[pathScript.path.Count - 1]);
				// Remove from list
				pathScript.path.RemoveAt(pathScript.path.Count - 1);
			}
		}



		GUILayout.EndHorizontal();

		if (pathScript.path.Count > 1)
		{
			if (GUILayout.Button("Generate Collider"))
			{
				for (int i = 0; i < pathScript.path.Count; i++)
				{
					GameObject currentNode = pathScript.path[i];
					CircleCollider2D circleCollider = currentNode.GetComponent<CircleCollider2D>();
					BoxCollider2D collider = currentNode.GetComponentInChildren<BoxCollider2D>();
					GameObject currentCollider = collider.gameObject;
					if (i != pathScript.path.Count - 1 && !pathScript.path[i].CompareTag("End")) // also excludes end of route within path
					{

						if (pathScript.path[i].CompareTag("Start")) {
							pathScript.routeStart.Add(i); // add current node to routeStart list if it is a Start node
						}
						// Find the directed between the current node and next node
						Vector3 dir = pathScript.path[i + 1].transform.position - currentNode.transform.position;
						float distance = dir.magnitude;

						// Normalize
						Vector3 normalDir = dir / distance;

						// Set its position to the middle between both nodes
						currentCollider.transform.localPosition = (normalDir * distance / 2);

						// Scale the collider to fit the distance between the nodes
						collider.size = new Vector2(distance, pathScript.colliderSize);
						circleCollider.radius = pathScript.colliderSize / 2;

						// Find the hypotenuse and adjacent
						float hyp = normalDir.magnitude;
						float adj = new Vector3(normalDir.x, 0, normalDir.z).magnitude;

						// Calculate the angle by finding the arc cos between adj and hyp then convert from radians
						float angle = Mathf.Acos(adj / hyp) * 180 / Mathf.PI;

						// pretty makes an unsigned angle (0 -> 180) into a signed angle (-180 -> 180)
						// kinda funky, but works
						if (dir.y < 0) angle *= -1;
						if (dir.x < 0) angle *= -1;

						// Set the rotation of the collider object
						// Quaternion.Euler pretty much turns a vector3 into a Quaternion used for rotaiton
						currentCollider.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
					}
					else
					{
                        if (pathScript.path[i].CompareTag("End")) { // add to routes count for each End node
							pathScript.routes++; //should find a way to restrict this so that it doesn't accumulate as colliders are generated again and again
                        }
						circleCollider.radius = pathScript.colliderSize / 2;
						collider.size = new Vector2(pathScript.colliderSize / 2, pathScript.colliderSize / 2);
					}

				}

			}
		}
	}
}
#endif