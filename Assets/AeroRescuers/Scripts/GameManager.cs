using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameManager : MonoBehaviour
{
    private bool _isPause;

    [Header("Components")]
    [SerializeField] private List<Entity> _entities;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CounterManager _counterManager;
    [SerializeField] private PanelWinView _panelWinView;
    [SerializeField] private PanelGameOverView _panelGameOverView;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private PanelPauseView _panelPauseView;
    [SerializeField] private EducationView _educationView;
    [Header("UI")]
    [SerializeField] private Button _pause;
    [SerializeField] private Button _startGame;
    [Header("Other settings")]
    [SerializeField] private Transform _mainCanvas;

    private void Awake()
    {
        AudioManager.Instance.PlayMusicGame();
    }

    private void Start()
    {
        CheckProgressEducation();
        _entities.Add(_levelManager.CurrentLevel);
        _entities.Add(_levelManager.CurrentPlane);

        foreach (var entity in _entities)
        {
            if (entity is PlaneEntity || entity is FramesMapEntity)
            {
                entity.Initialize(_mainCanvas);
                entity.AddObjectDisposable();
            }
        }

        FramesMapEntity framesMap = GetEntity<FramesMapEntity>();
        FramesMapView viewFramesMap = (FramesMapView)framesMap.View;

        foreach (var entity in _entities)
        {
            if (entity is PlaneEntity == false && entity is FramesMapEntity == false)
            {
                entity.Initialize(viewFramesMap.Transform);
                entity.AddObjectDisposable();
            }
        }

        PlaneEntity plane = GetEntity<PlaneEntity>();
        LevelEntity level = GetEntity<LevelEntity>();

        _counterManager.SetMaxSaveSkydrivers(level.CountMaxSkydrivers);
        _counterManager.ViewCountSkydrivers(0);

        _healthManager.GameOverCommand.Subscribe(_ => { OnGameOver(); plane.Controller.SetPlaneStatic(); });
        level.OnWinLevelCommand.Subscribe(_ => { OnWinLevel(); plane.Controller.SetPlaneStatic(); });
        PlaneView viewPlane = (PlaneView)plane.View;
        viewPlane.SaveSkydriverCommand.Subscribe(_ => { _counterManager.IncreaseCountSkydrivers(); });
        viewPlane.GetMoneyCommand.Subscribe(_ => { _counterManager.IncreaseCountMoney(); });
        viewPlane.GetDamageCommand.Subscribe(damage =>
        {
            if (_isPause)
                return;
            
            _healthManager.Damage(damage); Vibration();
        });
        viewPlane.OnDisappearedWithMapCommand.Subscribe(_ => { _healthManager.Die(); });
        _inputManager.OnMoveCommand.Subscribe(_ => { plane.Controller.Up(); });
        _inputManager.OnStopCommand.Subscribe(_ => { plane.Controller.Down(); });

        _pause.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            OnPauseGame();
            plane.Controller.SetPlaneStatic();
        });
        _startGame.onClick.AddListener(() =>
        {
            _startGame.gameObject.SetActive(false);
            _isPause = false;
            plane.Controller.StartFly();
        });
        _panelPauseView.OnContinueGameCommand.Subscribe(_ =>
        {
            OnContinueGame();
            plane.Controller.SetPlaneDynamic();
        });

        _isPause = true;
        plane.Controller.SetPlaneStatic();
    }

    private void Vibration()
    {
        if(ContainerSaveerPlayerPrefs.Instance.SaveerData.IsVibrationOn == 1)
            Handheld.Vibrate();
    }

    private void CheckProgressEducation()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.IsEducation == 0)
            _educationView.SetActive(true);
    }

    private void OnPauseGame()
    {
        _panelPauseView.SetActive(true);
        _isPause = true;
    }

    private void OnContinueGame()
    {
        _panelPauseView.SetActive(false);
        _isPause = false;
    }

    private void OnWinLevel()
    {
        AudioManager.Instance.PlayVictory();
        _panelWinView.SetActive(true);
        _isPause = true;
    }

    private void OnGameOver()
    {
        AudioManager.Instance.StopSoundPlaneFall();
        AudioManager.Instance.PlayGameOver();

        if(_panelGameOverView != null)
            _panelGameOverView.SetActive(true);

        _isPause = true;
    }

    private T GetEntity<T>() where T : Entity
    {
        return (T)_entities.Find(entity => entity is T);
    }

    private void FixedUpdate()
    {
        if (_isPause)
            return;

        foreach (var entity in _entities)
            entity.FixedUpdate();
    }

    private void OnDisable()
    {
        foreach (var entity in _entities)
            entity.Dispose();
    }
}
