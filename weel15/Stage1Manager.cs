using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stage1Manager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI CashText;


    void Start()
    {
        // 플레이어의 이름을 가져와서 NameText에 표시
        DisplayPlayerName();
    }

    void DisplayPlayerName()
    {
        // PlayerPrefs에서 저장된 PlayerName을 가져옴
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultName");

        nameText.text = playerName;
    }
}
