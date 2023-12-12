using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeController : MonoBehaviour
{
    public void SetWindowedMode()
    {
        // â ���� ����
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void SetFullscreenMode()
    {
        // ��ü ȭ�� ���� ����
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
