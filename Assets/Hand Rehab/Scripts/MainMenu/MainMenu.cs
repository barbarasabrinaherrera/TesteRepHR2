using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameObject _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //_mainCamera = GameObject.Find("Main Camera");
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Vector3.up == null)
            Debug.LogError("Vector3 UP is null");
        if (Time.deltaTime.Equals(null))
            Debug.LogError("TIME DELTA TIME is null");
        if (Space.World == 0)
            Debug.LogError("SPACE WORLD is 0");
        _mainCamera.transform.Rotate(Vector3.up, 5 * Time.deltaTime, Space.World);*/
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        Debug.Log("PLAY GAME");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void SetDifficulty(Dropdown dropdown)
    {
        //DifficultyManager.gameDifficulty = (DifficultyManager.Difficulty)dropdown.value;
    }
}