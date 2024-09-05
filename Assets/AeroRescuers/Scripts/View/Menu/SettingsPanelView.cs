using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelView : MonoBehaviour
{
    [SerializeField] private Button _back;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _back.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(false);
        });
    }
}
