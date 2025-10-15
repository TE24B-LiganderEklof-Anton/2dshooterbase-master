using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        print(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
