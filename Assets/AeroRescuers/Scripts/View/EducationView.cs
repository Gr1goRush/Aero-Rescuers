using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EducationView : MonoBehaviour
{
    [SerializeField] private Button _starGame;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _starGame.onClick.AddListener(OnStartGame);
    }

    private void OnStartGame()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.IsEducation = 1;
        SetActive(false);
    }
}
