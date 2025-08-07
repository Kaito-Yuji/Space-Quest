using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float worldSpeed;
    public int critterCounter;
    private ObjectPooler boss1Pool;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        boss1Pool = GameObject.Find("Boss1Pool").GetComponent<ObjectPooler>();
        critterCounter = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }
        if (critterCounter >= 15)
        {
            critterCounter = 0;
            GameObject boss1 = boss1Pool.GetPooledObject();
            boss1.transform.position = new Vector2(15f, 0f); // Set the position of the boss
            boss1.transform.rotation = Quaternion.Euler(0,0,-90); // Reset the rotation of the boss
            boss1.SetActive(true); 
        }
    }
    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game  
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f; // Resume the game
            PlayerController.Instance.ExitBoost(); // Exit boost if paused
            AudioManager.Instance.PlaySound(AudioManager.Instance.unpause);
        }
    }
    public void QuitGame()
    {
        Application.Quit(); // Quit the game
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene

    }
    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen()); // Start the coroutine to show the game over screen
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f); // Wait for 2 seconds before showing the game over screen
        SceneManager.LoadScene("GameOver"); // Load the game over scene
    }
    public void SetWorldSpeed(float speed)
    {
        worldSpeed = speed; 
    }
}
