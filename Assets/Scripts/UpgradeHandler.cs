using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    // [SerializeField]
    // GameObject upgradePanelPrefab;
    // [SerializeField]
    // GameObject upgradeMainPanel;
    // void Start()
    // {
    //     GameObject upgradePanel = Instantiate(upgradePanelPrefab);
    //     upgradeMainPanel.transform.SetParent(upgradeMainPanel.transform);
    // }
    public void Upgrade(string upgradeName)
    {
        print(DataHandler.instance.upgrades);
    }
}
