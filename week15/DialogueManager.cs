using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;      // 대화 텍스트를 표시할 TextMeshProUGUI 컴포넌트
    public TextMeshProUGUI instructionText;   // "space바를 누르세요" 텍스트를 표시할 TextMeshProUGUI 컴포넌트
    public string[] sentences;                // 대화 내용 배열
    private int index = 0;                    // 대화 진행을 나타내는 인덱스
    public VideoPlayer videoPlayer;           // VideoPlayer 컴포넌트 연결
    public AudioSource backgroundMusic;      // 배경 음악을 재생할 AudioSource
    public GameObject inputNamePanel;         // 이름을 입력받는 패널
    public TMP_InputField nameInputField;     // 이름을 입력받는 InputField
    public string NextScene;
    private string playerName; //플레이어의 이름

    private bool isTyping = false;            // 텍스트 입력 중인지 여부
    private bool isfirst = true;              // 첫대화인지 여부

    void Start()
    {
        // 배경 음악 재생
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        StartCoroutine(TypeSentence(sentences[index]));
        StartCoroutine(BlinkInstructionText());
    }

    void Update()
    {
        // 텍스트 입력 중이 아니면 사용자 입력으로 다음 대화로 진행
        if (!isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;   // 텍스트 입력 중으로 설정
        dialogueText.text = "";  // 텍스트 초기화

        // 한 글자씩 텍스트 표시
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 간격 조절
        }

        isTyping = false;  // 텍스트 입력 종료

        // 대화가 모두 끝나면 InputName 패널 활성화
        if (index == sentences.Length - 1 && isfirst == true)
        {
            ActivateInputNamePanel();
            isfirst = false;
        }
        
    }

    IEnumerator TypeSentence2(string sentence)
    {
        isTyping = true;   // 텍스트 입력 중으로 설정
        dialogueText.text = "";  // 텍스트 초기화

        // 한 글자씩 텍스트 표시
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 간격 조절
        }

        isTyping = false;  // 텍스트 입력 종료
    }


    IEnumerator BlinkInstructionText()
    {
        while (true)
        {
            instructionText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            instructionText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void NextSentence()
    {
        // 다음 대화로 진행
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence(sentences[index]));
        }
        else
        {
            // 대화가 끝나면 원하는 동작 수행 (예: 다음 씬으로 이동)
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Debug.Log("대화 종료");
        // 추가적인 동작을 수행하거나 다음 로직을 추가할 수 있음

        // 대화 종료 후 영상 재생
        PlayVideo();
    }

    void PlayVideo()
    {
        // 영상 재생
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    void ActivateInputNamePanel()
    {
        // InputName 패널이 비활성화되어 있을 때만 활성화
        if (inputNamePanel != null && !inputNamePanel.activeSelf)
        {
            inputNamePanel.SetActive(true);
        }
    }

    public void OnSubmitName()
    {
        // 이름 제출 버튼 클릭 시 호출되는 함수
        playerName = nameInputField.text;
        Debug.Log("Player Name: " + playerName);

        // PlayerPrefs를 사용하여 플레이어의 이름 저장
        PlayerPrefs.SetString("PlayerName", playerName);

        // Player 정보 초기화
        InitializePlayerStats();

        // 대화 내용 추가 및 실행
        AddAndRunAdditionalDialogue();

        // InputName 패널 비활성화
        if (inputNamePanel != null)
        {
            inputNamePanel.SetActive(false);
        }
    }

    void AddAndRunAdditionalDialogue()
    {
        // 여기에 추가 대화 내용을 추가하고 실행하는 로직을 작성
        // 예를 들어, "씨 환영합니다! 자 테스트를 시작해봅시다" 등의 대화 내용을 추가하고 실행
        sentences = new string[]
        {
            PlayerPrefs.GetString("PlayerName") + "씨 환영합니다!",
            "자, 테스트를 시작해봅시다!"
        };
        // 추가된 대화 내용 실행
        index = 0;
        StartCoroutine(TypeSentence2(sentences[index]));


        // 2초 뒤에 다음 씬으로 이동
        StartCoroutine(DelayedSceneChange(4f));
    }

    IEnumerator DelayedSceneChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 여기에 다음 씬으로 이동하는 로직을 추가
        SceneManager.LoadScene(NextScene);
    }

    void InitializePlayerStats()
    {
        // 플레이어의 초기 레벨, Money, 경험치 설정 및 저장
        int initialLevel = 1;
        int initialMoney = 0;
        int initialcash = 0;
        int initialExperience = 0;
        int initialMaxExperience = 100;

        // 플레이어 정보 저장
        PlayerPrefs.SetInt("PlayerLevel", initialLevel);
        PlayerPrefs.SetInt("PlayerMoney", initialMoney);
        PlayerPrefs.SetInt("PlayerCash", initialcash);
        PlayerPrefs.SetInt("PlayerExperience", initialExperience);
        PlayerPrefs.SetInt("PlayerMaxExperience", initialMaxExperience);

    }
}
