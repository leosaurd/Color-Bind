using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RaycastCone : MonoBehaviour
{
    public float angle;
    public float a;
    public float b;
    public float c;
    public float range;

    public SpriteRenderer sr;

    void Awake()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Test();
        }
    }
    public void Test()
    {
        CreateTexture();
        Vector3 relativeVector = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0) - transform.position;
        float dist = relativeVector.magnitude;
        Vector3 normalVector = new Vector3(0, 1, 0)/*relativeVector / dist*/;

        Vector3 middleRayDir = normalVector;
        Vector3 direction = middleRayDir;

        a = 1f;
        b = a;
        c = 0;

        while (c <= angle)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, 1 << 11);
            UnityEngine.Debug.DrawRay(transform.position, direction * range, Color.green, 3f);

            Quaternion rotation = Quaternion.AngleAxis(b, Vector3.forward);
            direction = rotation * middleRayDir;

            b *= -1;
            b += (b > 0) ? a : 0;
            c++;
        }
    }

    private void CreateTexture()
    {
        float resolution = 512;
        Texture2D texture = new Texture2D((int)resolution, (int)resolution);
        for (int x = 0; x < (int)resolution; x++)
        {
            for (int y = 0; y < (int)resolution; y++)
            {
                if (IsValidPixel(x, y, resolution))
                {
                    Color col = new Color(1, 1, 1, 0.2f);
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
        sr.transform.localScale = new Vector3(range * 2, range * 2, 1);
        sr.sprite = sprite;
    }

    private bool IsValidPixel(float x, float y, float resolution)
    {
        float normalX = x / resolution;
        float normalY = y / resolution;
        if (Vector2.Distance(new Vector2(normalX, normalY), new Vector2(0.5f, 0.5f)) <= 0.5f)
        {
            float pixelAngle = GetAngle(new Vector2(0.5f, 1f), new Vector2(0.5f, 0.5f), new Vector2(normalX, normalY));
            if (pixelAngle < angle / 2f && pixelAngle > -angle / 2f)
            {
                return true;
            }
            else return false;
        }
        return false;
    }
    private float GetAngle(Vector2 A, Vector2 B, Vector2 C)
    {
        return Vector2.SignedAngle(A - B, C - B);
    }
}