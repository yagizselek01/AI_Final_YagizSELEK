using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
    // This script comes from one of the Labs
    [SerializeField] private GridManager gridManager;

    public List<Node> CreatePath(Node startNode, Node goalNode, bool randomizer)
    {
        if (startNode == null || goalNode == null)
            return null;

        ResetCosts(goalNode);
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        startNode.gCost = 0;
        startNode.hCost = HeuristicCost(startNode, goalNode);
        openSet.Add(startNode);
        while (openSet.Count != 0)
        {
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < current.fCost || (openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost))
                    current = openSet[i];
            }

            if (current == goalNode)
                return ReconstructPath(startNode, goalNode);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Node neighbour in gridManager.GetNeighbours(current))
            {
                if (neighbour == null || !neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                Vector2Int currentDir = Vector2Int.zero;

                if (current.parent != null)
                    currentDir = new Vector2Int(current.x - current.parent.x, current.y - current.parent.y);

                Vector2Int newDir = new Vector2Int(neighbour.x - current.x, neighbour.y - current.y);
                float stepCost = 1f;
                if (randomizer) // Add randomness to the pathfinding for map
                {
                    if (currentDir != Vector2Int.zero && newDir == currentDir)
                        stepCost += Random.Range(2f, 5f);
                    else
                        stepCost += Random.Range(0f, 0.5f);

                    stepCost += Random.Range(0f, 5f);
                }
                float tentativeG = current.gCost + stepCost;
                if (tentativeG < neighbour.gCost)
                {
                    neighbour.parent = current;
                    neighbour.gCost = tentativeG;
                    neighbour.hCost = HeuristicCost(neighbour, goalNode);

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null;
    }

    private List<Node> ReconstructPath(Node startNode, Node goalNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = goalNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private void ResetCosts(Node goalNode)
    {
        for (int x = 0; x < gridManager.gridWidth; x++)
        {
            for (int y = 0; y < gridManager.gridHeight; y++)
            {
                Node node = gridManager.GetNode(x, y);
                node.gCost = float.PositiveInfinity;
                node.hCost = HeuristicCost(node, goalNode);
                node.parent = null;
            }
        }
    }

    private float HeuristicCost(Node a, Node b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return dx + dy;
    }
}