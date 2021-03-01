using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TowerA : Tower
{
    public SpriteRenderer sr;
    public float FoV;

  /*  public override void Update()
    {
        base.Update();
        sr.transform.localScale = new Vector3(stats.range * 2, stats.range * 2, 1);
        if (enemy != null)
        {
            sr.gameObject.SetActive(true);
            relativeVector = enemy.transform.position - transform.position;

            if (relativeVector.x > 0 && sr.transform.rotation.z > 0)
            {
                transform.Rotate(new Vector3(0, 0, -180));
            }
            else if (relativeVector.x < 0 && sr.transform.rotation.z < 0)
            {
                transform.Rotate(new Vector3(0, 0, 180));
            }
        }
        else
        {
            sr.gameObject.SetActive(false);
        }
    }
    public override void Fire()
    {
        relativeVector = enemy.transform.position - transform.position;
        dist = relativeVector.magnitude;
        normalVector = relativeVector / dist;
        Vector3 middleRayDir = normalVector;
        Vector3 direction = middleRayDir;

        float a = 1f;
        float b = a;
        float c = 0;


        StartCoroutine(FireRate());
        List<GameObject> enemies = new List<GameObject>();
        while (c <= FoV)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, stats.range, 1 << 11);
            for (int i = 0; i < hit.Length; i++)
            {
                if (!enemies.Contains(hit[i].collider.gameObject))
                {
                    enemies.Add(hit[i].collider.gameObject);
                    //If the target enemy has the same "target" value as the tower.
                    if (hit[i].collider.gameObject.GetComponent<Enemy>().enemyColor == stats.target)
                    {
                        hit[i].collider.gameObject.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
                    }
                }
            }
            Debug.DrawRay(transform.position, direction * stats.range, Color.green, stats.fireRate);

            Quaternion rotation = Quaternion.AngleAxis(b, Vector3.forward);
            direction = rotation * middleRayDir;

            b *= -1;
            b += (b > 0) ? a : 0;
            c++;
        }
    }
*/

    //This code is just to create the cone!

    private void CreateTexture()
    {
        float resolution = 512;
        Texture2D texture = new Texture2D((int)resolution, (int)resolution);
        for (int x = 0; x < (int)resolution; x++)
        {
            for (int y = 0; y < (int)resolution; y++)
            {
                if (IsValidPixel(x, y, resolution))
                {//mess with the float thing on the left of minus
                    Color col = new Color(1, 1, 1, 0.5f *(0.5f - Vector2.Distance(new Vector2(0.5f, 0.5f), new Vector2(x / resolution, y / resolution))));
                    texture.SetPixel(x, y, col);
                }
                else
                {
                    Color col = new Color(0, 0, 0, 0.0f);
                    texture.SetPixel(x, y, col);
                }

            }
        }
        texture.Apply();
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, resolution, resolution), new Vector2(0.5f, 0.5f), resolution);
        sr.transform.localScale = new Vector3(stats.range * 2, stats.range * 2, 1);
        sr.sprite = sprite;

        byte[] data = texture.EncodeToPNG();
        
        File.WriteAllBytes(Application.dataPath + "/../rangeAoE.png", data);
    }

    private bool IsValidPixel(float x, float y, float resolution)
    {
        float normalX = x / resolution;
        float normalY = y / resolution;
        if (Vector2.Distance(new Vector2(normalX, normalY), new Vector2(0.5f, 0.5f)) <= 0.5f)
        {
            float pixelAngle = GetAngle(new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f), new Vector2(normalX, normalY));
            if (pixelAngle < FoV / 2f && pixelAngle > -FoV / 2f)
            {
                return true;
            }
            else return false;
        }
        return false;
    }


#if (UNITY_EDITOR)
    // Custom inspector
    [CustomEditor(typeof(TowerA))]
    public class PathEditor : Editor
    {
        // Function called by unity to display in inspector
        public override void OnInspectorGUI()
        {
            // Show default inspector
            base.DrawDefaultInspector();

            // Reference to parent script
            TowerA Script = (TowerA)target;

            // Begin a horizontal row
            GUILayout.BeginHorizontal();

            // Draw a button and if it's clicked
            if (GUILayout.Button("GenerateTexture"))
                Script.CreateTexture();
            GUILayout.EndHorizontal();
        }
    }
#endif

}
