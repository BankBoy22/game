using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gotcha_System : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image imageRenderer; // 이미지를 표시할 SpriteRenderer

    public string[] names; // 텍스트 배열
    public Sprite[] images; // 이미지 배열

    private int currentIndex = 0; // 현재 배열 인덱스

    public GameObject gotchaPanel; // 뽑기 결과를 표시할 패널
    public Transform cardPanel; // 카드 버튼이 생성될 패널
    public GameObject cardButtonPrefab; // 카드 버튼 프리팹
    private List<GameObject> createdCards = new List<GameObject>(); // 생성된 카드들을 저장할 리스트


    // 뒤로가기 버튼을 눌렀을 때 호출되는 함수
    public void OnGameBackButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
    public void OnRightButtonClicked()
    {
        // 배열 인덱스 증가
        currentIndex++;

        // 배열 인덱스가 배열 길이를 초과하면 처음으로 돌아감
        if (currentIndex >= names.Length)
        {
            currentIndex = 0;
        }

        // 새로운 텍스트 및 이미지 표시
        UpdateTextAndImage();
    }

    // 왼쪽 버튼을 눌렀을 때 호출되는 함수
    public void OnLeftButtonClicked()
    {
        // 배열 인덱스 감소
        currentIndex--;

        // 배열 인덱스가 음수가 되면 마지막으로 돌아감
        if (currentIndex < 0)
        {
            currentIndex = names.Length - 1;
        }

        // 새로운 텍스트 및 이미지 표시
        UpdateTextAndImage();
    }

    // 배열의 현재 인덱스에 해당하는 텍스트 및 이미지를 표시하는 함수
    private void UpdateTextAndImage()
    {
        // 현재 인덱스에 해당하는 텍스트를 표시
        if (nameText != null && currentIndex < names.Length)
        {
            nameText.text = names[currentIndex];
        }

        // 현재 인덱스에 해당하는 이미지를 표시
        if (imageRenderer != null && currentIndex < images.Length)
        {
            imageRenderer.sprite = images[currentIndex];
        }
    }

    // 1회 뽑기 버튼을 눌렀을 때 호출되는 함수
    public void OnOneDrawButtonClicked()
    {
        CreateSingleCard();
    }

    // 10회 뽑기 버튼을 눌렀을 때 호출되는 함수
    public void OnTenDrawButtonClicked()
    {
        CreateMultipleCards(10);
    }

    // 카드를 중앙에 생성하는 함수
    private void CreateSingleCard()
    {
        DestroyPreviousCards(); // 이전에 생성된 카드들 제거

        gotchaPanel.SetActive(true);

        GameObject cardButton = Instantiate(cardButtonPrefab, cardPanel);
        createdCards.Add(cardButton); // 리스트에 추가
        // 추가 설정 가능 (이미지, 이벤트 등)

        // 중앙에 위치시킴
        cardButton.transform.localPosition = Vector3.zero;
    }

    // 10개의 카드를 나열하여 생성
    private void CreateMultipleCards(int count)
    {
        DestroyPreviousCards(); // 이전에 생성된 카드들 제거

        gotchaPanel.SetActive(true);

        const float cardSpacingX = 300f; // 카드 간격 조절
        const float cardSpacingY = 400f;

        for (int i = 0; i < count; i++)
        {
            GameObject cardButton = Instantiate(cardButtonPrefab, cardPanel);
            createdCards.Add(cardButton); // 리스트에 추가

            // 위치 설정 (5개씩 2줄로 나열)
            float xPos = -600f + (i % 5) * cardSpacingX;
            float yPos = 128f - ((i / 5) * cardSpacingY);
            cardButton.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // 추가 설정 가능 (이미지, 이벤트 등)
        }
    }

    // 이전에 생성된 카드들을 제거하는 함수
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
