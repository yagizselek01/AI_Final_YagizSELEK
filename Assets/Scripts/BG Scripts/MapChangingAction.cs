using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Map Changing", story: "[MapChanged] triggered // [Self] // [Wood] [Stone] [Sand] // [CurrentTask] // [StartPoint] // [Death] // [Gathering]", category: "Action", id: "d5ef23010d3521f2482504f8dfcf788a")]
public partial class MapChangingAction : Action
{
    [SerializeReference] public BlackboardVariable<int> MapChanged;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<int> Wood;
    [SerializeReference] public BlackboardVariable<int> Stone;
    [SerializeReference] public BlackboardVariable<int> Sand;
    [SerializeReference] public BlackboardVariable<TaskType> CurrentTask;
    [SerializeReference] public BlackboardVariable<Vector3> StartPoint;
    [SerializeReference] public BlackboardVariable<int> Death;
    [SerializeReference] public BlackboardVariable<bool> Gathering;

    private GridManager gridManager;

    protected override Status OnStart()
    {
        gridManager = GameObject.FindFirstObjectByType<GridManager>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (PlayerProgress.isMapChanging && !Gathering)
        {
            MapChanged.Value = 1;
            return Status.Success;
        }
        if (Physics.Raycast(Self.Value.transform.position, Vector3.down, out RaycastHit hitInfo, 5f))
        {
            Vector2Int nodeTransform = gridManager.WorldToGrid(hitInfo.transform.position);
            Node node = gridManager.GetNode(nodeTransform.x, nodeTransform.y);
            if (!node.walkable)
            {
                Self.Value.transform.position = StartPoint.Value;
                Wood.Value = Mathf.RoundToInt(Wood.Value * 0.3f);
                Stone.Value = Mathf.RoundToInt(Stone.Value * 0.3f);
                Sand.Value = Mathf.RoundToInt(Sand.Value * 0.3f);
                CurrentTask.Value = TaskType.Idle;
                Death.Value = 1;
                return Status.Success;
            }
        }
        return Status.Failure;
    }
}