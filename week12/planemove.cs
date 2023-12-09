using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planemove : MonoBehaviour
{
    // 키 입력을 통해 매 프레임마다 큐브를 회전 시킴
    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Horizontal") * 30.0f * Time.deltaTime, new Vector3(0, 0, 1));
        transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Vertical") * 30.0f * Time.deltaTime, new Vector3(1, 0, 0));
    }
}
