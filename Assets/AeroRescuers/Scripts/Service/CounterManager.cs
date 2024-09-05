using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour
{
    [Header("Кол-во спасенных парашютистов")]
    [SerializeField] private Text _viewCountSavedSkydrivers;
    [SerializeField] private Slider _countSavedSkydrivers;
    [Header("Кол-во собранных монет")]
    [SerializeField] private Text _viewCountMoney;
    [SerializeField] private Text _titleCounter;

    public void SetMaxSaveSkydrivers(int count)
        => _countSavedSkydrivers.maxValue = count;

    public void IncreaseCountSkydrivers()
    {
        var countSavedSkydrivers = int.Parse(_viewCountSavedSkydrivers.text.Split('/')[0]) + 1;
        ViewCountSkydrivers(countSavedSkydrivers);
    }

    public void IncreaseCountMoney()
    {
        var countMoney = int.Parse(_viewCountMoney.text) + 1;
        _viewCountMoney.text = countMoney.ToString();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney = countMoney;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.Money += 1;
    }

    public void ViewCountSkydrivers(int countSavedSkydrivers)
    {
        _viewCountSavedSkydrivers.text = _countSavedSkydrivers.maxValue != 0 ? countSavedSkydrivers.ToString() + "/" + _countSavedSkydrivers.maxValue.ToString() : countSavedSkydrivers.ToString();

        if (_countSavedSkydrivers.maxValue != 0)
            _countSavedSkydrivers.value = countSavedSkydrivers;

        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver = countSavedSkydrivers;
    }

    private void Start()
    {
        _viewCountMoney.text = "0";
        ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney = 0;
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver = 0;
        _titleCounter.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame == 0 ? "Score:" : "Goal:";
    }
}
