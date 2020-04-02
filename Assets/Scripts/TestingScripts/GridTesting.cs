using System.Collections;
using UnityEngine;
using CodeMonkey.Utils;

public class GridTesting : MonoBehaviour
{

    [SerializeField] private Grid unityGrid;

    [SerializeField] private Vector2 gridSize = new Vector2(4, 4);
    private GridSystem grid;
    private Vector3 cellSize;
    private Vector3 originPosition;

    [Header("Debug")]
    [SerializeField] private string sortingLayerName = "Default";

    void Start() {
        cellSize = unityGrid.cellSize;
        originPosition = unityGrid.gameObject.transform.position;
        grid = new GridSystem((int)gridSize.x, (int)gridSize.y, cellSize.x, sortingLayerName, originPosition);

    } 

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

}
