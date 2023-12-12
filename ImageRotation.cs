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

    public Sprite[] imageArray; // �̹��� �迭 �߰�
    private Button button; // Button ������Ʈ �߰�

    private void Start()
    {
        initialRotationSpeed = rotationSpeed;
        button = GetComponent<Button>(); // ��ũ��Ʈ�� ������ ������Ʈ�� Button ������Ʈ�� ������
    }

    private void Update()
    {
        if (isRotating)
        {
            if (currentRotationTime < rotationDuration)
            {
                // Y���� �������� ȸ��
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                currentRotationTime += Time.deltaTime;
                rotationSpeed = Mathf.Lerp(initialRotationSpeed, 0f, currentRotationTime / rotationDuration);
            }
            else
            {
                // ȸ���� ������ �̹����� �������� �����Ͽ� ����
                ChangeImage();
                isRotating = false;
            }
        }
    }

    // �̹����� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnImageClick()
    {
        // �ʱ�ȭ
        currentRotationTime = 0f;
        rotationSpeed = initialRotationSpeed;
        isRotating = true;
    }

    // �̹����� �����ϴ� �Լ�
    private void ChangeImage()
    {
        if (button != null && imageArray.Length > 0)
        {
            // ���� �̹��� ����
            Sprite randomSprite = imageArray[Random.Range(0, imageArray.Length)];

            // �̹��� ����
            button.image.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("Button component not found on the script-attached object or imageArray is empty.");
        }

        Debug.Log("�̹��� ����");
    }
}
