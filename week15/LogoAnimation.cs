using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public float movementAmount = 0.5f;  // 움직이는 거리
    public float movementSpeed = 1.0f;   // 움직이는 속도

    private Vector3 initialPosition;     // 초기 위치
    private bool isMovingUp = true;      // 위로 움직이는지 여부

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
    }

    void Update()
    {
        // 로고를 위아래로 움직이는 애니메이션
        MoveLogo();
    }

    void MoveLogo()
    {
        // 로고를 위아래로 움직이는 애니메이션
        float newY = initialPosition.y + (isMovingUp ? movementAmount : -movementAmount) * Mathf.Sin(Time.time * movementSpeed);
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
