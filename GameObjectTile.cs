using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New GameObject Tile", menuName = "Tiles/GameObject Tile")]
public class GameObjectTile : UnityEngine.Tilemaps.Tile {
    public GameObject Prefab;
}
