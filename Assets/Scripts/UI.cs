using NUnit.Framework;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] resourceTexts;

    private void Update()
    {
        resourceTexts[0].text = $"Wood: {PlayerProgress.GlobalWood}";
        resourceTexts[1].text = $"Stone: {PlayerProgress.GlobalStone}";
        resourceTexts[2].text = $"Sand: {PlayerProgress.GlobalSand}";
    }
}