using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[RequireComponent(typeof(Tilemap))]
public class TilemapLayerchild : MonoBehaviour {
    public TileBase[] TilesOnThisMap;
    [HideInInspector]
    public Tilemap tm;
}
