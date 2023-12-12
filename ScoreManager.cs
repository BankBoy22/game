using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject Finsh_panel; // ����Ŭ���� UI ������Ʈ
    public static ScoreManager instance;  // ScoreManager�� �ν��Ͻ�
    public int score = 0;  // ���� ����
    public TMPro.TextMeshProUGUI scoreText;  // ������ ǥ���� TextMeshProUGUI


    void Awake()
    {
        // �̱��� ������ ����Ͽ� ScoreManager �ν��Ͻ� ����
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        // ���� �߰�
        score += amount;
        scoreText.text = " " + score;  // ���� ���� �� TextMeshProUGUI ������Ʈ

        // ���� ���� ���� Ȯ��
        if (score >= 6)
        {
            Debug.Log("��������");
            Time.timeScale = 0f; // ���� �Ͻ�����
            Finsh_panel.SetActive(true);
        }
    }

    public void onclickGotoMain()
    {
        Time.timeScale = 1f; // ���� �簳
        SceneManager.LoadScene("MainScreen");
    }
}
