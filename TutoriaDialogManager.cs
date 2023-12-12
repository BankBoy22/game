using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutoriaDialogManager : MonoBehaviour
{
    public GameObject tutorialUI; // �ȳ� UI ������Ʈ
    public TextMeshProUGUI dialogueText; // ��ȭ �ؽ�Ʈ�� ǥ���� TextMeshProUGUI ������Ʈ
    public string[] tutorialSentences; // Ʃ�丮�� ��ȭ ���� �迭
    private int index = 0; // ��ȭ ������ ��Ÿ���� �ε���
    private bool isTyping = false; // �ؽ�Ʈ �Է� ������ ����

    void Start()
    {
        StartCoroutine(TypeSentence(tutorialSentences[index]));
    }

    // Update is called once per frame
    void Update()
    {
        // Space�ٸ� ������ �� ���� ��ȭ�� ����
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextSentence();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // �ؽ�Ʈ �Է� ������ ����
        dialogueText.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        // �� ���ھ� �ؽ�Ʈ ǥ��
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // ���� ����
        }

        isTyping = false; // �ؽ�Ʈ �Է� ����
    }

    void NextSentence()
    {
        // �ؽ�Ʈ �Է� ���� �ƴϸ� ����� �Է����� ���� ��ȭ�� ����
        if (!isTyping)
        {
            // ���� ��ȭ�� ����
            if (index < tutorialSentences.Length - 1)
            {
                index++;
                StartCoroutine(TypeSentence(tutorialSentences[index]));
            }
            else
            {
                // Ʃ�丮���� ������ ����
                EndTutorial();
            }
        }
    }

    void EndTutorial()
    {
        Debug.Log("Ʃ�丮�� ����");

        // Ʃ�丮���� ����Ǹ� �ΰ��� ���� ���� ���� �߰� ����

        // �ȳ� UI�� ��Ȱ��ȭ
        tutorialUI.SetActive(false);
    }
}
