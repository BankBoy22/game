using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public string BackToHome;

    private void Update()
    {
        // 일시정지 버튼을 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // 일시정지 패널의 활성화 상태를 토글
        pausePanel.SetActive(!pausePanel.activeSelf);

        // 게임 일시정지 여부에 따라 시간 흐름 제어
        if (pausePanel.activeSelf)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // 게임 재개
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame(); // 게임 재개
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(BackToHome); // MainMenu 씬으로 이동
        ResumeGame(); // 게임 재개
    }
}
