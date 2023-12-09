using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : ObstacleMove
{
    public GameObject stone;

    float delta = 0.01f;  // delta 변수 선언 및 초기화
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        //나의 위치에서 상대의 위치를 빼면 방향이 결정
        direction = direction.normalized * 1000; //힘 결정
        collision.gameObject.GetComponent<Rigidbody>().AddForce(direction);
    }
    float timeCount = 0;
    // Update is called once per frame
    void Update()
    {
        base.Update();
        timeCount += Time.deltaTime;
        if (timeCount > 3)
        {
            //Debug.Log("돌을 던져라");
            Instantiate(stone, transform.position, Quaternion.identity);
            timeCount = 0;
        }
        float newXPosition = transform.localPosition.x + delta;
        transform.localPosition = new Vector3(newXPosition, transform.localPosition.y,
        transform.localPosition.z);
        if (transform.localPosition.x < -9)
        {
            delta = 0.01f;
        }
        else if (transform.localPosition.x > 9)
        {
            delta = -0.01f;
        }
    }

}
