using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Deposit Resources", story: "Self deposit the inventories = [Wood] [Stone] [Sand]", category: "Action", id: "e074e5e34f78ba05e86ed17eb58ebcc6")]
public partial class DepositResourcesAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Wood;
    [SerializeReference] public BlackboardVariable<int> Stone;
    [SerializeReference] public BlackboardVariable<int> Sand;

    protected override Status OnStart()
    {
        PlayerProgress.GlobalSand += Sand.Value;
        PlayerProgress.GlobalStone += Stone.Value;
        PlayerProgress.GlobalWood += Wood.Value;
        Sand.Value = 0;
        Stone.Value = 0;
        Wood.Value = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}