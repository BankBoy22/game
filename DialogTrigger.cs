using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogueUI;  // 대화 UI 오브젝트
    public TextMeshProUGUI NPC_Name; // NPC 이름
    public TextMeshProUGUI NPC_Description; //NPC 대화내용
    public Image NPC_Image; // NPC 이미지를 표시할 Image 컴포넌트

    private bool isInRange = false;  // 플레이어가 대화 범위에 있는지 여부
    public string Npc_name;
    public string Npc_description;
    public Sprite npcImage; // NPC 이미지를 저장할 Sprite

    private void OnTriggerEnter(Collider other)
    {
        // 콜라이더 진입 시 호출되는 이벤트
        NPC_Name.text = Npc_name;
        NPC_Description.text = Npc_description;
        NPC_Image.sprite = npcImage; // 새 이미지로 변경
        // 만약 진입한 오브젝트가 "Player" 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // 여기에 추가적인 대화 처리 로직을 추가할 수 있습니다.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 콜라이더에서 나갈 때 호출되는 이벤트

        // 만약 나가는 오브젝트가 "Player" 태그를 가진 경우
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            dialogueUI.SetActive(false);

            // 여기에 추가적인 처리 로직을 추가할 수 있습니다.
        }
    }

    private void Update()
    {
        // E 키 입력을 감지
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // 대화 UI를 현재 상태의 반대로 설정 (활성화 <-> 비활성화)
            dialogueUI.SetActive(!dialogueUI.activeSelf);
        }
    }
}
