using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string sceneName = "Stage1";
    public AudioSource backgroundMusic;   // ��� ������ ����� AudioSource
    public GameObject settingPanel;       // ���� �г�

    void Start()
    {
        // ��� ���� ���
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // �ʱ⿡ ���� �г� ��Ȱ��ȭ
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ClickStart()
    {
        Debug.Log("�ε�");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("�ε�");
        sceneName = "MainScreen";
        SceneManager.LoadScene(sceneName);
    }

    public void ClickSetting()
    {
        Debug.Log("����ȭ��");

        // ���� �г� Ȱ��ȭ
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
    }

    public void ClickBack()
    {
        Debug.Log("�ڷΰ���");

        // ���� �г� ��Ȱ��ȭ
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ClickExit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
