using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using CodeMonkey.Utils;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tilemap walkableTilemap;

    [Header("Debug")]
    [SerializeField] private bool drawGrid = false;
    [SerializeField] private bool drawText = false;

    private Vector3Int[,] spots;
    private BoundsInt bounds;
    private TextMesh[,] debugTextMesh;
    private Grid tilemapsGrid;
    private AStar aStar;


    void Awake() {
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;
        tilemapsGrid = walkableTilemap.GetComponentInParent<Grid>();

        CreateGrid();
        aStar = new AStar(spots, bounds.size.x, bounds.size.y);
        

        if (drawGrid || drawText)
            CreateDebugText();
        
    }

    public void CreateGrid() {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];
        
        for (int x = bounds.xMin, i = 0; i < bounds.size.x; x++, i++) {
            for (int y = bounds.yMin, j = 0; j < bounds.size.y; y++, j++) {
                if (walkableTilemap.HasTile(new Vector3Int(x, y, 0))) {
                    // z = 0 --> has tile
                    spots[i, j] = new Vector3Int(x, y, 0);
                }
                else {
                    // z = 1 --> has no tile
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }

        Debug.Log(bounds.ToString());
        Debug.Log(bounds.size.x);
        Debug.Log(bounds.size.y);
    }

    public List<Spot> GetPath(Vector2Int start, Vector2Int end, int length) {
        return aStar.CreatePath(spots, start, end, length);
    }

    public Vector3Int GetRandomGridPosition() {
        return new Vector3Int((int)UnityEngine.Random.Range(bounds.xMin, bounds.xMax), (int)UnityEngine.Random.Range(bounds.yMin, bounds.yMax), 0);
    }

    public Vector3Int WorldToGridPosition(Vector3 worldPosition) {
        return tilemapsGrid.WorldToCell(worldPosition);
    }

    public Vector3 GridToWorldPosition(Vector3Int gridPosition) {
        return (tilemapsGrid.CellToWorld(gridPosition) + new Vector3(tilemapsGrid.cellSize.x/2, tilemapsGrid.cellSize.y/2));
    }

    private void DrawPath(Vector2Int start, Vector2Int end, int length) {
        List<Spot> path = aStar.CreatePath(spots, start, end, length);
        //Debug.Log(path.ToString());
        if (path == null) {
            Debug.Log("path not found");
            return;
        }
        CreateDebugText();
        for (int i = 0; i < path.Count; i++) {
            //debugTextMesh[path[i].X, path[i].Y].text = "X";
            Debug.Log(path[i].X + "," + path[i].Y);
        }
    }


    private void CreateDebugText() {
        debugTextMesh = new TextMesh[bounds.size.x, bounds.size.y];
        Vector3 worldPosition;
        for (int x = bounds.xMin, i = 0; x < bounds.xMax; x++, i++) {
            for (int y = bounds.yMin, j = 0; y < bounds.yMax; y++, j++) {
                worldPosition = tilemapsGrid.CellToWorld(new Vector3Int(x, y, 0));
                Vector3 cellCenterPosition = worldPosition + new Vector3(tilemapsGrid.cellSize.x/2, tilemapsGrid.cellSize.y/2);
                if (drawText)
                    debugTextMesh[i, j] = UtilsClass.CreateWorldText(x + "," + y, null, cellCenterPosition, 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, "DebugTextOnGround", 5);
                if (drawGrid) {
                    Debug.DrawLine(worldPosition, worldPosition + new Vector3(tilemapsGrid.cellSize.x, 0), Color.white, 400f);
                    Debug.DrawLine(worldPosition, worldPosition + new Vector3(0, tilemapsGrid.cellSize.y), Color.white, 400f);
                }
            }
        }
        if (drawGrid) {
            worldPosition = tilemapsGrid.CellToWorld(new Vector3Int(bounds.xMax, bounds.yMin, 0));
            Debug.DrawLine(worldPosition, worldPosition + new Vector3(0, bounds.size.y * tilemapsGrid.cellSize.y), Color.white, 400f);
            worldPosition = tilemapsGrid.CellToWorld(new Vector3Int(bounds.xMin, bounds.yMax, 0));
            Debug.DrawLine(worldPosition, worldPosition + new Vector3(bounds.size.x * tilemapsGrid.cellSize.x, 0), Color.white, 400f);
        }
    }
}
