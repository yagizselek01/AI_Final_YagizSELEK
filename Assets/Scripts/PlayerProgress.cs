using UnityEngine;

public class PlayerProgress
{
    #region Global Variables

    public static bool isMapChanging = false;

    #endregion Global Variables

    #region Global Resources

    public static int GlobalWood = 0;

    public static int GlobalStone = 0;
    public static int GlobalSand = 0;

    #endregion Global Resources

    #region Upgrades

    #region HouseUpgrade

    public const int MaxHouseLevel = 24;

    private static int houseLevel = 1;

    public static int HouseLevel
    {
        get => houseLevel;
        set => houseLevel = Mathf.Clamp(value, 1, MaxHouseLevel);
    }

    #endregion HouseUpgrade

    #region SpeedUpgrade

    public const int MaxSpeedLevel = 6;

    private static int speedLevel = 1;

    public static int SpeedLevel
    {
        get => speedLevel;
        set => speedLevel = Mathf.Clamp(value, 1, MaxSpeedLevel);
    }

    #endregion SpeedUpgrade

    #region GatherUpgrade

    public const int MaxGatherSpeedLevel = 6;

    private static int gatherSpeedLevel = 1;

    public static int GatherSpeedLevel
    {
        get => gatherSpeedLevel;
        set => gatherSpeedLevel = Mathf.Clamp(value, 1, MaxGatherSpeedLevel);
    }

    #endregion GatherUpgrade

    #region CarryUpgrade

    //Carry Upgrade
    public const int MaxCarryLevel = 12;

    private static int carryLevel = 1;

    public static int CarryLevel
    {
        get => carryLevel;
        set => carryLevel = Mathf.Clamp(value, 1, MaxCarryLevel);
    }

    //inventory capacity
    public static int InventoryCapacity { get => carryLevel * 2; }

    #endregion CarryUpgrade

    #endregion Upgrades

    #region Costs

    private const int baseHouseCostStone = 4;
    private const int baseHouseCostSand = 4;
    private const int baseHouseCostWood = 4;
    private const float costMultiplier = 1.4f;
    private const float costMultiplierHouseOnly = 1.2f;

    public static int GetHouseCostWood()
    {
        if (houseLevel == 1) return 0;
        if (houseLevel == 2) return 2;
        return Mathf.CeilToInt(baseHouseCostWood * Mathf.Pow(costMultiplierHouseOnly, houseLevel - 1));
    }

    public static int GetHouseCostStone()
    {
        if (houseLevel == 1) return 2;
        if (houseLevel == 2) return 2;
        return Mathf.CeilToInt(baseHouseCostStone * Mathf.Pow(costMultiplierHouseOnly, houseLevel - 1));
    }

    public static int GetHouseCostSand()
    {
        if (houseLevel == 1) return 0;
        if (houseLevel == 2) return 2;
        return Mathf.CeilToInt(baseHouseCostSand * Mathf.Pow(costMultiplierHouseOnly, houseLevel - 1));
    }

    private const int baseSpeedCostSand = 8;

    public static int GetSpeedCostSand()
    {
        return Mathf.CeilToInt(baseSpeedCostSand * Mathf.Pow(costMultiplier, speedLevel - 1));
    }

    private const int baseGatherSpeedCostWood = 8;

    public static int GetGatherSpeedCostWood()
    {
        return Mathf.CeilToInt(baseGatherSpeedCostWood * Mathf.Pow(costMultiplier, gatherSpeedLevel - 1));
    }

    private const int baseCarryCostStone = 8;

    public static int GetCarryCostStone()
    {
        return Mathf.CeilToInt(baseCarryCostStone * Mathf.Pow(costMultiplier, carryLevel - 1));
    }

    #endregion Costs

    #region bools

    public static bool CanAffordHouseUpgrade()
    {
        return GlobalWood >= GetHouseCostWood() && GlobalStone >= GetHouseCostStone() && GlobalSand >= GetHouseCostSand();
    }

    public static bool CanAffordSpeedUpgrade()
    {
        return GlobalSand >= GetSpeedCostSand();
    }

    public static bool CanAffordGatherSpeedUpgrade()
    {
        return GlobalWood >= GetGatherSpeedCostWood();
    }

    public static bool CanAffordCarryUpgrade()
    {
        return GlobalStone >= GetCarryCostStone();
    }

    #endregion bools
}