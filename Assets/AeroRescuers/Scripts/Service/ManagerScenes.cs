using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    public static ManagerScenes Instance { get; private set; }
    public string NameActiveScene => SceneManager.GetActiveScene().name;
    [SerializeField] private LoadingProgressView _loadingProgressView;
    [HideInInspector]
    public UnityEvent<float> LoadingSceneEventHandler = new UnityEvent<float>();
    [HideInInspector]
    public UnityEvent StartLoadingSceneEventHandler = new UnityEvent();
    [HideInInspector]
    public UnityEvent FinishLoadingSceneEventHandler = new UnityEvent();

    public void LoadAsyncFromCoroutine(string nameScene) => StartCoroutine(LoadAsync(nameScene));

    public void LoadAsyncFromCoroutine(string nameScene, Action action) => StartCoroutine(LoadAsync(nameScene, action));

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        StartCoroutine(_loadingProgressView.FirstLoadingGame());
        StartLoadingSceneEventHandler.AddListener(() => { _loadingProgressView.SetActive(true); });
        LoadingSceneEventHandler.AddListener((value) => { _loadingProgressView.SetProgress(value); });
        FinishLoadingSceneEventHandler.AddListener(() => { _loadingProgressView.SetActive(false); });
    }

    private IEnumerator LoadAsync(string nameScene, Action action = null)
    {
        StartLoadingSceneEventHandler?.Invoke();
        var operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);

        while (operation.progress <= 1)
        {
            var progressInPercent = (int)(operation.progress * 100);
            LoadingSceneEventHandler?.Invoke(progressInPercent);

            if (operation.progress == 1)
            {
                LoadingSceneEventHandler?.Invoke(100);
                yield return new WaitForSeconds(1);
                break;
            }

            yield return null;
        }

        if (action != null)
            action.Invoke();

        FinishLoadingSceneEventHandler?.Invoke();
    }
}
