using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField]
    GameObject upgradePanel;
    [SerializeField]
    float maxUpgradeAmount = 10;
    [SerializeField]
    GameObject currentPointsText;

    void Start()
    {
        UpdateCurrentPointsText();
        Dictionary<string, int> upgradesDict = DataHandler.instance.upgrades;
        foreach (KeyValuePair<string, int> pair in upgradesDict)
        {
            string upgradeName = pair.Key;
            float upgradeValue = DataHandler.instance.baseUpgrades[upgradeName];
            Slider sliderComponent = GetUpgradeSliderComponent(upgradeName);
            sliderComponent.minValue = upgradeValue;
            sliderComponent.maxValue = upgradeValue + maxUpgradeAmount;
            UpdateUpgradeVisuals(pair.Key);
        }
    }

    private Slider GetUpgradeSliderComponent(string upgradeName)
    {
        GameObject sliderGameObject = upgradePanel.transform.Find(upgradeName).Find("Slider").gameObject;
        Slider sliderComponent = sliderGameObject.GetComponent<Slider>();
        return sliderComponent;
    }

    private void UpdateUpgradeVisuals(string upgradeName)
    {
        Slider sliderComponent = GetUpgradeSliderComponent(upgradeName);
        float value = DataHandler.instance.upgrades[upgradeName];
        sliderComponent.GetComponent<Slider>().value = value;
    }

    private void UpdateCurrentPointsText()
    {
        float currentPoints = DataHandler.instance.points;
        string baseText = "Current Points: ";
        currentPointsText.GetComponent<TextMeshProUGUI>().text = baseText + currentPoints.ToString();
    }

    public void Upgrade(string upgradeName)
    {
        float currentPoints = DataHandler.instance.points;
        if (currentPoints <= 0)
        {
            return;
        }
        float currentUpgradeValue = DataHandler.instance.upgrades[upgradeName];
        float baseUpgradeValue = DataHandler.instance.baseUpgrades[upgradeName];
        float maxValue = baseUpgradeValue + maxUpgradeAmount;
        if (currentUpgradeValue >= maxValue)
        {
            return;
        }
        DataHandler.instance.upgrades[upgradeName] += 1;
        print(DataHandler.instance.upgrades[upgradeName]);
        DataHandler.instance.points--;
        UpdateUpgradeVisuals(upgradeName);
        UpdateCurrentPointsText();
    }
}
