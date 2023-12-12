using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public CharacterStats player;
    public EnemyStats enemy;
    public GameObject WhatIdo;
    public GameObject Gameoverpanel;
    public TextMeshProUGUI battleLogText;
    public TextMeshProUGUI battleResultText;
    public string text = "���� ����!!";


    void Start()
    {
        // ���� ���� �� �ʱ�ȭ
        StartBattle();
        battleLogText.text = text;
    }

    void StartBattle()
    {
        
    }

    public void PlayerAttack()
    {
        int damageDealt = player.attackDamage;
        enemy.TakeDamage(damageDealt);
        WhatIdo.SetActive(true);

        // ���� ü���� 0 �������� Ȯ��
        if (enemy.currentHealth <= 0)
        {
            Gameoverpanel.SetActive(true);
            battleResultText.text = "�÷��̾��!!";
            Time.timeScale = 0f; // ���� �Ͻ�����
        }
        
    }
    public void EnemyAttack()
    {

        int damageDealt = enemy.attackDamage;
        player.TakeDamage(damageDealt);
        if(WhatIdo.activeSelf ==  false)
        {
            WhatIdo.SetActive(true);
        }
        
        // �÷��̾��� ü���� 0 �������� Ȯ��
        if (player.currentHealth <= 0)
        {
            Gameoverpanel.SetActive(true);
            battleResultText.text = "����� ��...";
            Time.timeScale = 0f; // ���� �Ͻ�����
        }
        
    }

    void Update()
    {
        // TODO: ����� �Է� ���� �޾� �� ����� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }
    }
}
