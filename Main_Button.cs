using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Button : MonoBehaviour
{
    public GameObject gameModePanel; // ���Ӹ�� ���� �г� ������Ʈ
    public GameObject settingPanel; // ���Ӽ��� ���� �г� ������Ʈ

    void Start()
    {
        // ���� ���� �� ���Ӹ�� �г��� ��Ȱ��ȭ
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(false);
        }
    }

    // ���� ���� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnGameStartButtonClicked()
    {
        // ���Ӹ�� �г��� Ȱ��ȭ
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(true);
        }
    }

    // X ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnCloseButtonClicked()
    {
        // ���Ӹ�� �г��� ��Ȱ��ȭ
        if (gameModePanel != null)
        {
            gameModePanel.SetActive(false);
        }
    }

    // ���� ��ư�� �������� ȣ��Ǵ� �Լ�
    public void OnSettingButtonClicked()
    {
        // ���Ӹ�� �г��� ��Ȱ��ȭ
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    //���� �ݱ� ��ư�� �������� ȣ��Ǵ� �Լ�
    public void OnSettingcloseButtonClicked()
    {
        // ���Ӹ�� �г��� ��Ȱ��ȭ
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

    //Home���� �̵�
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("Intro");
    }

    //�̱�ȭ������ �̵�
    public void OnGotchaClicked()
    {
        SceneManager.LoadScene("GotchaSceen");
    }
}
