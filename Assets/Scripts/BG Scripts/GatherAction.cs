using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Gather", story: "[Self] gathers resource [Inventory] - [theList] // [Gathering]", category: "Action", id: "91864b67c327f5d220d80baef55a2b84")]
public partial class GatherAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> TheList;
    [SerializeReference] public BlackboardVariable<bool> Gathering;
    [SerializeReference] public BlackboardVariable<int> Inventory;
    private float gatheringTimer = 1f;
    private float currentTimer = 0f;
    private int defaultID;

    protected override Status OnStart()
    {
        defaultID = Self.Value.GetComponent<AgentMover>().ID;
        currentTimer = 0f;
        Gathering.Value = true;

        Self.Value.GetComponent<AgentMover>().ID = 30;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Inventory.Value >= PlayerProgress.InventoryCapacity)
        {
            return Status.Success;
        }

        currentTimer += Time.deltaTime;

        if (currentTimer >= gatheringTimer)
        {
            Inventory.Value += 1;
            currentTimer = 0;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (TheList.Value.Contains(Self.Value))
        {
            TheList.Value.Remove(Self.Value);
        }
        Gathering.Value = false;
        Self.Value.GetComponent<AgentMover>().ID = defaultID;
    }
}