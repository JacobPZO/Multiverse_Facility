using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int ChooseScene;
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(ChooseScene);
    }
}
