using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50.0f;  // �ʴ� ȸ�� �ӵ� (��)
    public AudioClip coinSound;  // ���� ȿ����

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    //������ �÷��̾�� �����ϸ�
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  // �÷��̾�� ��Ҵٸ�
        {
            ScoreManager.instance.AddScore(1);  // ���� �߰�

            // ȿ���� ���
            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            Destroy(gameObject);  // ���� ����
        }
    }
}
