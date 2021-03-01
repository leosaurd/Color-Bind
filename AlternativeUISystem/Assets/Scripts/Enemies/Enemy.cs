using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public float health;
    public Color colour; //to store colour
    public float maxhealth;
    public float damage;
    [HideInInspector]
    public float distanceToEnd = 0f;
    public float size = 0.11f;

    public GameObject floatingtext;
    public void Start()
    {
        health = maxhealth;
        colour = GetComponent<SpriteRenderer>().color; // retrieves colour of sprite
        CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = (size * transform.localScale.x);
        Rigidbody2D rb;

        gameObject.layer = 11;
        if (!GetComponent<Rigidbody2D>())
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.mass = 1f;
            rb.drag = 150f;
            rb.angularDrag = 0.05f;
        }

    }

    public virtual void OnDeath() 
    {
        Instantiate(floatingtext, transform.position + new Vector3(0f, 0.2f), Quaternion.Euler(0,0,0), null);
        Waves.singleton.allEnemies.Remove(gameObject);
        Destroy(gameObject);
        //addmoney
        HUDManager.singleton.money += 1;
    }

    public virtual void TakeInstantDamage(float dmg)
    {
        health -= dmg;
        UpdateColour();
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(Collider2D collider)
    {
        Projectile proj = collider.GetComponent<Projectile>();
        health -= proj.dmg;
        UpdateColour();
        proj.pierced++;
        if (proj.pierced >= proj.pierce) {
            Destroy(collider.gameObject);
        }
        if(health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void UpdateColour() {
        GetComponent<SpriteRenderer>().color = new Color(colour.r, colour.g, colour.b, colour.a * (health / maxhealth));// decrease alpha
        colour = GetComponent<SpriteRenderer>().color; // retrieve new colour
        //need to change actual colour based on stats and colour of tower that shot it
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(collision);
        }
    }
    public void DoDamage()
    {
        Waves.singleton.allEnemies.Remove(gameObject);
        Destroy(gameObject);
        HUDManager.singleton.health -= 1;
    }
}
