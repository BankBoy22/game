using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material defaultSkybox; // 기본 스카이박스 소스
    public Material expandedSkybox; // 영역전개 시 사용할 스카이박스 소스

    void Start()
    {
        RenderSettings.skybox = defaultSkybox; // 초기에는 기본 스카이박스 사용
    }

    public void SetExpandedSkybox()
    {
        RenderSettings.skybox = expandedSkybox; // 영역전개 시 스카이박스 변경
    }

    public void SetDefaultSkybox()
    {
        RenderSettings.skybox = defaultSkybox; // 기본 스카이박스로 변경
    }
}
