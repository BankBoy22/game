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
        // �Ͻ����� ��ư�� ������ ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // �Ͻ����� �г��� Ȱ��ȭ ���¸� ���
        pausePanel.SetActive(!pausePanel.activeSelf);

        // ���� �Ͻ����� ���ο� ���� �ð� �帧 ����
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
        Time.timeScale = 1f; // ���� �簳
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // ���� �Ͻ�����
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame(); // ���� �簳
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(BackToHome); // MainMenu ������ �̵�
        ResumeGame(); // ���� �簳
    }
}
