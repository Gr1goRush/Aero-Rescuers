using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchVibrationView : MonoBehaviour
{
    [SerializeField] private Button _on;
    [SerializeField] private Button _off;
    [SerializeField] private Image _imageOn;
    [SerializeField] private Image _imageOff;

    private void Start()
    {
        UpdateUI();

        _on.onClick.AddListener(() => { ChangeValue(1); });
        _off.onClick.AddListener(() => { ChangeValue(0); });
    }

    private void UpdateUI()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn == 0)
        {
            _imageOn.color = new Color(1, 1, 1, 0.5f);
            _imageOff.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _imageOn.color = new Color(1, 1, 1, 1);
            _imageOff.color = new Color(1, 1, 1, 0.5f);
        }
    }

    private void ChangeValue(int value)
    {
        AudioManager.Instance.PlayClickButton();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn = value;
        UpdateUI();
    }
}
