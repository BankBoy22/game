using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public float movementAmount = 0.5f;  // �����̴� �Ÿ�
    public float movementSpeed = 1.0f;   // �����̴� �ӵ�

    private Vector3 initialPosition;     // �ʱ� ��ġ
    private bool isMovingUp = true;      // ���� �����̴��� ����

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    void Update()
    {
        // �ΰ� ���Ʒ��� �����̴� �ִϸ��̼�
        MoveLogo();
    }

    void MoveLogo()
    {
        // �ΰ� ���Ʒ��� �����̴� �ִϸ��̼�
        float newY = initialPosition.y + (isMovingUp ? movementAmount : -movementAmount) * Mathf.Sin(Time.time * movementSpeed);
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
