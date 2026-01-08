using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Agents Interaction", story: "[Self] points = [wood] // [sand] // [stone] // [storage]", category: "Action", id: "f211c7f83188a8cfdf094c063f8572fc")]
public partial class AgentsInteractionAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Wood;
    [SerializeReference] public BlackboardVariable<Transform> Sand;
    [SerializeReference] public BlackboardVariable<Transform> Stone;
    [SerializeReference] public BlackboardVariable<Transform> Storage;
    private AgentMover agentMover;
    private float normalMoveSpeed;
    private static Collider[] buffer = new Collider[24];

    protected override Status OnStart()
    {
        agentMover = Self.Value.GetComponent<AgentMover>();
        normalMoveSpeed = agentMover.defaultMoveSpeed;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (HasHigherPriorityNearby())
        {
            agentMover.moveSpeed = 0f;
            return Status.Failure;
        }
        agentMover.moveSpeed = normalMoveSpeed;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private bool HasHigherPriorityNearby()
    {
        int count = Physics.OverlapSphereNonAlloc(
Self.Value.transform.position,
2f,
buffer);
        for (int i = 0; i < count; i++)
        {
            AgentMover other = buffer[i].GetComponent<AgentMover>();
            if (!other || other == Self.Value || AtEnterance(Self.Value.transform)) continue;

            if (other.ID < agentMover.ID && Self.Value.transform.position.x % 5 == 0 && Self.Value.transform.position.z % 5 == 0)
                return true;
        }
        return false;
    }

    private bool AtEnterance(Transform agent)
    {
        Vector3[] woodPos = { Wood.Value.position, Sand.Value.position, Stone.Value.position, Storage.Value.position };
        foreach (var pos in woodPos)
        {
            if (agent.position.x == pos.x && agent.position.y == pos.y)
                return true;
        }
        return false;
    }
}