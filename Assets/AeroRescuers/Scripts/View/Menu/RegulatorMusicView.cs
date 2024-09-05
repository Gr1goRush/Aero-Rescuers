using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RegulatorMusicView : MonoBehaviour
{
    [SerializeField] private Slider _regulatorMusic;

    private void Start()
    {
        _regulatorMusic.value = ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic;
        _regulatorMusic.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.PlayClickButton();
            ContainerSaveerPlayerPrefs.Instance.SaveerData.VolumeMusic = value;
            AudioManager.Instance.SaveVolume();
        });
    }
}
