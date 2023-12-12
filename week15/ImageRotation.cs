using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRotation : MonoBehaviour
{
    private bool isRotating = false;
    private float rotationDuration = 5f;
    private float rotationSpeed = 1000f;

    private float currentRotationTime = 0f;
    private float initialRotationSpeed;

    public Sprite[] imageArray; // 이미지 배열 추가
    private Button button; // Button 컴포넌트 추가

    private void Start()
    {
        initialRotationSpeed = rotationSpeed;
        button = GetComponent<Button>(); // 스크립트가 부착된 오브젝트의 Button 컴포넌트를 가져옴
    }

    private void Update()
    {
        if (isRotating)
        {
            if (currentRotationTime < rotationDuration)
            {
                // Y축을 기준으로 회전
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                currentRotationTime += Time.deltaTime;
                rotationSpeed = Mathf.Lerp(initialRotationSpeed, 0f, currentRotationTime / rotationDuration);
            }
            else
            {
                // 회전이 끝나면 이미지를 랜덤으로 선택하여 변경
                ChangeImage();
                isRotating = false;
            }
        }
    }

    // 이미지를 클릭했을 때 호출되는 함수
    public void OnImageClick()
    {
        // 초기화
        currentRotationTime = 0f;
        rotationSpeed = initialRotationSpeed;
        isRotating = true;
    }

    // 이미지를 변경하는 함수
    private void ChangeImage()
    {
        if (button != null && imageArray.Length > 0)
        {
            // 랜덤 이미지 선택
            Sprite randomSprite = imageArray[Random.Range(0, imageArray.Length)];

            // 이미지 변경
            button.image.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("Button component not found on the script-attached object or imageArray is empty.");
        }

        Debug.Log("이미지 변경");
    }
}
