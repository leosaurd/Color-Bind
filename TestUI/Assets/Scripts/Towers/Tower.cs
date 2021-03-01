using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public GameObject towerbufficon;
    public TowerVars stats;
    public UpgradeLevel upgradelevel;
    public MaxLevel mxlvl;
    public IncrementBy incb;
    public BasePrice bP;
    public string s = "name";
    [HideInInspector]
    public GameObject enemy;
    public GameObject shot;
    public readonly float f = 0.5f;
    [HideInInspector]
    public bool canFire = true;
    public bool isSelected = false;
    public aimMode currentAimMode;
    



    public void Start()
    {
        if (!GetComponent<CircleCollider2D>())
        {
            CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = stats.size;
            //ONLY FOR FORCING COLOR VALUES, DISABLE IF PROJECTILE AND TOWER WANTS DIFF COLOURS.
            stats.colour = GetComponent<SpriteRenderer>().color;
        }
    }

    public virtual void Update()
    {
        if (towerbufficon != null)
        {
            towerbufficon.SetActive(stats.buffed);
        }
        if (gameObject.layer == 8)
        {
            FindTarget();
            if (enemy != null && canFire)
            {
                //Debug.Log(enemy);
                Fire();
            }
        }

    }

    public void setRangeVisible(bool s)
    {
        if (this != null) {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = s;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(f, f, f, f/2);
        Gizmos.DrawSphere(transform.position, stats.range);
    }
    public virtual void FindTarget()
    {

        GameObject[] enemies = Waves.singleton.allEnemies.ToArray();
        GameObject currentEnem = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            //if within range and enemy is not same color < this if statement can be altered as needed
            if (distance < stats.range && enemies[i].GetComponent<Enemy>().enemyColor == stats.target)
            {
                switch (currentAimMode)
                {
                    case aimMode.Last: 
                        currentEnem = aimModeLast(enemies[i], currentEnem);
                        break;
                    case aimMode.First:
                        currentEnem = aimModeFirst(enemies[i], currentEnem);
                        break;
                    case aimMode.Close:
                        currentEnem = aimModeClose(enemies[i], currentEnem);
                        break;
                    case aimMode.Strong:
                        currentEnem = aimModeStrongest(enemies[i], currentEnem);
                        break;
                    default:
                        currentEnem = aimModeLast(enemies[i], currentEnem);
                        break;
                }
            }

        }
        enemy = currentEnem;
    }

    GameObject aimModeLast(GameObject a, GameObject b)
    {
        if (b == null || b.GetComponent<Enemy>().distanceToEnd < a.GetComponent<Enemy>().distanceToEnd)
        {
            return a;
        }
        return b;
    }

    GameObject aimModeFirst(GameObject a, GameObject b)
    {
        if (b == null || b.GetComponent<Enemy>().distanceToEnd > a.GetComponent<Enemy>().distanceToEnd)
        {
            return a;
        }
        return b;
    }

    GameObject aimModeClose(GameObject a, GameObject b)
    {
        if (b != null)
        {
            float adist = Vector3.Distance(transform.position, a.transform.position);
            float bdist = Vector3.Distance(transform.position, b.transform.position);
            if (adist < bdist) return a;
        }
        else
        {
            return a;
        }
        return b;
    }
    GameObject aimModeStrongest(GameObject a, GameObject b)
    {
        if (b == null || a.GetComponent<Enemy>().maxhealth > b.GetComponent<Enemy>().maxhealth)
        {
            return a;
        }
        return b;
    }
    public virtual void Fire()
    {
        if (Time.timeScale != 0 ) {
            Vector3 relativeVector = enemy.transform.position - transform.position;
            float distance = relativeVector.magnitude;
            Vector3 normalVector = relativeVector / distance;
            float hyp = normalVector.magnitude;
            float adj = new Vector3(normalVector.x, 0, normalVector.z).magnitude;
            float angle = GetAngle(Vector3.up + transform.position, transform.position, enemy.transform.position);
            createShot(normalVector, angle, 0);
            StartCoroutine(FireRate());
        }

    }

    public static float GetAngle(Vector2 A, Vector2 B, Vector2 C)
    {
        return Vector2.SignedAngle(A - B, C - B);
    }
    public virtual void createShot(Vector3 normalVector, float angle,float offset)
    {
        GameObject basicShot = Instantiate(shot, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + offset)));
        basicShot.transform.parent = Waves.singleton.shotHolder.transform;
        Projectile ps = basicShot.GetComponent<Projectile>();
        ProjStats(ps);
        Rigidbody2D rb = basicShot.GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(offset, new Vector3(0, 0, Mathf.Abs(angle))) * normalVector * stats.bulletspeed;
    }
    public virtual void ProjStats(Projectile ps)
    {
        ps.dmg = stats.damage;
        ps.pierce = stats.pierce;
        ps.shotSpeed = stats.bulletspeed;
        //for being able to "hit/deal damage"
        ps.canHit = stats.target;
        ps.colour = stats.colour;//TODO colour thingy done ^_^
    }

    public IEnumerator FireRate()
    {
        canFire = false;
        float maxval = 100;//value to determine if we do this iteration
        if (stats.fireRate > 0.2f)//if the fire rate value exceeds framerate iteration?
        {
            for (float i = 0; i < maxval; i++)
            {
                //if (stats.fireRate > 0.2f)
                GetComponent<SpriteRenderer>().color = Color.Lerp(Color.gray, stats.colour, i / maxval);
                yield return new WaitForSeconds(stats.fireRate / maxval);
            }
        } else
        {
            yield return new WaitForSeconds(stats.fireRate);
        }
        
        canFire = true;
    }

    public virtual void Sell()
    {
        HUDManager.singleton.money += Mathf.Round(stats.price/2);
        isSelected = false;
        setRangeVisible(false);
        CreateTower.singleton.allTowers.Remove(gameObject);
        Destroy(gameObject);
    }

    public virtual void Upgrade(ref float stat, ref float bp, int mxlvl, ref int ul, float incb)
    {
        if (HUDManager.singleton.money >= bp && mxlvl > ul)
        {
            stat += incb;
            HUDManager.singleton.money -= bp;
            ul++;
            stats.price += bp;
            //Upgrade cost formula down here(i aint great at math so someone figure this out already)
            bp = Mathf.Round(bp += (200 * ul));
        }
        else
        {
            //just to say that we're really poors.
            Debug.Log("You're poor");
        }
        ButtonStatUpgrade.singleton.UpdateText();
    }
    //Just extra stuff cuz im actually bored lol.
    public virtual void ColorChange()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        switch (stats.target)
        {
            case colorAim.Blue:
                s.color = Color.blue;
                break;
            case colorAim.Red:
                s.color = Color.red;
                break;
            case colorAim.Green:
                s.color = Color.green;
                break;
            default:
                break;
        }
        stats.colour = s.color;
    }

    //This holds the initial values of the stats
    [System.Serializable]
    public struct TowerVars
    {
        public float fireRate;
        public float damage;
        public Color colour;//changed to be actual colour value
        public float pierce;
        public float price;
        public float range;
        public float bulletspeed;
        public float size;
        public float unique;
        public colorAim target;
        public bool buffed;
    }

    //Enum for aiming
    public enum colorAim {Red, Green, Blue};
    public enum aimMode { First, Last, Close, Strong};

    //This struct holds how far upgraded the current tower is. EG 1 in DMG
    public struct UpgradeLevel
    {
        public int fireRate;
        public int damage;
        public int range;
        public int pierce;
        public int bulletspeed;
        public int unique;
    }
    //Holds the max level of upgrades

    [System.Serializable]
    public struct MaxLevel
    {
        public int fireRate;
        public int damage;
        public int range;
        public int pierce;
        public int bulletspeed;
        public int unique;
    }
    //How much the stat gets incremented

    [System.Serializable]
    public struct IncrementBy
    {
        public float fireRate;
        public float damage;
        public float range;
        public float pierce;
        public float bulletspeed;
    }
    //Cost of the upgrade; multipled by num of upgrades i guess

    [System.Serializable]
    public struct BasePrice
    {
        public float fireRate;
        public float damage;
        public float range;
        public float pierce;
        public float bulletspeed;
        public float unique;
    }

    
}
    
