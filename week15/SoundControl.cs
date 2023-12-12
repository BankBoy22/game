using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioSource gameSound;  // 게임 사운드용 AudioSource
    public Slider soundSlider;  // 사운드 조절용 슬라이더

    void Start()
    {
        // 슬라이더의 값이 변경될 때마다 OnSoundSliderChanged 메소드를 호출하도록 설정
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);

        // 초기 슬라이더 값을 설정
        soundSlider.value = gameSound.volume;
    }

    // 슬라이더 값이 변경될 때 호출되는 메소드
    void OnSoundSliderChanged(float value)
    {
        // 슬라이더 값에 따라 게임 사운드의 볼륨 조절
        gameSound.volume = value;
    }
}
