using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region variables

    [SerializeField] private TMP_Text[] resourceTexts;
    [SerializeField] public TMP_Text houseNameText;
    [SerializeField] public TMP_Text houseCostText;
    [SerializeField] private Button houseUpgradeButton;
    [SerializeField] public TMP_Text speedNameText;
    [SerializeField] public TMP_Text speedCostText;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] public TMP_Text gatherSpeedNameText;
    [SerializeField] public TMP_Text gatherSpeedCostText;
    [SerializeField] private Button gatherSpeedUpgradeButton;
    [SerializeField] public TMP_Text carryNameText;
    [SerializeField] public TMP_Text carryCostText;
    [SerializeField] private Button carryUpgradeButton;
    [SerializeField] private TMP_Text gameOverText;

    private string notAffordableText = "Not Affordable";
    private string upgradeText = "Upgrade";

    #endregion variables

    private void OnEnable()
    {
        InvokeRepeating(nameof(LookingForCost), 0f, 0.5f);
    }
    private void LateUpdate()
    {
        resourceTexts[0].text = $"Wood: {PlayerProgress.GlobalWood}";
        resourceTexts[1].text = $"Stone: {PlayerProgress.GlobalStone}";
        resourceTexts[2].text = $"Sand: {PlayerProgress.GlobalSand}";
        HouseTexts();
        SpeedTexts();
        GatherSpeedTexts();
        CarryTexts();
        if (PlayerProgress.HouseLevel >= PlayerProgress.MaxHouseLevel && PlayerProgress.SpeedLevel >= PlayerProgress.MaxSpeedLevel &&
           PlayerProgress.GatherSpeedLevel >= PlayerProgress.MaxGatherSpeedLevel && PlayerProgress.CarryLevel >= PlayerProgress.MaxCarryLevel)
        {
            gameOverText.gameObject.SetActive(true);
        }//Game Over
    }

    private void HouseTexts()
    {
        houseNameText.text = $"House (Level {PlayerProgress.HouseLevel})";
        houseCostText.text = $"Cost: {PlayerProgress.GetHouseCostStone()} Stone, {PlayerProgress.GetHouseCostSand()} Sand, {PlayerProgress.GetHouseCostWood()} Wood";
    }

    private void SpeedTexts()
    {
        speedNameText.text = $"Speed (Level {PlayerProgress.SpeedLevel})";
        speedCostText.text = $"Cost: {PlayerProgress.GetSpeedCostSand()} Sand";
    }

    private void GatherSpeedTexts()
    {
        gatherSpeedNameText.text = $"Gather Speed (Level {PlayerProgress.GatherSpeedLevel})";
        gatherSpeedCostText.text = $"Cost: {PlayerProgress.GetGatherSpeedCostWood()} Wood";
    }

    private void CarryTexts()
    {
        carryNameText.text = $"Carry Capacity (Level {PlayerProgress.CarryLevel})";
        carryCostText.text = $"Cost: {PlayerProgress.GetCarryCostStone()} stone";
    }

    public void HouseButton()
    {
        houseUpgradeButton.interactable = false;
        speedUpgradeButton.interactable = false;
        gatherSpeedUpgradeButton.interactable = false;
        carryUpgradeButton.interactable = false;
        if (PlayerProgress.CarryLevel < PlayerProgress.MaxCarryLevel)
            carryUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.HouseLevel < PlayerProgress.MaxHouseLevel)
            houseUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.GatherSpeedLevel < PlayerProgress.MaxGatherSpeedLevel)
            gatherSpeedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.SpeedLevel < PlayerProgress.MaxSpeedLevel)
            speedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        PlayerProgress.GlobalStone -= PlayerProgress.GetHouseCostStone();
        PlayerProgress.GlobalSand -= PlayerProgress.GetHouseCostSand();
        PlayerProgress.GlobalWood -= PlayerProgress.GetHouseCostWood();
        PlayerProgress.HouseLevel++;
    }

    public void SpeedButton()
    {
        houseUpgradeButton.interactable = false;
        speedUpgradeButton.interactable = false;
        gatherSpeedUpgradeButton.interactable = false;
        carryUpgradeButton.interactable = false;
        if (PlayerProgress.CarryLevel < PlayerProgress.MaxCarryLevel)
            carryUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.HouseLevel < PlayerProgress.MaxHouseLevel)
            houseUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.GatherSpeedLevel < PlayerProgress.MaxGatherSpeedLevel)
            gatherSpeedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.SpeedLevel < PlayerProgress.MaxSpeedLevel)
            speedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        PlayerProgress.GlobalSand -= PlayerProgress.GetSpeedCostSand();
        PlayerProgress.SpeedLevel++;
    }

    public void GatherSpeedButton()
    {
        houseUpgradeButton.interactable = false;
        speedUpgradeButton.interactable = false;
        gatherSpeedUpgradeButton.interactable = false;
        carryUpgradeButton.interactable = false;
        if (PlayerProgress.CarryLevel < PlayerProgress.MaxCarryLevel)
            carryUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.HouseLevel < PlayerProgress.MaxHouseLevel)
            houseUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.GatherSpeedLevel < PlayerProgress.MaxGatherSpeedLevel)
            gatherSpeedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.SpeedLevel < PlayerProgress.MaxSpeedLevel)
            speedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        PlayerProgress.GlobalWood -= PlayerProgress.GetGatherSpeedCostWood();
        PlayerProgress.GatherSpeedLevel++;
    }

    public void CarryButton()
    {
        houseUpgradeButton.interactable = false;
        speedUpgradeButton.interactable = false;
        gatherSpeedUpgradeButton.interactable = false;
        carryUpgradeButton.interactable = false;
        if (PlayerProgress.CarryLevel < PlayerProgress.MaxCarryLevel)
            carryUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.HouseLevel < PlayerProgress.MaxHouseLevel)
            houseUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.GatherSpeedLevel < PlayerProgress.MaxGatherSpeedLevel)
            gatherSpeedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        if (PlayerProgress.SpeedLevel < PlayerProgress.MaxSpeedLevel)
            speedUpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().text = notAffordableText;
        PlayerProgress.GlobalStone -= PlayerProgress.GetCarryCostStone();
        PlayerProgress.CarryLevel++;
    }

    private void LookingForCost()
    {
        if (PlayerProgress.HouseLevel >= PlayerProgress.MaxHouseLevel)// Max Level Check
        {
            houseUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Max Level";
            houseUpgradeButton.interactable = false;
        }
        else if (PlayerProgress.CanAffordHouseUpgrade())
        {
            houseUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeText;
            houseUpgradeButton.interactable = true;
        }
        if (PlayerProgress.SpeedLevel >= PlayerProgress.MaxSpeedLevel)// Max Level Check
        {
            speedUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Max Level";
            speedUpgradeButton.interactable = false;
        }
        else if (PlayerProgress.CanAffordSpeedUpgrade())
        {
            speedUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeText;
            speedUpgradeButton.interactable = true;
        }
        if (PlayerProgress.GatherSpeedLevel >= PlayerProgress.MaxGatherSpeedLevel)// Max Level Check
        {
            gatherSpeedUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Max Level";
            gatherSpeedUpgradeButton.interactable = false;
        }
        else if (PlayerProgress.CanAffordGatherSpeedUpgrade())
        {
            gatherSpeedUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeText;
            gatherSpeedUpgradeButton.interactable = true;
        }
        if (PlayerProgress.CarryLevel >= PlayerProgress.MaxCarryLevel)// Max Level Check
        {
            carryUpgradeButton.GetComponentInChildren<TMP_Text>().text = "Max Level";
            carryUpgradeButton.interactable = false;
        }
        else if (PlayerProgress.CanAffordCarryUpgrade())
        {
            carryUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeText;
            carryUpgradeButton.interactable = true;
        }
    }
}