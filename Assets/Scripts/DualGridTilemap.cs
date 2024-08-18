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

    [SerializeField]
    public int horizontalRenderDistance;
    [SerializeField]
    public int verticalRenderDistance;

    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTile;
    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTileGrass;
    protected static Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> neighbourTupleToTileBrass;

    // Provide references to each tilemap in the inspector
    public Tilemap placeholderTilemap;
    public Tilemap displayTilemap;
    public Tilemap displayTilemapTwo;
    public Tilemap displayTilemapThree;

    // Provide the dirt and grass placeholder tiles in the inspector
    public Tile grassPlaceholderTile;
    public Tile dirtPlaceholderTile;
    public Tile brassPlaceholderTile;

    // Provide the 16 tiles in the inspector
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

        neighbourTupleToTileGrass = new() {
            {new (None, None, None, None), tiles[22]},
            {new (Dirt, Dirt, Dirt, None), tiles[29]}, // OUTER_BOTTOM_RIGHT
            {new (Dirt, Dirt, None, Dirt), tiles[16]}, // OUTER_BOTTOM_LEFT
            {new (Dirt, None, Dirt, Dirt), tiles[24]}, // OUTER_TOP_RIGHT
            {new (None, Dirt, Dirt, Dirt), tiles[31]}, // OUTER_TOP_LEFT
            {new (Dirt, None, Dirt, None), tiles[17]}, // EDGE_RIGHT
            {new (None, Dirt, None, Dirt), tiles[27]}, // EDGE_LEFT
            {new (Dirt, Dirt, None, None), tiles[19]}, // EDGE_BOTTOM
            {new (None, None, Dirt, Dirt), tiles[25]}, // EDGE_TOP
            {new (Dirt, None, None, None), tiles[21]}, // INNER_BOTTOM_RIGHT
            {new (None, Dirt, None, None), tiles[18]}, // INNER_BOTTOM_LEFT
            {new (None, None, Dirt, None), tiles[26]}, // INNER_TOP_RIGHT
            {new (None, None, None, Dirt), tiles[23]}, // INNER_TOP_LEFT
            {new (Dirt, None, None, Dirt), tiles[30]}, // DUAL_UP_RIGHT
            {new (None, Dirt, Dirt, None), tiles[20]}, // DUAL_DOWN_RIGHT
            {new (Dirt, Dirt, Dirt, Dirt), tiles[28]},

        };

        neighbourTupleToTileBrass = new() {
            {new (None, None, None, None), tiles[38]},
            {new (Brass, Brass, Brass, None), tiles[45]}, // OUTER_BOTTOM_RIGHT
            {new (Brass, Brass, None, Brass), tiles[32]}, // OUTER_BOTTOM_LEFT
            {new (Brass, None, Brass, Brass), tiles[40]}, // OUTER_TOP_RIGHT
            {new (None, Brass, Brass, Brass), tiles[47]}, // OUTER_TOP_LEFT
            {new (Brass, None, Brass, None), tiles[33]}, // EDGE_RIGHT
            {new (None, Brass, None, Brass), tiles[43]}, // EDGE_LEFT
            {new (Brass, Brass, None, None), tiles[35]}, // EDGE_BOTTOM
            {new (None, None, Brass, Brass), tiles[41]}, // EDGE_TOP
            {new (Brass, None, None, None), tiles[37]}, // INNER_BOTTOM_RIGHT
            {new (None, Brass, None, None), tiles[34]}, // INNER_BOTTOM_LEFT
            {new (None, None, Brass, None), tiles[42]}, // INNER_TOP_RIGHT
            {new (None, None, None, Brass), tiles[39]}, // INNER_TOP_LEFT
            {new (Brass, None, None, Brass), tiles[46]}, // DUAL_UP_RIGHT
            {new (None, Brass, Brass, None), tiles[36]}, // DUAL_DOWN_RIGHT
            {new (Brass, Brass, Brass, Brass), tiles[44]},

        };

        RefreshDisplayTilemap();
    }

    public void SetCell(Vector3Int coords, Tile tile) {
        placeholderTilemap.SetTile(coords, tile);
        setDisplayTile(coords);
    }

    private TileType getPlaceholderTileTypeAt(Vector3Int coords, String tileSetName) {
        if (tileSetName != "Brass")
        {
            if (placeholderTilemap.GetTile(coords) == grassPlaceholderTile)
                return Grass;
            else if (placeholderTilemap.GetTile(coords) == dirtPlaceholderTile)
                return Dirt;
            else
                return None;
        }
        else
        {
            if(placeholderTilemap.GetTile(coords) == brassPlaceholderTile)
            {
                return Brass;
            }
            else
            {
                return None;
            }
        }
    }

    protected Tile calculateDisplayTile(Vector3Int coords, Dictionary<Tuple<TileType, TileType, TileType, TileType>, Tile> tileSet, String tileSetName) {
        // 4 neighbours
        TileType topRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[0], tileSetName);
        TileType topLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[1], tileSetName);
        TileType botRight = getPlaceholderTileTypeAt(coords - NEIGHBOURS[2], tileSetName);
        TileType botLeft = getPlaceholderTileTypeAt(coords - NEIGHBOURS[3], tileSetName);

        Tuple<TileType, TileType, TileType, TileType> neighbourTuple = new(topLeft, topRight, botLeft, botRight);

        return tileSet[neighbourTuple];
    }

    protected void setDisplayTile(Vector3Int pos) {
        for (int i = 0; i < NEIGHBOURS.Length; i++) {
            Vector3Int newPos = pos + NEIGHBOURS[i];
            displayTilemap.SetTile(newPos, calculateDisplayTile(newPos, neighbourTupleToTile, "Dirt"));
            displayTilemapTwo.SetTile(newPos, calculateDisplayTile(newPos, neighbourTupleToTileGrass, "Grass"));
            displayTilemapThree.SetTile(newPos, calculateDisplayTile(newPos, neighbourTupleToTileBrass, "Brass"));
        }
    }

    // The tiles on the display tilemap will recalculate themselves based on the placeholder tilemap
    public void RefreshDisplayTilemap() {
        for (int i = (horizontalRenderDistance * -1); i < horizontalRenderDistance; i++) {
            for (int j = (verticalRenderDistance * -1); j < verticalRenderDistance; j++) {
                setDisplayTile(new Vector3Int(i, j, 0));
            }
        }
    }
}
//hello
public enum TileType {
    None,
    Grass,
    Dirt,
    Brass
}
