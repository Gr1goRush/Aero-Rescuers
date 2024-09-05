using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProgressView : MonoBehaviour
{
    [SerializeField] private Slider _progressLoading;
    [SerializeField] private float _stepIncreaseFirstProgress;
    [SerializeField] private float _delayFirstProgress;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    public void SetProgress(float value)
        => _progressLoading.value = value;

    public IEnumerator FirstLoadingGame()
    {
        while (_progressLoading.value != _progressLoading.maxValue)
        {
            _progressLoading.value += _stepIncreaseFirstProgress;
            yield return new WaitForSeconds(_delayFirstProgress);
        }

        SetActive(false);
    }
}
