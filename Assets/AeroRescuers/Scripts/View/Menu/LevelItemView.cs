using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemView : MonoBehaviour
{
    [SerializeField] private int _numberLevel;
    [SerializeField] private Button _play;
    [SerializeField] private Text _numberLevelView;
    [SerializeField] private int _viewNumberLevel;

    public void SetNumberLevel(int number)
        => _numberLevel = number;

    public void SetViewNumberLevel(int numberLevel)
        => _viewNumberLevel = numberLevel;

    public void SetActiveButtonPlay(bool value)
    {
        _play.enabled = value;
        var image = _play.gameObject.GetComponent<Image>();
        image.color = new Color(1, 1, 1, value ? 1 : 0.4f);
    }

    private void Start()
    {
        _numberLevelView.text = _viewNumberLevel.ToString();
        _play.onClick.AddListener(OnPlay);
    }

    private void OnPlay()
    {
        AudioManager.Instance.PlayClickButton();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel = _numberLevel;
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }
}
