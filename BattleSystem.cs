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
    public string text = "게임 시작!!";


    void Start()
    {
        // 게임 시작 시 초기화
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

        // 적의 체력이 0 이하인지 확인
        if (enemy.currentHealth <= 0)
        {
            Gameoverpanel.SetActive(true);
            battleResultText.text = "플레이어승!!";
            Time.timeScale = 0f; // 게임 일시정지
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
        
        // 플레이어의 체력이 0 이하인지 확인
        if (player.currentHealth <= 0)
        {
            Gameoverpanel.SetActive(true);
            battleResultText.text = "상대의 승...";
            Time.timeScale = 0f; // 게임 일시정지
        }
        
    }

    void Update()
    {
        // TODO: 사용자 입력 등을 받아 턴 기능을 구현
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerAttack();
        }
    }
}
