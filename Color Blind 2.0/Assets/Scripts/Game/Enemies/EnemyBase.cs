using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trent;
public abstract class EnemyBase : MonoBehaviour
{
	public float distanceToEnd;

	public enemyStats stats = new enemyStats();

	[System.Serializable]
	public struct enemyStats
	{
		public float speed;
		public float health;
		public float maxHealth;
		public float damage;
		public int value;
		public TrentUtil.gameColor color;
	}

	private void Awake()
	{
		stats.health = stats.maxHealth;
		gameObject.layer = 11;

		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		sr.color = TrentUtil.GetColor(stats.color);

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();

		rb.gravityScale = 0f;
		rb.interpolation = RigidbodyInterpolation2D.Interpolate;
		rb.mass = 1f;
		rb.drag = 150f;
		rb.angularDrag = 0.05f;
	}


	/// <summary>
	/// This is used to deal damage to the player
	/// </summary>
	public void DoDamage()
	{
		// TODO add money, increase saturation
		Delete();
	}

	/// <summary>
	/// This method is used to instantly delete the enemy for no reward
	/// </summary>
	public virtual void Delete()
	{
		Destroy(gameObject);
	}


	/// <summary>
	/// This method is used to deal damage to the enemy
	/// </summary>
	public virtual void TakeDamage(float damage)
	{
		stats.health -= damage;
		if (stats.health <= 0)
		{
			OnDeath();
		}
	}

	public virtual void OnDeath()
	{
		// TODO add money, increase saturation
		Destroy(gameObject);
	}

}
