using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public TowerVars stats;

    public string s = "name";
    [HideInInspector]
    public GameObject enemy;
    public GameObject shot;
    public readonly float f = 0.5f;
    [HideInInspector]
    public bool canFire = true;
    public bool isSelected = false;
    public RangeIndicator rangeIndicator;
    public void Awake()
    {
        rangeIndicator = GetComponentInChildren<RangeIndicator>();
    }
    public void Start()
    {
        if (!GetComponent<CircleCollider2D>())
        {
            CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = stats.size;
        }
    }

    public void setRangeVisible(bool s)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = s;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(f, f, f, f/2);
        Gizmos.DrawSphere(transform.position, stats.range.value);
    }
    public virtual void FindTarget()
    {

        GameObject[] enemies = Waves.singleton.allEnemies.ToArray();
        GameObject furthestEnem = null;
        for (int i = 0; i < enemies.Length; i++)
        {

            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if(distance < stats.range.value)
            {
                if (furthestEnem == null || furthestEnem.GetComponent<Enemy>().distanceToEnd > enemies[i].GetComponent<Enemy>().distanceToEnd)
                {
                    furthestEnem = enemies[i];
                }
            }

        }
        enemy = furthestEnem;
    }
    public virtual void Fire()
    {
        if (gameObject.layer == 8 && Time.timeScale != 0) {
            Vector3 relativeVector = enemy.transform.position - transform.position;
            float distance = relativeVector.magnitude;
            Vector3 normalVector = relativeVector / distance;
            float hyp = normalVector.magnitude;
            float adj = new Vector3(normalVector.x, 0, normalVector.z).magnitude;
            float angle = Mathf.Acos(adj / hyp) * 180 / Mathf.PI;



            if (relativeVector.y < 0) angle *= -1;
            if (relativeVector.x < 0) angle *= -1;
            GameObject localshot = Instantiate(shot, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            localshot.GetComponent<Projectile>().dmg = stats.damage.value;
            localshot.GetComponent<Projectile>().pierce = stats.pierce.value;
            Rigidbody2D rb = localshot.GetComponent<Rigidbody2D>();
            rb.velocity = normalVector * stats.bulletspeed;
            StartCoroutine(FireRate());
        }

    }

    public IEnumerator FireRate()
    {
        canFire = false;
        yield return new WaitForSeconds(stats.fireRate.value);
        canFire = true;
    }

    public virtual void Sell()
    {

    }

    //This holds the initial values of the stats
    [System.Serializable]
    public struct TowerVars
    {
        [Header("NOTICE: These are initialized in the script, any editing here before play will have no effect")]
        public Stat fireRate;
        public Stat damage;
        public Stat range;
        [Space(20)]
        public Stat pierce;
        public float price;
        public float bulletspeed;
        public float size;
        public float colour;
    }

    
}

[System.Serializable]    
public class Stat
{
    public Stat(float _value, int _maxLevel, float _incrementBy, int _basePrice, Tower _parentScript)
    {
        baseValue = _value;
        value = _value;
        level = 0;
        maxLevel = _maxLevel;
        incrementBy = _incrementBy;
        basePrice = _basePrice;
        upgradeCost = _basePrice;
        parentScript = _parentScript;
    }
    public Tower parentScript;
    public float baseValue;
    public float value;
    public int level;
    public int maxLevel;
    public float incrementBy;
    public int basePrice;
    public int upgradeCost;

    public void Upgrade()
    {
        level++;
        HUDManager.singleton.money -= upgradeCost;
        value += incrementBy;
        parentScript.rangeIndicator.refrange();
        upgradeCost += Mathf.RoundToInt((basePrice / 2f) * level);
    }
}