using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListLevelsView : MonoBehaviour
{
    [SerializeField] private LevelItemView _levelItemViewPrefab;
    [SerializeField] private Transform _contentPanel;
    [SerializeField] private List<LevelEntity> _levels;
    [SerializeField] private Button _openListLevels;
    [SerializeField] private Button _closePanel;
    [SerializeField] private GameObject _parentContent;
    [Header("Открыть все уровни")]
    [SerializeField] private bool _isOpenAllLevels;

    private void Start()
    {
        if (_isOpenAllLevels)
            ViewAllItemLevels();
        else
            ViewItemLevels();

        _closePanel.onClick.AddListener(() => { _parentContent.SetActive(false); });

        _openListLevels.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            _parentContent.SetActive(true);
        });
    }

    private void ViewItemLevels()
    {
        int count = 1;
        var openedLevels = ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenedLevels;
        for (int i = 0; i < _levels.Count; i++)
        {
            var levelItem = CreateLevelItem(count, _levels[i].LevelNumber);
            levelItem.SetActiveButtonPlay(openedLevels.Contains(_levels[i].LevelNumber + ";") ? true : false);
            ++count;
        }
    }

    private void ViewAllItemLevels()
    {
        int count = 1;
        for (int i = 0; i < _levels.Count; i++)
        {
            CreateLevelItem(count, _levels[i].LevelNumber);
            ++count;
        }
    }

    private LevelItemView CreateLevelItem(int numberLevelView, int numberLevel)
    {
        var levelItem = Instantiate(_levelItemViewPrefab, _contentPanel);
        levelItem.SetViewNumberLevel(numberLevelView);
        levelItem.SetNumberLevel(numberLevel);

        return levelItem;
    }
}
