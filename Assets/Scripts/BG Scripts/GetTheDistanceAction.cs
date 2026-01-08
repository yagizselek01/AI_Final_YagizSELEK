using System;
using System.Collections.Generic;
using TMPro;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Get The Distance", story: "Getting the Distance", category: "Action", id: "e7aac7339025fd1c1791777337d2236f")]
public partial class GetTheDistanceAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> TreePoint;
    [SerializeReference] public BlackboardVariable<Transform> StonePoint;
    [SerializeReference] public BlackboardVariable<Transform> SandPoint;
    [SerializeReference] public BlackboardVariable<float> DistanceToWood;
    [SerializeReference] public BlackboardVariable<float> DistanceToStone;
    [SerializeReference] public BlackboardVariable<float> DistanceToSand;
    private NavMeshAgent agent;

    protected override Status OnStart()
    {
        agent = Self.Value.GetComponent<NavMeshAgent>();
        DistanceToWood.Value = GetDistance(TreePoint.Value);
        DistanceToStone.Value = GetDistance(StonePoint.Value);
        DistanceToSand.Value = GetDistance(SandPoint.Value);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private float GetDistance(Transform target)
    {
        if (target == null || Self.Value == null)
            return float.MaxValue;
        return CalculatePathLength(target.position) / 10f;
    }

    private float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.enabled)
            agent.CalculatePath(targetPosition, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = Self.Value.transform.position;

        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}