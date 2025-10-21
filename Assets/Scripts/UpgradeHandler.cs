using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField]
    GameObject upgradePanel;
    [SerializeField]
    float maxUpgradeAmount = 10;

    void Start()
    {
        Dictionary<string, int> upgradesDict = DataHandler.instance.upgrades;
        foreach (KeyValuePair<string, int> pair in upgradesDict)
        {
            string upgradeName = pair.Key;
            float upgradeValue = DataHandler.instance.upgrades[upgradeName];
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

    public void Upgrade(string upgradeName)
    {
        float currentPoints = DataHandler.instance.points;
        // if (currentPoints <= 0)
        // {
        //     return;
        // }

        DataHandler.instance.upgrades[upgradeName] += 1;
        print(DataHandler.instance.upgrades[upgradeName]);
        DataHandler.instance.points--;
        UpdateUpgradeVisuals(upgradeName);
    }
}
