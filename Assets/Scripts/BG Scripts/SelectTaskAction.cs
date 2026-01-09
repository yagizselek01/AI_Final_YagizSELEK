using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Select Task", story: "Selecting The Task", category: "Action", id: "7815e6e161708a7c1fe4886fc896a861")]
public partial class SelectTaskAction : Action
{
    #region Variables

    [SerializeReference] public BlackboardVariable<int> InventoryWood;
    [SerializeReference] public BlackboardVariable<int> InventoryStone;
    [SerializeReference] public BlackboardVariable<int> InventorySand;
    [SerializeReference] public BlackboardVariable<TaskType> CurrentTask;
    [SerializeReference] public BlackboardVariable<List<GameObject>> OnWood;
    [SerializeReference] public BlackboardVariable<List<GameObject>> OnStone;
    [SerializeReference] public BlackboardVariable<List<GameObject>> OnSand;
    [SerializeReference] public BlackboardVariable<float> DistanceToWood;
    [SerializeReference] public BlackboardVariable<float> DistanceToStone;
    [SerializeReference] public BlackboardVariable<float> DistanceToSand;

    private float sandScore;
    private float stoneScore;
    private float woodScore;
    private static int maxExpectedDistance = 25;
    private static int maxExpectedAgent = 8;

    #endregion Variables

    protected override Status OnStart()
    {
        SetScores();
        Dictionary<TaskType, float> scores = new Dictionary<TaskType, float>
        {
            {  TaskType.GatherWood, woodScore  },
            { TaskType.GatherStone, stoneScore },
            { TaskType.GatherSand, sandScore },
            { TaskType.StoreResources, ScoreStoreResources() },
            { TaskType.Idle, scoreIdle }
        };
        float bestScore = float.MaxValue;
        CurrentTask.Value = TaskType.Idle;
        foreach (var score in scores)
        {
            float tempScore = score.Value;
            if (tempScore < bestScore)
            {
                bestScore = tempScore;
                CurrentTask.Value = score.Key;
            }
        }
        return Status.Success;
    }

    private int TotalInventory()
    {
        return InventoryStone.Value + InventoryWood.Value + InventorySand.Value;
    }

    private int TotalResources()
    {
        return PlayerProgress.GlobalWood + PlayerProgress.GlobalStone + PlayerProgress.GlobalSand;
    }

    private float scoreIdle = float.MaxValue - 10;

    private float ScoreStoreResources()
    {
        if (TotalInventory() >= PlayerProgress.InventoryCapacity)
            return float.MinValue;
        return float.MaxValue;
    }

    private float SetScore(int globalResource, float distance, List<GameObject> onTheObject)
    {
        //Math is awful here the important part is system works
        float totalscore = 0f;
        if (TotalResources() != 0)
            totalscore += (float)globalResource / TotalResources();
        totalscore += distance / maxExpectedDistance;
        totalscore += (float)onTheObject.Count / maxExpectedAgent;
        return totalscore;
    }

    private void SetScores()
    {
        sandScore = SetScore(PlayerProgress.GlobalSand, DistanceToSand.Value, OnSand.Value);
        stoneScore = SetScore(PlayerProgress.GlobalStone, DistanceToStone.Value, OnStone.Value);
        woodScore = SetScore(PlayerProgress.GlobalWood, DistanceToWood.Value, OnWood.Value);
    }
}