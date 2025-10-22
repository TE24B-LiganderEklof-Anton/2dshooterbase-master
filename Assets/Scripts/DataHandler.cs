using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    // declares the a static variable for this instance of the datahandler. Needs to be static since it is to be a shared beetween all instances of the datahandler.
    public static DataHandler instance;

    private void Awake() //needs to be awake() so it gets called before start()
    {
        //checks if there is already and instance, if yes then the current gameobject needs to be deleted to not create multiple datahandlers. If there is none then instance is set to this instance and the datahandler gameobject is set to not be destroyed on scene change
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        ApplyBaseUpgrades();
    }

    public Dictionary<string, int> baseUpgrades = new()
    {
        {"Health", 3},
        {"AttackSpeed", 3},
        {"MoveSpeed", 3},
        {"Damage", 3},
    };

    public void ApplyBaseUpgrades()
    {
        foreach(KeyValuePair<string, int> pair in baseUpgrades)
        {
            upgrades.Add(pair.Key,pair.Value);
        }
    }

    //data goes here:
    public float points = 0;
    public Dictionary<string, int> upgrades = new Dictionary<string, int>();
}
