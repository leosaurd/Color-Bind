using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
	public float distanceToEnd;

	public enemyStats stats = new enemyStats();

	public struct enemyStats
	{
		public float speed;
		public int health;
		public int maxHealth;
		public int damage;
	}

	private void Awake()
	{
		stats.health = stats.maxHealth;
	}


	public void DoDamage()
	{
		// TODO damage the player
	}
}
