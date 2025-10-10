using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public Button howtoplayButton;
    public Button quitButton;
    public GameObject howtoplayPanel;

    private bool isPanelOpen = false;
    private Vector3 playStartPos, howStartPos, quitStartPos;
    private Vector3 playTargetPos, howTargetPos, quitTargetPos;

    void Start()
    {
        howtoplayPanel.SetActive(false);

        playStartPos = playButton.transform.localPosition;
        howStartPos = howtoplayButton.transform.localPosition;
        quitStartPos = quitButton.transform.localPosition;

        playTargetPos = playStartPos + new Vector3(-501, 0, 0);
        howTargetPos = howStartPos + new Vector3(-501, 0, 0);
        quitTargetPos = quitStartPos + new Vector3(-501, 0, 0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Halloween Party");
    }

    public void ShowHowToPlay()
    {
        if (!isPanelOpen)
        {
            StartCoroutine(SlideButtons(playTargetPos, true));
        }
        else
        {
            StartCoroutine(SlideButtons(playStartPos, false));
        }
        isPanelOpen = !isPanelOpen;
    }

    IEnumerator SlideButtons(Vector3 targetPos, bool openPanel)
    {
        float elapsed = 0f;
        float duration = 0.4f;

        Vector3 playInitial = playButton.transform.localPosition;
        Vector3 howInitial = howtoplayButton.transform.localPosition;
        Vector3 quitInitial = quitButton.transform.localPosition;

        if (openPanel)
            yield return new WaitForSeconds(0.1f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            playButton.transform.localPosition = Vector3.Lerp(playInitial, openPanel ? playTargetPos : playStartPos, t);
            howtoplayButton.transform.localPosition = Vector3.Lerp(howInitial, openPanel ? howTargetPos : howStartPos, t);
            quitButton.transform.localPosition = Vector3.Lerp(quitInitial, openPanel ? quitTargetPos : quitStartPos, t);

            yield return null;
        }

        if (openPanel)
            howtoplayPanel.SetActive(true);
        else
            howtoplayPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
