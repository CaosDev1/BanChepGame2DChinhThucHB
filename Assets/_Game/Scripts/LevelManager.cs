using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
