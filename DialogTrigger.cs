using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogueUI;  // ��ȭ UI ������Ʈ
    public TextMeshProUGUI NPC_Name; // NPC �̸�
    public TextMeshProUGUI NPC_Description; //NPC ��ȭ����
    public Image NPC_Image; // NPC �̹����� ǥ���� Image ������Ʈ

    private bool isInRange = false;  // �÷��̾ ��ȭ ������ �ִ��� ����
    public string Npc_name;
    public string Npc_description;
    public Sprite npcImage; // NPC �̹����� ������ Sprite

    private void OnTriggerEnter(Collider other)
    {
        // �ݶ��̴� ���� �� ȣ��Ǵ� �̺�Ʈ
        NPC_Name.text = Npc_name;
        NPC_Description.text = Npc_description;
        NPC_Image.sprite = npcImage; // �� �̹����� ����
        // ���� ������ ������Ʈ�� "Player" �±׸� ���� ���
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // ���⿡ �߰����� ��ȭ ó�� ������ �߰��� �� �ֽ��ϴ�.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �ݶ��̴����� ���� �� ȣ��Ǵ� �̺�Ʈ

        // ���� ������ ������Ʈ�� "Player" �±׸� ���� ���
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            dialogueUI.SetActive(false);

            // ���⿡ �߰����� ó�� ������ �߰��� �� �ֽ��ϴ�.
        }
    }

    private void Update()
    {
        // E Ű �Է��� ����
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // ��ȭ UI�� ���� ������ �ݴ�� ���� (Ȱ��ȭ <-> ��Ȱ��ȭ)
            dialogueUI.SetActive(!dialogueUI.activeSelf);
        }
    }
}
