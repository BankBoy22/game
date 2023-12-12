using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverTooltip : MonoBehaviour
{
    public GameObject tooltipText; // 마우스를 가져다 대면 나타날 툴팁 텍스트

    void Start()
    {
        HideTooltip(); // 초기에는 툴팁을 숨깁니다.
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // 마우스가 이미지 위에 있는지 확인
        if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), mousePosition))
        {
            ShowTooltip();
            
        }
        else
        {
            HideTooltip();
        }
    }

    void ShowTooltip()
    {
        tooltipText.gameObject.SetActive(true);
    }

    void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);
    }
}
