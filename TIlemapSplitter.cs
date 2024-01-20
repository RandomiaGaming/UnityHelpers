using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TilemapSplitter : MonoBehaviour
{
	public GameObject[] SubTilemapPrefabs;
	private TilemapTilePair[] _convertTable;
	private struct TilemapTilePair
	{
		public Tilemap _tilemap;
		public TileBase _tile;
		public TilemapTilePair(Tilemap tilemap, TileBase tile)
		{
			_tilemap = tilemap;
			_tile = tile;
		}
	}
	private void Start()
	{
		List<TilemapTilePair> convertTableList = new List<TilemapTilePair>();
		Transform grid = transform.parent;
		foreach (GameObject subTilemapPrefab in SubTilemapPrefabs)
		{
			GameObject subTilemap = Instantiate(subTilemapPrefab, Vector3.zero, Quaternion.identity, grid);
			Tilemap tilemap = subTilemap.GetComponent<Tilemap>();
			TilemapTagger tilemapTagger = subTilemap.GetComponent<TilemapTagger>();
			foreach (TileBase targetTile in tilemapTagger.TargetTiles)
			{
				convertTableList.Add(new TilemapTilePair(tilemap, targetTile));
			}
		}
		_convertTable = convertTableList.ToArray();

		{
			Tilemap tilemap = GetComponent<Tilemap>();
			tilemap.CompressBounds();
			for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
			{
				for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
				{
					Vector3Int position = new Vector3Int(x, y, 0);

					TileBase tile = tilemap.GetTile(position);

					foreach(TilemapTilePair pair in _convertTable)
					{
						if(pair._tile == tile)
						{
							pair._tilemap.SetTile(position, tile);
						}
					}
				}
			}
		}

		Destroy(gameObject);
	}
}