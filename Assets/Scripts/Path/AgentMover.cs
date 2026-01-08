using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    public FindPath pathfinder;
    public GridManager gridManager;
    public float moveSpeed = 3f;
    [HideInInspector] public float defaultMoveSpeed;
    public List<Node> currentPath;
    private int currentIndex = 0;
    private Rigidbody rb;
    private bool moving = false;
    [SerializeField] private GameObject originTile;
    [SerializeField] public int ID;

    public void ConstructPath(Transform target)
    {
        Node targetNode = gridManager.GetNode(gridManager.WorldToGrid(target.position).x, gridManager.WorldToGrid(target.position).y);
        Node startnode = gridManager.GetNode(gridManager.WorldToGrid(transform.position).x, gridManager.WorldToGrid(transform.position).y);
        FollowPath(pathfinder.CreatePath(startnode, targetNode, false));
    }

    private void Start()
    {
        defaultMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }

    public void FollowPath(List<Node> path)
    {
        currentPath = path;
        currentIndex = 0;
    }

    private void FixedUpdate()
    {
        if (currentPath == null || currentPath.Count == 0 || currentIndex >= currentPath.Count) return;

        Node targetNode = currentPath[currentIndex];
        Vector3 targetPos = GridToWorld(targetNode);

        if (transform.position.x == targetPos.x && transform.position.z == targetPos.z)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        if (!moving)
        {
            currentIndex++;
        }
        float step = moveSpeed * Time.deltaTime;
        rb.MovePosition(Vector3.MoveTowards(rb.position, targetPos, step));
    }

    public Vector3 GridToWorld(Node node)
    {
        return new Vector3(
            Mathf.FloorToInt((node.x * gridManager.cellSize) + originTile.transform.position.x), 3,
            Mathf.FloorToInt((node.y * gridManager.cellSize) + originTile.transform.position.z)
        );
    }
}