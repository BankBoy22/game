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
        // �÷��̾��� �̸��� �����ͼ� NameText�� ǥ��
        DisplayPlayerName();
    }

    void DisplayPlayerName()
    {
        // PlayerPrefs���� ����� PlayerName�� ������
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultName");

        nameText.text = playerName;
    }
}
