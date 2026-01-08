using UnityEngine;

public class PlayerProgress
{
    //Variables that need to be accessed globally
    public static bool isMapChanging = false;

    //Resources
    public static int GlobalWood = 0;

    public static int GlobalStone = 0;
    public static int GlobalSand = 0;

    //Upgrades

    //House Upgrade
    public const int MaxHouseLevel = 24;

    private static int houseLevel = 1;

    public static bool canBuildHouse = false;

    public static int HouseLevel
    {
        get => houseLevel;
        set => houseLevel = Mathf.Clamp(value, 1, MaxHouseLevel);
    }

    //Speed Upgrade
    public const int MaxSpeedLevel = 12;

    private static int speedLevel = 1;

    public static int SpeedLevel
    {
        get => speedLevel;
        set => speedLevel = Mathf.Clamp(value, 1, MaxSpeedLevel);
    }

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

    //public readonly static int[,,] HouseCost =
}