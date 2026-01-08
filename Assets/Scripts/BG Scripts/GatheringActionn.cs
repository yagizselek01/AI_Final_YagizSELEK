using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GatheringAction", story: "[Self] gathering resource [Inventory] - [theList]", category: "Action", id: "0100ec1842feb1966081e51f6f48c028")]
public partial class GatheringActionn : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<int> Inventory;
    [SerializeReference] public BlackboardVariable<List<GameObject>> TheList;

    private int gatheringTimer = 5;

    protected override Status OnStart()
    {
        Self.Value.GetComponent<MonoBehaviour>().StartCoroutine(GatherCoroutine());
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Inventory.Value >= PlayerProgress.InventoryCapacity)
        {
            Debug.Log("Inventory full. Stopping gathering.");
            TheList.Value.Remove(Self.Value);
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private IEnumerator GatherCoroutine()
    {
        while (Inventory.Value <= PlayerProgress.InventoryCapacity)
        {
            yield return new WaitForSeconds(gatheringTimer);
            Inventory.Value += 1;
            Debug.Log("Gathered 1 " + ". Current Inventory: " + Inventory.Value);
        }
    }
}