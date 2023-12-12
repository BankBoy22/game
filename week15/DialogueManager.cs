using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;      // ��ȭ �ؽ�Ʈ�� ǥ���� TextMeshProUGUI ������Ʈ
    public TextMeshProUGUI instructionText;   // "space�ٸ� ��������" �ؽ�Ʈ�� ǥ���� TextMeshProUGUI ������Ʈ
    public string[] sentences;                // ��ȭ ���� �迭
    private int index = 0;                    // ��ȭ ������ ��Ÿ���� �ε���
    public VideoPlayer videoPlayer;           // VideoPlayer ������Ʈ ����
    public AudioSource backgroundMusic;      // ��� ������ ����� AudioSource
    public GameObject inputNamePanel;         // �̸��� �Է¹޴� �г�
    public TMP_InputField nameInputField;     // �̸��� �Է¹޴� InputField
    public string NextScene;
    private string playerName; //�÷��̾��� �̸�

    private bool isTyping = false;            // �ؽ�Ʈ �Է� ������ ����
    private bool isfirst = true;              // ù��ȭ���� ����

    void Start()
    {
        // ��� ���� ���
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        StartCoroutine(TypeSentence(sentences[index]));
        StartCoroutine(BlinkInstructionText());
    }

    void Update()
    {
        // �ؽ�Ʈ �Է� ���� �ƴϸ� ����� �Է����� ���� ��ȭ�� ����
        if (!isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;   // �ؽ�Ʈ �Է� ������ ����
        dialogueText.text = "";  // �ؽ�Ʈ �ʱ�ȭ

        // �� ���ھ� �ؽ�Ʈ ǥ��
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // ���� ����
        }

        isTyping = false;  // �ؽ�Ʈ �Է� ����

        // ��ȭ�� ��� ������ InputName �г� Ȱ��ȭ
        if (index == sentences.Length - 1 && isfirst == true)
        {
            ActivateInputNamePanel();
            isfirst = false;
        }
        
    }

    IEnumerator TypeSentence2(string sentence)
    {
        isTyping = true;   // �ؽ�Ʈ �Է� ������ ����
        dialogueText.text = "";  // �ؽ�Ʈ �ʱ�ȭ

        // �� ���ھ� �ؽ�Ʈ ǥ��
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // ���� ����
        }

        isTyping = false;  // �ؽ�Ʈ �Է� ����
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
        // ���� ��ȭ�� ����
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence(sentences[index]));
        }
        else
        {
            // ��ȭ�� ������ ���ϴ� ���� ���� (��: ���� ������ �̵�)
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        Debug.Log("��ȭ ����");
        // �߰����� ������ �����ϰų� ���� ������ �߰��� �� ����

        // ��ȭ ���� �� ���� ���
        PlayVideo();
    }

    void PlayVideo()
    {
        // ���� ���
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    void ActivateInputNamePanel()
    {
        // InputName �г��� ��Ȱ��ȭ�Ǿ� ���� ���� Ȱ��ȭ
        if (inputNamePanel != null && !inputNamePanel.activeSelf)
        {
            inputNamePanel.SetActive(true);
        }
    }

    public void OnSubmitName()
    {
        // �̸� ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
        playerName = nameInputField.text;
        Debug.Log("Player Name: " + playerName);

        // PlayerPrefs�� ����Ͽ� �÷��̾��� �̸� ����
        PlayerPrefs.SetString("PlayerName", playerName);

        // Player ���� �ʱ�ȭ
        InitializePlayerStats();

        // ��ȭ ���� �߰� �� ����
        AddAndRunAdditionalDialogue();

        // InputName �г� ��Ȱ��ȭ
        if (inputNamePanel != null)
        {
            inputNamePanel.SetActive(false);
        }
    }

    void AddAndRunAdditionalDialogue()
    {
        // ���⿡ �߰� ��ȭ ������ �߰��ϰ� �����ϴ� ������ �ۼ�
        // ���� ���, "�� ȯ���մϴ�! �� �׽�Ʈ�� �����غ��ô�" ���� ��ȭ ������ �߰��ϰ� ����
        sentences = new string[]
        {
            PlayerPrefs.GetString("PlayerName") + "�� ȯ���մϴ�!",
            "��, �׽�Ʈ�� �����غ��ô�!"
        };
        // �߰��� ��ȭ ���� ����
        index = 0;
        StartCoroutine(TypeSentence2(sentences[index]));


        // 2�� �ڿ� ���� ������ �̵�
        StartCoroutine(DelayedSceneChange(4f));
    }

    IEnumerator DelayedSceneChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        // ���⿡ ���� ������ �̵��ϴ� ������ �߰�
        SceneManager.LoadScene(NextScene);
    }

    void InitializePlayerStats()
    {
        // �÷��̾��� �ʱ� ����, Money, ����ġ ���� �� ����
        int initialLevel = 1;
        int initialMoney = 0;
        int initialcash = 0;
        int initialExperience = 0;
        int initialMaxExperience = 100;

        // �÷��̾� ���� ����
        PlayerPrefs.SetInt("PlayerLevel", initialLevel);
        PlayerPrefs.SetInt("PlayerMoney", initialMoney);
        PlayerPrefs.SetInt("PlayerCash", initialcash);
        PlayerPrefs.SetInt("PlayerExperience", initialExperience);
        PlayerPrefs.SetInt("PlayerMaxExperience", initialMaxExperience);

    }
}
