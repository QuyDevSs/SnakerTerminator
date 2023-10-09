using UnityEngine;

public class TerritoryDrawer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D territoryCollider;

    void Start()
    {
        DrawTerritory();
    }

    void DrawTerritory()
    {
        Bounds bounds = territoryCollider.bounds;
        Texture2D territoryTexture = new Texture2D((int)bounds.size.x, (int)bounds.size.y);

        for (int x = 0; x < territoryTexture.width; x++)
        {
            for (int y = 0; y < territoryTexture.height; y++)
            {
                Vector2 worldPoint = new Vector2(bounds.min.x + x, bounds.min.y + y);
                Vector2 localPoint = territoryCollider.transform.InverseTransformPoint(worldPoint);

                if (territoryCollider.OverlapPoint(localPoint))
                {
                    Debug.Log(1);
                    territoryTexture.SetPixel(x, y, Color.white);
                }
                else
                {
                    territoryTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        territoryTexture.Apply();

        Sprite sprite = Sprite.Create(territoryTexture, new Rect(0, 0, territoryTexture.width, territoryTexture.height), new Vector2(0.5f, 0.5f));

        spriteRenderer.sprite = sprite;
    }
}
