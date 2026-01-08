using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] public int gridWidth = 20;

    [SerializeField] public int gridHeight = 14;
    [SerializeField] public GameObject startTile;
    [SerializeField] public float cellSize = 10f;
    [SerializeField] private int pathChangeTime = 30;

    [Header("Grids")]
    [SerializeField] private GameObject GridParent;

    [SerializeField] private GameObject EntranceParent;

    [Header("Materials")]
    [SerializeField] private Material walkableMaterial;

    [SerializeField] private Material lavaMaterial;

    [Header("Other")]
    [SerializeField] private NavMeshSurface navMeshSurface;

    private Node[] entrances;
    public Node[,] nodes;
    private FindPath findPath;

    private void Awake()
    {
        GetGrids();
        findPath = GetComponent<FindPath>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MakePath), 0f, pathChangeTime);
    }

    private void GetGrids()
    {
        nodes = new Node[gridWidth, gridHeight];
        for (int i = 0; i < GridParent.transform.childCount; i++)
        {
            GameObject tile = GridParent.transform.GetChild(i).gameObject;
            Vector2Int p = WorldToGrid(tile.transform.position);
            Node node = new Node(p.x, p.y, true, tile);
            SetTileMaterial(node, walkableMaterial);
            nodes[p.x, p.y] = node;
        }

        entrances = new Node[EntranceParent.transform.childCount];
        for (int i = 0; i < EntranceParent.transform.childCount; i++)
        {
            GameObject tile = EntranceParent.transform.GetChild(i).gameObject;
            Vector2Int p = WorldToGrid(tile.transform.position);
            Node node = new Node(p.x, p.y, true, tile);
            SetTileMaterial(node, walkableMaterial);
            nodes[p.x, p.y] = node;
            entrances[i] = node;
        }
    }

    private void SetTileMaterial(Node node, Material mat)
    {
        var renderer = node.tile.GetComponent<Renderer>();
        if (renderer != null && mat != null)
        {
            renderer.material = mat;
        }
        else
        {
            Debug.LogWarning("Renderer or Material is null.");
        }
    }

    public Vector2Int WorldToGrid(Vector3 pos)
    {
        Vector3 origin = startTile.transform.position;
        return new Vector2Int(
            Mathf.FloorToInt((pos.x - origin.x) / cellSize),
            Mathf.FloorToInt((pos.z - origin.z) / cellSize)
        );
    }

    public IEnumerable<Node> GetNeighbours(Node node, bool allowDiagonals = false)
    {
        int x = node.x;
        int y = node.y;

        yield return GetNode(x + 1, y);
        yield return GetNode(x - 1, y);
        yield return GetNode(x, y + 1);
        yield return GetNode(x, y - 1);
        if (allowDiagonals)
        {
            yield return GetNode(x + 1, y + 1);
            yield return GetNode(x - 1, y + 1);
            yield return GetNode(x + 1, y - 1);
            yield return GetNode(x - 1, y - 1);
        }
    }

    public Node GetNode(int x, int y)
    {
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
            return null;
        return nodes[x, y];
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPos)
    {
        Vector2Int gridPos = WorldToGrid(worldPos);
        return GetNode(gridPos.x, gridPos.y);
    }

    private void MakePath()
    {
        List<Node> safeNodes = new();
        List<Node> availableEntrances = new List<Node>(entrances);

        Node current = availableEntrances[Random.Range(0, availableEntrances.Count)];
        availableEntrances.Remove(current);

        while (availableEntrances.Count > 0)
        {
            Node next = availableEntrances[Random.Range(0, availableEntrances.Count)];
            availableEntrances.Remove(next);

            List<Node> path = findPath.CreatePath(current, next, true);

            if (path == null)
            {
                Debug.LogWarning($"No path found between {current} and {next}.");
            }
            else
            {
                foreach (Node node in path)
                {
                    if (!safeNodes.Contains(node))
                    {
                        safeNodes.Add(node);
                    }
                }
            }
            current = next;
        }
        StartCoroutine(MapChanging());
        SetLava(safeNodes);
        navMeshSurface.BuildNavMesh();
    }

    private void SetLava(List<Node> safenodes)
    {
        List<Node> lavaNodes = new();
        foreach (Node node in nodes)
        {
            node.walkable = true;
            node.tile.layer = LayerMask.NameToLayer("Default");
            SetTileMaterial(node, walkableMaterial);
            if (safenodes.Contains(node) || entrances.Contains(node))
                continue;
            lavaNodes.Add(node);
        }
        for (int i = 0; i < Random.Range(80, 160); i++)
        {
            if (lavaNodes.Count == 0)
                break;
            int index = Random.Range(0, lavaNodes.Count);
            Node lavaNode = lavaNodes[index];
            lavaNode.walkable = false;
            lavaNode.tile.layer = LayerMask.NameToLayer("NonWalkable");
            SetTileMaterial(lavaNode, lavaMaterial);
            lavaNodes.RemoveAt(index);
        }
    }

    private IEnumerator MapChanging()
    {
        PlayerProgress.isMapChanging = true;
        yield return new WaitForSeconds(0.2f);
        PlayerProgress.isMapChanging = false;
    }
}