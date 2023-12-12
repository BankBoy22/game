using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50.0f;  // 초당 회전 속도 (도)
    public AudioClip coinSound;  // 코인 효과음

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

    //코인이 플레이어와 접촉하면
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))  // 플레이어에게 닿았다면
        {
            ScoreManager.instance.AddScore(1);  // 점수 추가

            // 효과음 재생
            if (audioSource != null && coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            Destroy(gameObject);  // 코인 제거
        }
    }
}
