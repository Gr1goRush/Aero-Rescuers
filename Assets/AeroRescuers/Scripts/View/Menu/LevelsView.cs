using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsView : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private Transform _listLevelsLovation1;
    [SerializeField] private Transform _listLevelsLovation2;
    [SerializeField] private Transform _listLevelsLovation3;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        AudioManager.Instance.PlayClickButton();
        _back.onClick.AddListener(() =>
        {
            if (_listLevelsLovation1.gameObject.activeSelf == false && _listLevelsLovation2.gameObject.activeSelf == false && _listLevelsLovation3.gameObject.activeSelf == false)
                SetActive(false);
            else
            {
                _listLevelsLovation1.gameObject.SetActive(false);
                _listLevelsLovation2.gameObject.SetActive(false);
                _listLevelsLovation3.gameObject.SetActive(false);
            }
        });
    }
}
