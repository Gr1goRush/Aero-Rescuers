using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanelView : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private Button _infinityGame;
    [SerializeField] private Button _storyGame;
    [SerializeField] private LevelsView _levelsView;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _back.onClick.AddListener(() => { SetActive(false); });
        _infinityGame.onClick.AddListener(() =>
        {
            OnEntryStoryGame(TypeGame.Infinity);
            ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
        });
        _storyGame.onClick.AddListener(() =>
        {
            OnEntryStoryGame(TypeGame.Story);
            _levelsView.SetActive(true);
        });
    }

    private void OnEntryStoryGame(TypeGame typeGame)
    {
        AudioManager.Instance.PlayClickButton();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame = (int)typeGame;
    }
}
