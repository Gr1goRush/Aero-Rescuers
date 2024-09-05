using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSound;
    [SerializeField] private AudioSource _audioSourceSound2;
    [SerializeField] private AudioSource _audioSourceSound3;
    [SerializeField] private AudioSource _audioSourceSound4;
    [SerializeField] private List<Sound> _audio;

    public static AudioManager Instance { get; private set; }

    public void PlayClickButton() => PlaySound("Click", _audioSourceSound);

    public void PlayMusicMenu() => PlayMusic("Menu", true);

    public void PlayMusicGame() => PlayMusic("Game", true);

    public void PlayVictory() => PlayMusic("Victory", false);

    public void PlayGameOver() => PlayMusic("GameOver", false);

    public void PlayAirplaneFall() => PlaySound("AirplaneFall", _audioSourceSound2, true);

    public void PlayAirplaneTunnel() => PlaySound("AirplaneTunnel", _audioSourceSound2);

    public void PlayAttackCloud() => PlaySound("AttackCloud", _audioSourceSound);

    public void PlayAttackFire() => PlaySound("AttackFire", _audioSourceSound);

    public void PlayAttackStone() => PlaySound("AttackStone", _audioSourceSound);

    public void PlayAttackWave() => PlaySound("AttackWave", _audioSourceSound);

    public void PlayCaughtSkydriver() => PlaySound("CaughtSkydriver", _audioSourceSound3);

    public void PlayCoin() => PlaySound("Coin", _audioSourceSound3);

    public void PlayDown() => PlaySound("Down", _audioSourceSound4);

    public void PlayUp() => PlaySound("Up", _audioSourceSound4);

    public void PlayEnginePlane() => PlaySound("Engine", _audioSourceSound4, true);

    public void PlayPurchaseError() => PlaySound("PurchaseError", _audioSourceSound2);

    public void StopSoundPlaneFall()
        => _audioSourceSound2.Stop();

    public void StopSoundEnginePlane()
        => _audioSourceSound4.Stop();

    public void SaveVolume()
    {
        _audioSourceMusic.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic;
        _audioSourceSound.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
        _audioSourceSound2.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
        _audioSourceSound3.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
        _audioSourceSound4.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
    }

    private void PlayMusic(string name, bool isLoop)
    {
        var audio = FindAudio(name);

        if (audio != null)
        {
            _audioSourceMusic.clip = audio.Music;
            _audioSourceMusic.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic;
            _audioSourceMusic.Play();
            _audioSourceMusic.loop = isLoop;
        }
    }

    private void SetMusicSource(string name)
    {
        var audio = FindAudio(name);

        if (audio != null)
        {
            _audioSourceMusic.clip = audio.Music;
            _audioSourceMusic.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic;
            _audioSourceMusic.Play();
        }
    }

    private void PlaySound(string name, AudioSource audioSource, bool isLoop = false)
    {
        var audio = FindAudio(name);

        if (audio != null)
        {
            audioSource.clip = audio.Music;
            audioSource.volume = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound;
            audioSource.Play();
            audioSource.loop = isLoop;
        }
    }

    private Sound FindAudio(string name)
        => _audio.Find(audio => audio.Name == name);

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetMusicSource(ManagerScenes.Instance.NameActiveScene);
    }
}