using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Enemy : MonoBehaviour
{
    public SpriteRenderer GlowEffect;
    public GameObject debuffedobj;
    public GameObject deathAnim;
    public float speed;
    [HideInInspector]
    public float health;
    public Color colour; //to store colour
    public float maxhealth;
    public float damage;
    public float value = 1;
    [HideInInspector]
    public float distanceToEnd = 0f;
    public float size = 0.11f;
    public GameObject floatingtext;
    //Enum variable for selective aiming? - based on tower enum, if implemented.
    public Tower.colorAim enemyColor;
    public bool debuffed = false;
    private float maxspeed;
    public void Start()
    {
        health = maxhealth;
        colour = GetComponent<SpriteRenderer>().color; // retrieves colour of sprite
        CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = (size * transform.localScale.x);
        Rigidbody2D rb;
        colour = GetComponent<SpriteRenderer>().color;
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
        maxspeed = speed;
    }

    public void Update()
    {
        if (debuffedobj != null)
        {
            debuffedobj.SetActive(debuffed);
        }
    }


    public IEnumerator slowDur(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        debuffed = false;
        speed = maxspeed;
    }

    public virtual void OnDeath() 
    {
        floatingtext.GetComponent<TextMeshPro>().text = "+" + value;
        GameObject txt = Instantiate(floatingtext, transform.position + new Vector3(0f, 0.2f), Quaternion.Euler(0,0,0), null);
        txt.transform.SetParent(gameObject.transform.parent);
        
        Waves.singleton.allEnemies.Remove(gameObject);
        if (ButtonStatUpgrade.singleton.twr)
        {
            ButtonStatUpgrade.singleton.UpdateText();
        }
        GameObject ph = Instantiate(deathAnim, gameObject.transform.position, Quaternion.identity);
        ph.transform.parent = Waves.singleton.waveController.transform;
        HUDManager.singleton.money += value;
        HUDManager.singleton.saturation += HUDManager.singleton.incBy;
        Destroy(gameObject); // TODO: leave stationary money object (light?) <- might not do this
    }

    public virtual void TakeInstantDamage(float dmg)
    {
        health -= dmg; // TODO: change to colour-based damage
        UpdateColour();
        if (health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void TakeDamage(Collider2D collider)
    {
        Projectile proj = collider.GetComponent<Projectile>();
        //Choice to make: Projectile gets destroyed on hit(lead bloons), projectile continues on(camo bloons), projectile "Hits" an enemy and gets it pierced decreased but doesnt do anything.
        //hit and pierce decrease style
        /*        if (proj.canHit != enemyColor)
                {
                    if (proj.pierced >= proj.pierce) {
                        Destroy(collider.gameObject);
                    }
                    proj.pierced++;
                    return;
                }*/
        //lead style
        if (proj.canHit != enemyColor)
        {
            Destroy(collider.gameObject);
            return;
        }
        //camo style.
/*        if(proj.canHit == enemyColor)
        {
            health -= proj.dmg; // Updated TODO: Added multiplier if colour matches.
            UpdateColour();
            proj.pierced++;
            if (proj.pierced >= proj.pierce)
            {
                Destroy(collider.gameObject);
            }
            if (health <= 0)
            {
                OnDeath();
            }
        }*/

        // Could make it such that if Tower.colorAim is same here, then take no damage etc;
        //How it works is: if proj.colour is same as colour on enemy, do proj.dmg *2 else do proj.dmg.
        health -= proj.dmg; // Updated TODO: Added multiplier if colour matches.
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
        // TODO: need to change actual colour based on stats and colour of tower that shot it
        //^ Do you mean of the enemy? or of the projectile?
        if(GlowEffect != null)
        GlowEffect.color = new Color(GlowEffect.color.r, GlowEffect.color.g, GlowEffect.color.b, 1.0f - (health/maxhealth));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(collision);
        }
    }
    public virtual void DoDamage()
    {
        Waves.singleton.allEnemies.Remove(gameObject);
        HUDManager.singleton.saturation += HUDManager.singleton.incBy;
        HUDManager.singleton.health -= damage;
        Destroy(gameObject);
    }

}
