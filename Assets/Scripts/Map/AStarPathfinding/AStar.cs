using System;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    public Spot[,] spots;


    #region constructor

    public AStar(Vector3Int[,] grid, int columns, int rows) {
        spots = new Spot[columns, rows];
    }

    #endregion

    #region methods

    private bool IsValidPath(Vector3Int[,] grid, Spot start, Spot end) {
        if (start == null)
            return false;
        if (end == null)
            return false;
        if (start.height > 1)
            return false;
        return true;
    }

    public List<Spot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length) {

        Spot Start = null;
        Spot End = null;
        var columns = spots.GetUpperBound(0) + 1;
        var rows = spots.GetUpperBound(1) + 1;
        spots = new Spot[columns, rows];

        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                spots[i, j] = new Spot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }

        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                spots[i, j].AddNeighboors(spots, i, j);
                if (spots[i, j].X == start.x && spots[i, j].Y == start.y)
                    Start = spots[i, j];
                if (spots[i, j].X == end.x && spots[i, j].Y == end.y)
                    End = spots[i, j];
            }
        }

        if (!IsValidPath(grid, Start, End))
            return null;
        
        List<Spot> openSet = new List<Spot>();
        List<Spot> closedSet = new List<Spot>();

        openSet.Add(Start);

        while (openSet.Count > 0) {

            int winner = 0;
            for (int i = 0; i < openSet.Count; i++) {
                if (openSet[i].F < openSet[winner].F)
                    winner = i;
                else if (openSet[i].F == openSet[winner].F)
                    if (openSet[i].H < openSet[winner].H)
                        winner = i;
            }

            var current = openSet[winner];

            // if found the path --> creates and returns the path
            if (End != null && openSet[winner] == End) {
                List<Spot> path = new List<Spot>();
                var temp = current;
                path.Add(temp);
                while (temp.previous != null) {
                    path.Add(temp.previous);
                    temp = temp.previous;
                }
                if (length - (path.Count - 1) > 0) {
                    //path.RemoveRange(0, (path.Count - 1) - length);
                }
                return path;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            // find the next closest step on the grid
            var neighboors = current.neighboors;
            for (int i = 0; i < neighboors.Count; i++) {
                var n = neighboors[i];
                if (!closedSet.Contains(n) && n.height < 1)//Checks to make sure the neighboor of our current tile is not within closed set, and has a height of less than 1
                {
                    var tempG = current.G + 1;//gets a temp comparison integer for seeing if a route is shorter than our current path

                    bool newPath = false;
                    if (openSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                    {
                        if (tempG < n.G)//The distance to the end goal from this neighboor is shorter so we need a new path
                        {
                            n.G = tempG;
                            newPath = true;
                        }
                    }
                    else//if its not in openSet or closed set, then it IS a new path and we should add it too openset
                    {
                        n.G = tempG;
                        newPath = true;
                        openSet.Add(n);
                    }
                    if (newPath)//if it is a newPath caclulate the H and F and set current to the neighboors previous
                    {
                        n.H = Heuristic(n, End);
                        n.F = n.G + n.H;
                        n.previous = current;
                    }
                }
            }
        }

        return null;
    }

    private int Heuristic(Spot a, Spot b)
    {
        //manhattan
        var dx = Math.Abs(a.X - b.X);
        var dy = Math.Abs(a.Y - b.Y);
        return 1 * (dx + dy);

        #region diagonal
        //diagonal
        // Chebyshev distance
        //var D = 1;
        // var D2 = 1;
        //octile distance
        //var D = 1;
        //var D2 = 1;
        //var dx = Math.Abs(a.X - b.X);
        //var dy = Math.Abs(a.Y - b.Y);
        //var result = (int)(1 * (dx + dy) + (D2 - 2 * D));
        //return result;// *= (1 + (1 / 1000));
        //return (int)Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        #endregion
    }

    #endregion
}

public class Spot {

    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public int height;

    public List<Spot> neighboors;
    public Spot previous = null;


    #region constructor

    public Spot(int x, int y, int height) {
        this.X = x;
        this.Y = y;
        this.F = 0;
        this.G = 0;
        this.H = 0;
        this.height = height;
        neighboors = new List<Spot>();
    }

    #endregion

    #region methods

    public void AddNeighboors(Spot[,] grid, int x, int y) {

        if (x < grid.GetUpperBound(0))
            neighboors.Add(grid[x + 1, y]);
        if (x > 0)
            neighboors.Add(grid[x - 1, y]);
        if (y < grid.GetUpperBound(1))
            neighboors.Add(grid[x, y + 1]);
        if (y > 0)
            neighboors.Add(grid[x, y - 1]);
        
        #region diagonal
        //if (X > 0 && Y > 0)
        //    Neighboors.Add(grid[X - 1, Y - 1]);
        //if (X < Utils.Columns - 1 && Y > 0)
        //    Neighboors.Add(grid[X + 1, Y - 1]);
        //if (X > 0 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X - 1, Y + 1]);
        //if (X < Utils.Columns - 1 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X + 1, Y + 1]);
        #endregion
    }

    #endregion
}
