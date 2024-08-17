using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TileType;

public class DualGridTilemap : MonoBehaviour {
    protected static Vector3Int[] NEIGHBOURS = new Vector3Int[] {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0)
    };

    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTile;

    // Provide references to each tilemap in the inspector
    public Tilemap placeholderTilemap;
    public Tilemap displayTilemap;

    // Provide the dirt and grass placeholder tiles in the inspector
    public Tile grassPlaceholderTile;
    public Tile dirtPlaceholderTile;

    // Provide the 16 tiles in the inspector
    [SerializeField]
    public Tile[] tiles;

    void Start() {
        // This dictionary stores the "rules", each 4-neighbour configuration corresponds to a tile
        // |_1_|_2_|
        // |_3_|_4_|
        neighbourTupleToTile = new() {
            {new (None, None, None, None), tiles[6]},
            {new (Dirt, Dirt, Dirt, None), tiles[13]}, // OUTER_BOTTOM_RIGHT
            {new (Dirt, Dirt, None, Dirt), tiles[0]}, // OUTER_BOTTOM_LEFT
            {new (Dirt, None, Dirt, Dirt), tiles[8]}, // OUTER_TOP_RIGHT
            {new (None, Dirt, Dirt, Dirt), tiles[15]}, // OUTER_TOP_LEFT
            {new (Dirt, None, Dirt, None), tiles[1]}, // EDGE_RIGHT
            {new (None, Dirt, None, Dirt), tiles[11]}, // EDGE_LEFT
            {new (Dirt, Dirt, None, None), tiles[3]}, // EDGE_BOTTOM
            {new (None, None, Dirt, Dirt), tiles[9]}, // EDGE_TOP
            {new (Dirt, None, None, None), tiles[5]}, // INNER_BOTTOM_RIGHT
            {new (None, Dirt, None, None), tiles[2]}, // INNER_BOTTOM_LEFT
            {new (None, None, Dirt, None), tiles[10]}, // INNER_TOP_RIGHT
            {new (None, None, None, Dirt), tiles[7]}, // INNER_TOP_LEFT
            {new (Dirt, None, None, Dirt), tiles[14]}, // DUAL_UP_RIGHT
            {new (None, Dirt, Dirt, None), tiles[4]}, // DUAL_DOWN_RIGHT
            {new (Dirt, Dirt, Dirt, Dirt), tiles[12]},

        };
        RefreshDisplayTilemap();
    }

    public void SetCell(Vector3Int coords, Tile tile) {
        placeholderTilemap.SetTile(coords, tile);
        setDisplayTile(coords);
    }

    private TileType getPlaceholderTileTypeAt(Vector3Int coords) {
        if (placeholderTilemap.GetTile(coords) == grassPlaceholderTile)
            return Grass;
        else if (placeholderTilemap.GetTile(coords) == dirtPlaceholderTile)
            return Dirt;
        else
            return None;
    }

    protected Tile calculateDisplayTile(Vector3Int coords) {
        // 4 neighbours
        TileType topRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[0]);
        TileType topLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[1]);
        TileType botRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[2]);
        TileType botLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[3]);

        Tuple<TileType, TileType, TileType, TileType> neighbourTuple = new(topLeft, topRight, botLeft, botRight);

        return neighbourTupleToTile[neighbourTuple];
    }

    protected void setDisplayTile(Vector3Int pos) {
        for (int i = 0; i < NEIGHBOURS.Length; i++) {
            Vector3Int newPos = pos + NEIGHBOURS[i];
            displayTilemap.SetTile(newPos, calculateDisplayTile(newPos));
        }
    }

    // The tiles on the display tilemap will recalculate themselves based on the placeholder tilemap
    public void RefreshDisplayTilemap() {
        for (int i = -50; i < 50; i++) {
            for (int j = -50; j < 50; j++) {
                setDisplayTile(new Vector3Int(i, j, 0));
            }
        }
    }
}

public enum TileType {
    None,
    Grass,
    Dirt
}
