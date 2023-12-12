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

        // ���� ���� ������ ������ ���� ����
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
        // �浹�� ������Ʈ�� �±װ� "Player"�� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� ��ġ�� Ư�� ������Ʈ�� ��ġ�� ���� (��: (0, 0, 0))
            collision.gameObject.transform.position = new Vector3(-26.57f, 23.9f, -20.33f);
        }
    }
}
