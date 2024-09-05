using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _gamePlay;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _settings;
    [Header("Components")]
    [SerializeField] private PlayPanelView _playPanelView;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private SettingsPanelView _settingsPanelView;

    private void Start()
    {
        _gamePlay.onClick.AddListener(() =>
        {
            _playPanelView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });
        _shop.onClick.AddListener(() =>
        {
            _shopView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });
        _settings.onClick.AddListener(() =>
        {
            _settingsPanelView.SetActive(true);
            AudioManager.Instance.PlayClickButton();
        });
    }
}
