using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Stage1";
    public AudioSource backgroundMusic;   // 배경 음악을 재생할 AudioSource
    public GameObject settingPanel;       // 세팅 패널

    void Start()
    {
        // 배경 음악 재생
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // 초기에 세팅 패널 비활성화
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("로드");
        sceneName = "MainScreen";
        SceneManager.LoadScene(sceneName);
    }

    public void ClickSetting()
    {
        Debug.Log("세팅화면");

        // 세팅 패널 활성화
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    public void ClickBack()
    {
        Debug.Log("뒤로가기");

        // 세팅 패널 비활성화
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
