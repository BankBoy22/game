using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutoriaDialogManager : MonoBehaviour
{
    public GameObject tutorialUI; // 안내 UI 오브젝트
    public TextMeshProUGUI dialogueText; // 대화 텍스트를 표시할 TextMeshProUGUI 컴포넌트
    public string[] tutorialSentences; // 튜토리얼 대화 내용 배열
    private int index = 0; // 대화 진행을 나타내는 인덱스
    private bool isTyping = false; // 텍스트 입력 중인지 여부

    void Start()
    {
        StartCoroutine(TypeSentence(tutorialSentences[index]));
    }

    // Update is called once per frame
    void Update()
    {
        // Space바를 눌렀을 때 다음 대화로 진행
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // 텍스트 입력 중으로 설정
        dialogueText.text = ""; // 텍스트 초기화

        // 한 글자씩 텍스트 표시
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 간격 조절
        }

        isTyping = false; // 텍스트 입력 종료
    }

    void NextSentence()
    {
        // 텍스트 입력 중이 아니면 사용자 입력으로 다음 대화로 진행
        if (!isTyping)
        {
            // 다음 대화로 진행
            if (index < tutorialSentences.Length - 1)
            {
                index++;
                StartCoroutine(TypeSentence(tutorialSentences[index]));
            }
            else
            {
                // 튜토리얼이 끝나면 종료
                EndTutorial();
            }
        }
    }

    void EndTutorial()
    {
        Debug.Log("튜토리얼 종료");

        // 튜토리얼이 종료되면 인게임 시작 등의 로직 추가 가능

        // 안내 UI를 비활성화
        tutorialUI.SetActive(false);
    }
}
