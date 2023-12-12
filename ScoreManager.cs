using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public GameObject Finsh_panel; // 게임클리어 UI 오브젝트
    public static ScoreManager instance;  // ScoreManager의 인스턴스
    public int score = 0;  // 현재 점수
    public TMPro.TextMeshProUGUI scoreText;  // 점수를 표시할 TextMeshProUGUI


    void Awake()
    {
        // 싱글톤 패턴을 사용하여 ScoreManager 인스턴스 설정
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
        // 점수 추가
        score += amount;
        scoreText.text = " " + score;  // 점수 변경 시 TextMeshProUGUI 업데이트

        // 게임 종료 조건 확인
        if (score >= 6)
        {
            Debug.Log("게임종료");
            Time.timeScale = 0f; // 게임 일시정지
            Finsh_panel.SetActive(true);
        }
    }

    public void onclickGotoMain()
    {
        Time.timeScale = 1f; // 게임 재개
        SceneManager.LoadScene("MainScreen");
    }
}
