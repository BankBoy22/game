using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioSource gameSound;  // ���� ����� AudioSource
    public Slider soundSlider;  // ���� ������ �����̴�

    void Start()
    {
        // �����̴��� ���� ����� ������ OnSoundSliderChanged �޼ҵ带 ȣ���ϵ��� ����
        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);

        // �ʱ� �����̴� ���� ����
        soundSlider.value = gameSound.volume;
    }

    // �����̴� ���� ����� �� ȣ��Ǵ� �޼ҵ�
    void OnSoundSliderChanged(float value)
    {
        // �����̴� ���� ���� ���� ������ ���� ����
        gameSound.volume = value;
    }
}
