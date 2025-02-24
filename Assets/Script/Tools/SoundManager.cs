using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum BGMList
{
    Main, Vilige, Boss
}
public enum SFXList
{
    U_Attack, M_Attack, MonsterHit, Walk, Run, Jump, Size
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;

    [Header("BGM")]
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioMixerGroup bgmMixer;
    private AudioSource bgmPlayer;

    [Header("SFX")]
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] int channel;
    private AudioSource[] sfxPlayers;

    private void Awake()
    {
        bgmPlayer = new GameObject("BGMPlayer").AddComponent<AudioSource>();
        bgmPlayer.outputAudioMixerGroup = bgmMixer;

        GameObject sfxPlayer = new GameObject("SFXPlayer");

        bgmPlayer.transform.parent = transform;
        sfxPlayer.transform.parent = transform;

        sfxPlayers = new AudioSource[channel];
        for (int i = 0; i < channel; i++)
        {
            sfxPlayers[i] = sfxPlayer.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = sfxMixer;
        }
    }

    public void BGMPlaying(BGMList bgm)
    {
        bgmPlayer.clip = bgmClips[(int)bgm];
    }

    public void SFXPlaying(SFXList sfx)
    {
        AudioSource source = sfxPlayers[0];
        foreach (var item in sfxPlayers)
        {
            if (!item.isPlaying)
            {
                source = item;
                break;
            }
        }
        source.clip = sfxClips[(int)sfx];
    }
}
