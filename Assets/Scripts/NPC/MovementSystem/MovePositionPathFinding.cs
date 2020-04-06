using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePositionPathFinding : MonoBehaviour, IMovePosition
{
   public event EventHandler OnPositionReached;

   [SerializeField] private GridManager gridManager;
   [SerializeField] private float speed = 4f;
    private float eps = 0.05f;
    private Vector3 movePosition;
    private List<Spot> path;
    private Vector2Int start;
    private Vector2Int end;
    private int targetSpotIndex = 0;
    private bool move = false;

    void Update() {
        if (move) {
            Move();
        }
    }

    public void SetMovePosition(Vector3 movePosition) {
        this.movePosition = movePosition;
        end = new Vector2Int(gridManager.WorldToGridPosition(movePosition).x, gridManager.WorldToGridPosition(movePosition).y);
        path = Path();
        if (path == null) {
           move = false;
        }
        else if (path.Count > 1) {
            targetSpotIndex = 1;
            move = true;
        }
    }

    private void Move() {
        Vector3 currentMovePosition = gridManager.GridToWorldPosition(new Vector3Int(path[targetSpotIndex].X, path[targetSpotIndex].Y, 0));
        if ((currentMovePosition - transform.position).magnitude >= eps) {
            Vector3 moveDirection = (currentMovePosition - transform.position).normalized;
            //gameObject.GetComponent<IMoveVelocity>().SetVelocity(moveDirection * speed);
            gameObject.GetComponent<IMoveVelocity>().MoveTowards(currentMovePosition, speed * Time.deltaTime);
        }
        else {
            if (targetSpotIndex + 1 == path.Count) {
                move = false;
                //gameObject.GetComponent<IMoveVelocity>().SetVelocity(Vector3.zero);
                //Debug.Log("set to false");
                if (OnPositionReached != null)
                    OnPositionReached(this, EventArgs.Empty);
            }
            else {
                targetSpotIndex++;
                //Debug.Log("target Index: " + targetSpotIndex);
            }
        }
    }

    private List<Spot> Path() {
        start = new Vector2Int(gridManager.WorldToGridPosition(transform.position).x, gridManager.WorldToGridPosition(transform.position).y);
        return gridManager.GetPath(start, end, 1000);
    }

}
