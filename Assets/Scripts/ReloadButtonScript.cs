using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadButtonScript : MonoBehaviour
{
    public Button reloadButton;
    public bool isGameOver = false; 

    private void Start()
    {
        
        reloadButton = GetComponent<Button>();

        reloadButton.onClick.AddListener(ReloadGame);

        reloadButton.enabled = false;
    }

    private void Update()
    {
        
        reloadButton.enabled = isGameOver;
    }

    private void ReloadGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }
}