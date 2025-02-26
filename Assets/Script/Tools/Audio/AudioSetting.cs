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
        masterSlider.value = PlayerPrefs.HasKey("Master") ? PlayerPrefs.GetFloat("Master") : 0.5f;
        MasterValueChanged(masterSlider.value);
        masterSlider.onValueChanged.AddListener(MasterValueChanged);

        sfxSlider.value = PlayerPrefs.HasKey("SFX") ? PlayerPrefs.GetFloat("SFX") : 0.5f;
        SfxValueChanged(sfxSlider.value);
        sfxSlider.onValueChanged.AddListener(SfxValueChanged);

        bgmSlider.value = PlayerPrefs.HasKey("BGM") ? PlayerPrefs.GetFloat("BGM") : 0.5f;
        BGMValueChanged(bgmSlider.value);
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
    public void CloseSetting()
    {
        gameObject.SetActive(false);
        PlayerPrefs.SetFloat("Master", masterSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
    }
}
