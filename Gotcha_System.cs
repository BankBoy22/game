using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gotcha_System : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image imageRenderer; // �̹����� ǥ���� SpriteRenderer

    public string[] names; // �ؽ�Ʈ �迭
    public Sprite[] images; // �̹��� �迭

    private int currentIndex = 0; // ���� �迭 �ε���

    public GameObject gotchaPanel; // �̱� ����� ǥ���� �г�
    public Transform cardPanel; // ī�� ��ư�� ������ �г�
    public GameObject cardButtonPrefab; // ī�� ��ư ������
    private List<GameObject> createdCards = new List<GameObject>(); // ������ ī����� ������ ����Ʈ


    // �ڷΰ��� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnGameBackButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
    public void OnRightButtonClicked()
    {
        // �迭 �ε��� ����
        currentIndex++;

        // �迭 �ε����� �迭 ���̸� �ʰ��ϸ� ó������ ���ư�
        if (currentIndex >= names.Length)
        {
            currentIndex = 0;
        }

        // ���ο� �ؽ�Ʈ �� �̹��� ǥ��
        UpdateTextAndImage();
    }

    // ���� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnLeftButtonClicked()
    {
        // �迭 �ε��� ����
        currentIndex--;

        // �迭 �ε����� ������ �Ǹ� ���������� ���ư�
        if (currentIndex < 0)
        {
            currentIndex = names.Length - 1;
        }

        // ���ο� �ؽ�Ʈ �� �̹��� ǥ��
        UpdateTextAndImage();
    }

    // �迭�� ���� �ε����� �ش��ϴ� �ؽ�Ʈ �� �̹����� ǥ���ϴ� �Լ�
    private void UpdateTextAndImage()
    {
        // ���� �ε����� �ش��ϴ� �ؽ�Ʈ�� ǥ��
        if (nameText != null && currentIndex < names.Length)
        {
            nameText.text = names[currentIndex];
        }

        // ���� �ε����� �ش��ϴ� �̹����� ǥ��
        if (imageRenderer != null && currentIndex < images.Length)
        {
            imageRenderer.sprite = images[currentIndex];
        }
    }

    // 1ȸ �̱� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnOneDrawButtonClicked()
    {
        CreateSingleCard();
    }

    // 10ȸ �̱� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnTenDrawButtonClicked()
    {
        CreateMultipleCards(10);
    }

    // ī�带 �߾ӿ� �����ϴ� �Լ�
    private void CreateSingleCard()
    {
        DestroyPreviousCards(); // ������ ������ ī��� ����

        gotchaPanel.SetActive(true);

        GameObject cardButton = Instantiate(cardButtonPrefab, cardPanel);
        createdCards.Add(cardButton); // ����Ʈ�� �߰�
        // �߰� ���� ���� (�̹���, �̺�Ʈ ��)

        // �߾ӿ� ��ġ��Ŵ
        cardButton.transform.localPosition = Vector3.zero;
    }

    // 10���� ī�带 �����Ͽ� ����
    private void CreateMultipleCards(int count)
    {
        DestroyPreviousCards(); // ������ ������ ī��� ����

        gotchaPanel.SetActive(true);

        const float cardSpacingX = 300f; // ī�� ���� ����
        const float cardSpacingY = 400f;

        for (int i = 0; i < count; i++)
        {
            GameObject cardButton = Instantiate(cardButtonPrefab, cardPanel);
            createdCards.Add(cardButton); // ����Ʈ�� �߰�

            // ��ġ ���� (5���� 2�ٷ� ����)
            float xPos = -600f + (i % 5) * cardSpacingX;
            float yPos = 128f - ((i / 5) * cardSpacingY);
            cardButton.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // �߰� ���� ���� (�̹���, �̺�Ʈ ��)
        }
    }

    // ������ ������ ī����� �����ϴ� �Լ�
    private void DestroyPreviousCards()
    {
        foreach (var card in createdCards)
        {
            Destroy(card);
        }

        createdCards.Clear();
    }

    public void Gotcha_Cancel_button()
    {
        gotchaPanel.SetActive(false);
    }
}
