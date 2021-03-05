using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
	private GameObject healthBar;
	private GameObject background;
	private EnemyBase controller;
	Vector3 initialScale;

	private void Awake()
	{
		controller = transform.parent.GetComponent<EnemyBase>();
		healthBar = transform.GetChild(0).gameObject;
		background = transform.GetChild(1).gameObject;
		initialScale = healthBar.transform.localScale;

	}

	private void Update()
	{
		float percent = controller.stats.health / controller.stats.maxHealth;
		healthBar.transform.localScale = new Vector3(initialScale.x * percent, initialScale.y, initialScale.z);
	}
}
