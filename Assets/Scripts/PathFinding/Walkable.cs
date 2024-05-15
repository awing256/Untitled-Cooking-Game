using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Walkable : MonoBehaviour {
    private BoundsInt bounds;
    [SerializeField] private Tilemap collTM;
    [SerializeField, HideInInspector] private Tilemap walkTM;
    [SerializeField] private TileBase tile;
    void Start()
    {
        gameObject.gameObject.SetActive(false);
        walkTM = GetComponent<Tilemap>();
        collTM.CompressBounds();
        
        bounds = collTM.cellBounds;
        createWalkable();
    }
    private void createWalkable()
    {
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int z = bounds.min.z; z < bounds.max.z; z++)
                {

                    var v = collTM.GetTile(new Vector3Int(x, y, z));
                    if (v == null)
                    {
                        walkTM.SetTile(new Vector3Int(x, y, z), tile);
                    }
                }
            }

        }
        walkTM.CompressBounds();
    }
}
