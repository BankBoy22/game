using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Button : MonoBehaviour
{
    public GameObject gameModePanel; // 게임모드 선택 패널 오브젝트
    public GameObject settingPanel; // 게임설정 선택 패널 오브젝트

    void Start()
    {
        // 게임 시작 시 게임모드 패널을 비활성화
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(false);
        }
    }

    // 게임 시작 버튼을 눌렀을 때 호출되는 함수
    public void OnGameStartButtonClicked()
    {
        // 게임모드 패널을 활성화
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(true);
        }
    }

    // X 버튼을 눌렀을 때 호출되는 함수
    public void OnCloseButtonClicked()
    {
        // 게임모드 패널을 비활성화
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(false);
        }
    }

    // 세팅 버튼을 눌렸을때 호출되는 함수
    public void OnSettingButtonClicked()
    {
        // 게임모드 패널을 비활성화
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    //세팅 닫기 버튼을 눌렸을때 호출되는 함수
    public void OnSettingcloseButtonClicked()
    {
        // 게임모드 패널을 비활성화
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void OnButtonClickToScene(string A)
    {
        SceneManager.LoadScene(A);
    }

    public void OnDeckButtonClicked()
    {
        SceneManager.LoadScene("DeckSceen");
    }

    public void OnBattleButtonClicked()
    {
        SceneManager.LoadScene("Battle_Scene");
    }

    public void OnCashShopButtonClicked()
    {
        SceneManager.LoadScene("Shop");
    }

    //Home으로 이동
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("Intro");
    }

    //뽑기화면으로 이동
    public void OnGotchaClicked()
    {
        SceneManager.LoadScene("GotchaSceen");
    }
}
