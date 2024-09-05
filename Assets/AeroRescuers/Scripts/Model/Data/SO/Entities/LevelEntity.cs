using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "LevelEntity", menuName = "ScriptableObject/LevelEntity")]
public class LevelEntity : Entity
{
    private LevelController _levelController;

    [Header("Components")]
    [SerializeField] private List<FrameMapEntity> _framesMapEntityPrefab;
    [SerializeField] private FrameMapEntity _airstripPrefab;
    [SerializeField] private List<SkydiverEntity> _skydiverEntities;
    [SerializeField] private MoneyEnetity _moneyEnetityPrefab;
    [SerializeField] private AirTunnelEntity _airTunnelEntityPrefab;
    [SerializeField] private List<CloudEntity> _cloudEntityPrefab;
    [Header("Other settings")]
    [SerializeField] private int _number;
    [SerializeField] private int _startFramesMap;
    [Header("Кол-во парашютистов на уровне")]
    [SerializeField] private int _countMaxSkydrivers;

    public override ViewEntity View { get; }
    public int CountMaxSkydrivers => _countMaxSkydrivers;
    public int LevelNumber => _number;
    public ReactiveCommand OnWinLevelCommand;

    public override void Initialize(Transform parent)
    {
        OnWinLevelCommand = new();
        _levelController = new LevelController();

        var airstripFrameMap = Instantiate(_airstripPrefab, parent);
        airstripFrameMap.Initialize(parent);

        for (int i = 0; i < (_startFramesMap == -1 ? _framesMapEntityPrefab.Count : _startFramesMap); i++)
        {
            var frameMap = Instantiate(_framesMapEntityPrefab[_startFramesMap == -1 ? i : 0], parent);
            frameMap.Initialize(parent);
            FrameMapView frameMapView = (FrameMapView)frameMap.View;

            if (i % 2 == 0)
                frameMap.Controller.RotateBack();
            
            frameMap.Controller.UpdatePosition((i + 1) * new Vector3(frameMapView.Width, 0, 0));
            frameMapView.OffsetFrameBack.Subscribe((frameMapEntity) =>
            {
                var frameMapView = (FrameMapView)frameMapEntity.View;
                _levelController.ChangeLastFrameMap(frameMapView);

                if ((frameMap.View is FrameMapTransitionView) == false)
                {
                    InitializeSkydriver(frameMapView);
                    InitializeMoney(frameMapView);
                    InitializeAirTunnel(frameMapView);
                    InitializeCloud(frameMapView);
                    frameMap.InitializeEnemy();
                }
            });

            if ((frameMap.View is FrameMapTransitionView) == false)
            {
                InitializeSkydriver(frameMapView);
                InitializeMoney(frameMapView);
                InitializeAirTunnel(frameMapView);
                InitializeCloud(frameMapView);
            }

            if (i == (_startFramesMap == -1 ? _framesMapEntityPrefab.Count - 1 : _startFramesMap - 1))
                _levelController.SetLastFrameMapEntity(frameMap.View.transform.localPosition);

            _levelController.AddFrameMapEntity(frameMap);
        }
    }

    public override void FixedUpdate()
    {
        if (TryWinLevel())
            OnWinLevelCommand.Execute();

        _levelController.FixedUpdateFramesMap();
    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(OnWinLevelCommand);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(OnWinLevelCommand);
    }

    private bool TryWinLevel()
        => _countMaxSkydrivers != 0 && _countMaxSkydrivers == ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver;

    private void InitializeSkydriver(FrameMapView frameMap)
    {
        var countSkudrivers = Random.Range(1, 3);

        for (int i = 0; i < countSkudrivers; i++)
        {
            var indexSkydrive = Random.Range(0, _skydiverEntities.Count);
            var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);

            var skydriver = _skydiverEntities[indexSkydrive];
            skydriver.Initialize(frameMap.transform);
            skydriver.View.transform.localPosition = new Vector3(x, skydriver.View.transform.localPosition.y, 0);
        }
    }

    private void InitializeMoney(FrameMapView frameMap)
    {
        var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);
        var y = Random.Range(0, frameMap.Height / 4);
        var moneyEntity = Instantiate(_moneyEnetityPrefab);
        moneyEntity.Initialize(frameMap.transform);
        moneyEntity.View.transform.localPosition = new Vector3(x, y, 0);
    }

    private void InitializeAirTunnel(FrameMapView frameMap)
    {
        var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);
        var y = Random.Range(-frameMap.Height / 4, frameMap.Height / 2);
        var airTunnelEntity = Instantiate(_airTunnelEntityPrefab);
        airTunnelEntity.Initialize(frameMap.transform);
        airTunnelEntity.View.transform.localPosition = new Vector3(x, y, 0);
    }

    private void InitializeCloud(FrameMapView frameMap)
    {
        var countClouds = ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame == 0 ? Random.Range(0, 3) : Random.Range(1, 3);

        for (int i = 0; i < countClouds; i++)
        {
            var indexCloud = Random.Range(0, _cloudEntityPrefab.Count);
            var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);

            var cloud = _cloudEntityPrefab[indexCloud];
            cloud.Initialize(frameMap.transform);
            cloud.View.transform.localPosition = new Vector3(x, cloud.View.transform.localPosition.y, 0);
        }
    }
}
