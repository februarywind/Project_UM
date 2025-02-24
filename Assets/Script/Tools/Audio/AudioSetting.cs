using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(MasterValueChanged);
        sfxSlider.onValueChanged.AddListener(SfxValueChanged);
        bgmSlider.onValueChanged.AddListener(BGMValueChanged);
    }

    private void MasterValueChanged(float value)
    {
        // 인간의 귀가 소리를 로그방식으로? 인식해서 자연스럽게 들리게 하기 위해 Log10을 사용한다고 한다.
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    private void SfxValueChanged(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    private void BGMValueChanged(float value)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }
}
