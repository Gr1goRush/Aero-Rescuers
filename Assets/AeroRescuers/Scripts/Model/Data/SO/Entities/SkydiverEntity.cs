using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkydiverEntity", menuName = "ScriptableObject/SkydiverEntity")]
public class SkydiverEntity : Entity
{
    private SkydiverView _skydiverView;
    private SkydiverController _skydiverController;

    [SerializeField] private SkydiverView _skydiverViewPrefab;

    public override ViewEntity View => _skydiverView;
    public SkydiverController Controller => _skydiverController;

    public override void Initialize(Transform parent)
    {
        _skydiverView = Instantiate(_skydiverViewPrefab, parent);

        _skydiverController = new SkydiverController(_skydiverView);
    }

    public override void FixedUpdate()
    {

    }

    public override void AddObjectDisposable()
    {

    }

    public override void Dispose()
    {

    }
}
