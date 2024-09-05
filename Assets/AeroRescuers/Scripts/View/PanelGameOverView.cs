using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGameOverView : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _quit;
    [SerializeField] private Text _money;
    [SerializeField] private Text _countSkydriver;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _restart.onClick.AddListener(OnRestart);
        _quit.onClick.AddListener(OnQuit);

        Initialize();
    }

    private void Initialize()
    {
        _money.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.GameMoney.ToString();
        _countSkydriver.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver.ToString();
    }

    private void OnRestart()
    {
        AudioManager.Instance.PlayClickButton();
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Game");
    }

    private void OnQuit()
    {
        AudioManager.Instance.PlayClickButton();
        ManagerScenes.Instance.LoadAsyncFromCoroutine("Menu");
    }
}
