using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeController : MonoBehaviour
{
    public void SetWindowedMode()
    {
        // 창 모드로 변경
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void SetFullscreenMode()
    {
        // 전체 화면 모드로 변경
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
}
