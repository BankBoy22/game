using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Button : MonoBehaviour
{
    public RedHollowControl redHollowControl;
    public GameObject WhatIdo; 
    public GameObject Active; // �ൿ UI
    public GameObject Tenkai_button; // �������� ��ư
    public AudioSource buttonAudioSource;  
    public AudioClip Aka;
    public AudioClip Aao;
    public AudioClip Murasaki;
    public AudioClip Tenkai;

    public BattleSystem battleSystem;
    public SkyboxManager skyboxManager; // SkyboxManager ���� �߰�
    public CharacterStats playerStats; // CharacterStats ���� �߰�


    private void Start()
    {
        // Gojo Red Hollow ������Ʈ�� RedHollowControl ��ũ��Ʈ�� ���ٸ� ��� �޽����� ���
        if (redHollowControl == null)
        {
            Debug.LogWarning("RedHollowControl script is not assigned.");
        }
    }

    public void ClickBattleON()
    {
        // ���� �г� Ȱ��ȭ
        if (Active != null)
        {
            WhatIdo.SetActive(false);
            Active.SetActive(true);
        }
    }

    public void ClickActiveClose()
    {
        // ���� �г� Ȱ��ȭ
        if (WhatIdo != null)
        {
            WhatIdo.SetActive(true);
            Active.SetActive(false);
        }
    }

    //���Ĺ��� ��ī ��ư Ŭ�� �� ����� �Լ�
    public void ClickAka()
    {
        // ȿ���� ���
        if (buttonAudioSource != null && Aka != null)
        {
            buttonAudioSource.PlayOneShot(Aka);
        }
        // RedHollowControl ��ũ��Ʈ�� Play_Charging �Լ� ȣ��
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            redHollowControl.Burst_Beam();
            redHollowControl.Dead();
            Active.SetActive(false);
            redHollowControl.hue = 1.0F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "��";
            battleSystem.EnemyAttack();
    
        }
    }

    //���ļ��� �ƿ� ��ư Ŭ�� �� ����� �Լ�
    public void ClickAao()
    {
        // ȿ���� ���
        if (buttonAudioSource != null && Aao != null)
        {
            buttonAudioSource.PlayOneShot(Aao);
        }
        // RedHollowControl ��ũ��Ʈ�� Play_Charging �Լ� ȣ��
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            redHollowControl.Dead();
            Active.SetActive(false);
            redHollowControl.hue = 0.5F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "â";
            battleSystem.EnemyAttack();

        }
    }

    //��� �����Ű ��ư Ŭ�� �� ����� �Լ�
    public void ClickMurasaki()
    {
        // ȿ���� ���
        if (buttonAudioSource != null && Murasaki != null)
        {
            buttonAudioSource.PlayOneShot(Murasaki);
        }
        // RedHollowControl ��ũ��Ʈ�� Play_Charging �Լ� ȣ��
        if (redHollowControl != null)
        {
            redHollowControl.Play_Charging();
            Active.SetActive(false);
            redHollowControl.hue = 0.8F;
            battleSystem.PlayerAttack();
            battleSystem.battleLogText.text = "�����Ű";
            battleSystem.EnemyAttack();
        }
    }

    //�������� ��ư Ŭ�� �� ����� �Լ�
    public void ClickTenkai()
    {
        // ȿ���� ���
        if (buttonAudioSource != null && Tenkai != null)
        {
            buttonAudioSource.PlayOneShot(Tenkai);
        }
        // ���������� ������!
        if (redHollowControl != null)
        {
            Active.SetActive(false);
            battleSystem.battleLogText.text = "������ó";
            battleSystem.EnemyAttack();

            // CharacterStats�� attackDamage�� 15�� ����
            playerStats.attackDamage = 15;

            // �������� ��ư�� ������ ��ī�̹ڽ��� ����
            skyboxManager.SetExpandedSkybox();

            //��ư ���� (���������� �� ���Ӵ� �ѹ��� ��밡����)
            Tenkai_button.SetActive(false);
        }
    }
}
