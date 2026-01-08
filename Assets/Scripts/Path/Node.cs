using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public bool walkable;
    public GameObject tile;
    public float gCost;
    public float hCost;
    public Node parent;
    public float fCost => gCost + hCost;

    public Node(int x, int y, bool walkable, GameObject tile)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        this.tile = tile;
        gCost = float.PositiveInfinity;
        hCost = 0f;
        parent = null;
    }
}