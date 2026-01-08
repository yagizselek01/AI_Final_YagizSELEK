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

    protected override Status OnStart()
    {
        SetScores();
        Dictionary<TaskType, float> scores = new Dictionary<TaskType, float>
        {
            {  TaskType.GatherWood, woodScore  },
            { TaskType.GatherStone, stoneScore },
            { TaskType.GatherSand, sandScore },
            { TaskType.StoreResources, ScoreStoreResources() },
            { TaskType.BuildHouse, ScoreBuildHouse()},
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
        Debug.Log("Selected Task: " + CurrentTask.Value.ToString());
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
        Debug.Log("Total Inventory: " + TotalInventory());
        if (TotalInventory() >= PlayerProgress.InventoryCapacity)
            return float.MinValue;
        return float.MaxValue;
    }

    private float ScoreBuildHouse()
    {
        if (PlayerProgress.canBuildHouse)
            return 0f;
        return float.MaxValue;
    }

    private float SetScore(int globalResource, float distance, List<GameObject> onTheObject)
    {
        float totalscore = 0f;
        if (2 * (TotalResources() - globalResource) / 3 < globalResource)
            totalscore += 20f;
        totalscore += distance;
        totalscore += onTheObject.Count * 5f;
        return totalscore;
    }

    private void SetScores()
    {
        sandScore = SetScore(PlayerProgress.GlobalSand, DistanceToSand.Value, OnSand.Value);
        stoneScore = SetScore(PlayerProgress.GlobalStone, DistanceToStone.Value, OnStone.Value);
        woodScore = SetScore(PlayerProgress.GlobalWood, DistanceToWood.Value, OnWood.Value);
    }
}