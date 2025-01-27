using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class TilemapLayers : MonoBehaviour
{
    public TilemapLayerchild[] children;
    private Tilemap tm;
    // Use this for initialization
    void Start()
    {
        tm = GetComponent<Tilemap>();
        Regenerate();
    }
    public void Regenerate()
    {
        foreach(TilemapLayerchild t in children)
        {
            t.tm = t.GetComponent<Tilemap>();
        }
        for (int x = tm.cellBounds.min.x; x < tm.cellBounds.max.x; x++)
        {
            for (int y = tm.cellBounds.min.y; y < tm.cellBounds.max.y; y++)
            {
                foreach (TilemapLayerchild t in children)
                {
                    if(Contains(t.TilesOnThisMap, tm.GetTile(new Vector3Int(x, y, 0)))){
                        t.tm.SetTile(new Vector3Int(x, y, 0), tm.GetTile(new Vector3Int(x, y, 0)));
                        tm.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        } 
    }
    private bool Contains(TileBase[] tiles, TileBase tile)
    {
        foreach (TileBase t in tiles)
        {
            if(t == tile)
            {
                return true;
            }
        }
        return false;
    }
}
