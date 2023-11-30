using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class TileMapReplacer : MonoBehaviour
{
    void Start()
    {
        Tilemap tm = GetComponent<Tilemap>();
        for (int x = tm.cellBounds.xMin; x < tm.cellBounds.xMax; x++)
        {
            for (int y = tm.cellBounds.yMin; y < tm.cellBounds.yMax; y++)
            {
                if (tm.GetTile(new Vector3Int(x, y, 0)) != null && tm.GetTile(new Vector3Int(x, y, 0)).GetType() == typeof(GameObjectTile))
                {
                    GameObjectTile t = (GameObjectTile)tm.GetTile(new Vector3Int(x, y, 0));
                    GameObject go = Instantiate(t.Prefab, new Vector3(x + 0.5f, y + 0.5f, 0), Quaternion.identity);
                    go.transform.parent = transform;
                    if (go.GetComponent<SpriteRenderer>() != null)
                    {
                        go.GetComponent<SpriteRenderer>().color = t.color;
                        go.GetComponent<SpriteRenderer>().sprite = t.sprite;
                    }
                    tm.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }
}
