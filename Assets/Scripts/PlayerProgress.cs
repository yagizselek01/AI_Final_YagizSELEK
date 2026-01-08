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

    public static bool canBuildHouse = false;

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

    //public readonly static int[,,] HouseCost =
}