using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Create The Path", story: "[Self] going the [TargetPoint] and adding itself to [theList]", category: "Action", id: "c77ab520424a62d39b3f436c69bcf7b0")]
public partial class CreateThePathAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> TargetPoint;
    [SerializeReference] public BlackboardVariable<List<GameObject>> TheList;
    private AgentMover agentMover;

    protected override Status OnStart()
    {
        agentMover = Self.Value.GetComponent<AgentMover>();
        agentMover.ConstructPath(TargetPoint.Value);
        if (TheList.Value != null)
            TheList.Value.Add(Self.Value); //Agent going to gather that resource
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (TargetPoint == null) return Status.Running;
        if (Self.Value.transform.position.x == TargetPoint.Value.position.x &&
           Self.Value.transform.position.z == TargetPoint.Value.position.z)
        {
            return Status.Success; //Reached the target
        }
        if (!Self.Value.transform.hasChanged && Self.Value.transform.position.x % 5 != 0 && Self.Value.transform.position.z % 5 != 0) // %5 to avoid stopping on non grid positions
        {
            return Status.Failure; //Stuck somewhere
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (this.CurrentStatus == Status.Interrupted) //death or map change
        {
            if (agentMover != null && agentMover.currentPath != null)
                agentMover.currentPath.Clear();
            if (TheList.Value != null && TheList.Value.Contains(Self.Value))
            {
                TheList.Value.Remove(Self.Value); //Didn't reach the target, so remove itself from the gathering list
            }
        }
    }
}