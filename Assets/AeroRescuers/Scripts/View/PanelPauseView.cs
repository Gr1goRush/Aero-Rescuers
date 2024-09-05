using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class PanelPauseView : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _changeStateSound;
    [SerializeField] private Button _quit;
    [SerializeField] private Text _titleStateVolume;

    public ReactiveCommand OnContinueGameCommand = new();

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        UpdateTitleStateVolume();
        ManagerUniRx.AddObjectDisposable(OnContinueGameCommand);
        _continue.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnContinueGameCommand.Execute();
        });
        _changeStateSound.onClick.AddListener(OnChangeStateSound);
        _quit.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
        });
    }

    private void OnChangeStateSound()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound != 0 || ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic != 0)
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound = 0;
            ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic = 0;
        }
        else
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound = 1;
            ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic = 1;
        }

        UpdateTitleStateVolume();
        AudioManager.Instance.SaveVolume();
        AudioManager.Instance.PlayClickButton();
    }

    private void UpdateTitleStateVolume()
    {
        _titleStateVolume.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeSound != 0 || ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic != 0 ? "Sound: ON" : "Sound: OFF";
    }    

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnContinueGameCommand);
    }
}
