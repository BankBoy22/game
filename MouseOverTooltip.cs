using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverTooltip : MonoBehaviour
{
    public GameObject tooltipText; // ���콺�� ������ ��� ��Ÿ�� ���� �ؽ�Ʈ

    void Start()
    {
        HideTooltip(); // �ʱ⿡�� ������ ����ϴ�.
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // ���콺�� �̹��� ���� �ִ��� Ȯ��
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
