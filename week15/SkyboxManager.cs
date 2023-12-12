using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material defaultSkybox; // �⺻ ��ī�̹ڽ� �ҽ�
    public Material expandedSkybox; // �������� �� ����� ��ī�̹ڽ� �ҽ�

    void Start()
    {
        RenderSettings.skybox = defaultSkybox; // �ʱ⿡�� �⺻ ��ī�̹ڽ� ���
    }

    public void SetExpandedSkybox()
    {
        RenderSettings.skybox = expandedSkybox; // �������� �� ��ī�̹ڽ� ����
    }

    public void SetDefaultSkybox()
    {
        RenderSettings.skybox = defaultSkybox; // �⺻ ��ī�̹ڽ��� ����
    }
}
