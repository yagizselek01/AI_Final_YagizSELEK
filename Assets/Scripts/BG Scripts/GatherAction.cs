using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Gather", story: "[Self] gathers resource [MainInventory] - [theList] // [Gathering] // [OtherInventory] // [OtherInventory2]", category: "Action", id: "91864b67c327f5d220d80baef55a2b84")]
public partial class GatherAction : Action
{
    #region variables

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<GameObject>> TheList;
    [SerializeReference] public BlackboardVariable<bool> Gathering;
    [SerializeReference] public BlackboardVariable<int> MainInventory;
    [SerializeReference] public BlackboardVariable<int> OtherInventory;
    [SerializeReference] public BlackboardVariable<int> OtherInventory2;
    private float gatheringTimer;
    private float currentTimer = 0f;
    private int defaultID;

    #endregion variables

    protected override Status OnStart()
    {
        defaultID = Self.Value.GetComponent<AgentMover>().ID;
        currentTimer = 0f;
        Gathering.Value = true;

        Self.Value.GetComponent<AgentMover>().ID = 30; // If gathering, don't disturb other agents
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        gatheringTimer = 3 / PlayerProgress.GatherSpeedLevel;
        if (Inventory >= PlayerProgress.InventoryCapacity)
        {
            return Status.Success;
        }

        currentTimer += Time.deltaTime;

        if (currentTimer >= gatheringTimer)
        {
            MainInventory.Value += 1;
            currentTimer = 0;
        }

        return Status.Running; // Still gathering
    }

    private int Inventory
    {
        get
        {
            return MainInventory.Value + OtherInventory.Value + OtherInventory2.Value;
        }
    }

    protected override void OnEnd()
    {
        if (TheList.Value.Contains(Self.Value)) // Remove from gathering list
        {
            TheList.Value.Remove(Self.Value);
        }
        Gathering.Value = false;
        Self.Value.GetComponent<AgentMover>().ID = defaultID; // Restore original ID
    }
}