using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
	public Rigidbody2D rb;

	private EnemyBase controller;

	private GameObject currentNode;
	private GameObject nextNode;

	public bool reachedEnd = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		currentNode = Path.instance.path[0];
		nextNode = Path.instance.path[1];

		controller = GetComponent<EnemyBase>();
		transform.position = Path.instance.path[0].transform.position;
	}


	private void FixedUpdate()
	{
		if(!reachedEnd) MoveToNextNode();

		controller.distanceToEnd = CalculateDistanceToEnd();
	}

	float CalculateDistanceToEnd()
	{
		float distance = 0;
		for(int i = Path.instance.path.IndexOf(nextNode); i < Path.instance.path.Count; i++)
		{
			distance += Path.instance.path[i].GetComponent<Node>().distanceToNextNode;
		}
		Vector3 relative = nextNode.transform.position - transform.position;
		distance += relative.magnitude;
		return distance;
	}

	void MoveToNextNode()
	{
		Vector3 relativeVector = nextNode.transform.position - transform.position;
		float distance = relativeVector.magnitude;
		Vector3 directionToNextNode = relativeVector / distance;

		if (distance <= 0.05f * controller.stats.speed / 500){
			currentNode = nextNode;
			if(Path.instance.path.IndexOf(currentNode) == Path.instance.path.Count - 1){
				reachedEnd = true;
				controller.DoDamage();
				return;
			}
			nextNode = Path.instance.path[Path.instance.path.IndexOf(nextNode) + 1];
			return;
		}

		rb.AddForce(directionToNextNode * controller.stats.speed);

	}
}