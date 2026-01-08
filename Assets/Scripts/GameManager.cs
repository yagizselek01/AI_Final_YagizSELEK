using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("House References")]
    [SerializeField] private List<GameObject> agentHouses;

    [SerializeField] private List<GameObject> speedHouses;
    [SerializeField] private List<GameObject> gatheringHouses;
    [SerializeField] private List<GameObject> carryHouses;

    [Header("Agent References")]
    [SerializeField] private List<GameObject> agents;

    [SerializeField] private BehaviorGraph behaviorGraph;

    [Header("Settings")]
    [SerializeField] private float updateInterval = 3f;

    private int lastAgentLevel = 0;
    private int lastSpeedLevel = 0;
    private int lastGatheringLevel = 0;
    private int lastCarryLevel = 0;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateHouseLevels), 0f, updateInterval);
    }

    private void UpdateHouseLevels()
    {
        UpdateHouseList(agentHouses, PlayerProgress.HouseLevel, ref lastAgentLevel, nameof(GetAgents));
        UpdateHouseList(speedHouses, PlayerProgress.SpeedLevel, ref lastSpeedLevel, nameof(Empty));
        UpdateHouseList(gatheringHouses, PlayerProgress.GatherSpeedLevel, ref lastGatheringLevel, nameof(Empty));
        UpdateHouseList(carryHouses, PlayerProgress.CarryLevel, ref lastCarryLevel, nameof(Empty));
    }

    private void UpdateHouseList(List<GameObject> houses, int currentLevel, ref int cachedLevel, string methodName)
    {
        if (houses == null || houses.Count == 0) return;

        if (currentLevel == cachedLevel) return;

        for (int i = cachedLevel; i < currentLevel && i < houses.Count; i++)
        {
            if (!houses[i].activeSelf)
                houses[i].SetActive(true);
        }
        Invoke(methodName, currentLevel);
        cachedLevel = currentLevel;
    }

    private void GetAgents()
    {
        agents[lastAgentLevel - 1].SetActive(true);
        agents[lastAgentLevel - 1].GetComponent<BehaviorGraphAgent>().Graph = behaviorGraph;
        agents[lastAgentLevel - 1].GetComponent<BehaviorGraphAgent>().enabled = true;
    }

    private void Empty()
    {
        // Placeholder
    }
}