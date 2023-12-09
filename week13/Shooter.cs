using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : ObstacleMove
{
    public GameObject stone;

    float delta = 0.01f;  // delta ���� ���� �� �ʱ�ȭ
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        //���� ��ġ���� ����� ��ġ�� ���� ������ ����
        direction = direction.normalized * 1000; //�� ����
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
            //Debug.Log("���� ������");
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
