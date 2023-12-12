using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Button : MonoBehaviour
{
    public RedHollowControl redHollowControl;
    public GameObject WhatIdo; 
    public GameObject Active; // 행동 UI
    public GameObject Tenkai_button; // 영역전개 버튼
    public AudioSource buttonAudioSource;  
    public AudioClip Aka;
    public AudioClip Aao;
    public AudioClip Murasaki;
    public AudioClip Tenkai;

    public BattleSystem battleSystem;
    public SkyboxManager skyboxManager; // SkyboxManager 참조 추가
    public CharacterStats playerStats; // CharacterStats 참조 추가


    private void Start()
    {
        // Gojo Red Hollow 오브젝트에 RedHollowControl 스크립트가 없다면 경고 메시지를 출력
        if (redHollowControl == null)
        {
            Debug.LogWarning("RedHollowControl script is not assigned.");
        }
    }

    public void ClickBattleON()
    {
        // 세팅 패널 활성화
        if (Active != null)
        {
            WhatIdo.SetActive(false);
            Active.SetActive(true);
        }
    }

    public void ClickActiveClose()
    {
        // 세팅 패널 활성화
        if (WhatIdo != null)
        {
            WhatIdo.SetActive(true);
            Active.SetActive(false);
        }
    }

    //술식반전 아카 버튼 클릭 시 실행될 함수
    public void ClickAka()
    {
        // 효과음 재생
        if (buttonAudioSource != null && Aka != null)
        {
            buttonAudioSource.PlayOneShot(Aka);
        }
        // RedHollowControl 스크립트의 Play_Charging 함수 호출
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            redHollowControl.Burst_Beam();
            redHollowControl.Dead();
            Active.SetActive(false);
            redHollowControl.hue = 1.0F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "혁";
            battleSystem.EnemyAttack();
    
        }
    }

    //술식순전 아오 버튼 클릭 시 실행될 함수
    public void ClickAao()
    {
        // 효과음 재생
        if (buttonAudioSource != null && Aao != null)
        {
            buttonAudioSource.PlayOneShot(Aao);
        }
        // RedHollowControl 스크립트의 Play_Charging 함수 호출
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            redHollowControl.Dead();
            Active.SetActive(false);
            redHollowControl.hue = 0.5F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "창";
            battleSystem.EnemyAttack();

        }
    }

    //허식 무라사키 버튼 클릭 시 실행될 함수
    public void ClickMurasaki()
    {
        // 효과음 재생
        if (buttonAudioSource != null && Murasaki != null)
        {
            buttonAudioSource.PlayOneShot(Murasaki);
        }
        // RedHollowControl 스크립트의 Play_Charging 함수 호출
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            Active.SetActive(false);
            redHollowControl.hue = 0.8F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "무라사키";
            battleSystem.EnemyAttack();
        }
    }

    //영역전개 버튼 클릭 시 실행될 함수
    public void ClickTenkai()
    {
        // 효과음 재생
        if (buttonAudioSource != null && Tenkai != null)
        {
            buttonAudioSource.PlayOneShot(Tenkai);
        }
        // 영역전개는 버프다!
        if (redHollowControl != null)
        {
            Active.SetActive(false);
            battleSystem.battleLogText.text = "무량공처";
            battleSystem.EnemyAttack();

            // CharacterStats의 attackDamage를 15로 변경
            playerStats.attackDamage = 15;

            // 영역전개 버튼이 눌리면 스카이박스를 변경
            skyboxManager.SetExpandedSkybox();

            //버튼 제거 (영역전개는 한 게임당 한번만 사용가능함)
            Tenkai_button.SetActive(false);
        }
    }
}
