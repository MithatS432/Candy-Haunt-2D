using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public Button howtoplayButton;
    public GameObject howtoplayPanel;
    public Button quitButton;
    private Animator panelAnimator;
    private void Start()
    {
        panelAnimator = GetComponent<Animator>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Halloween Party");
    }
    public void ShowHowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }
    public void QuitGame()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
