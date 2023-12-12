using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;
    public float changeDirectionInterval = 2f;

    private bool isMovingRight = true;
    private float timeSinceLastDirectionChange = 0f;

    void Update()
    {
        MoveObstacle();
    }

    void MoveObstacle()
    {
        float moveDirection = isMovingRight ? 1f : -1f;
        transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);

        // 방향 변경 간격이 지나면 방향 변경
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            ChangeDirection();
            timeSinceLastDirectionChange = 0f;
        }
    }

    void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"인 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 위치를 특정 오브젝트의 위치로 설정 (예: (0, 0, 0))
            collision.gameObject.transform.position = new Vector3(-26.57f, 23.9f, -20.33f);
        }
    }
}
