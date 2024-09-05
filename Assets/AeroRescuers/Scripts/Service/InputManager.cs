using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour
{
    private InputMap _inputMap;

    public ReactiveCommand OnMoveCommand = new();
    public ReactiveCommand OnStopCommand = new();

    private void Awake()
    {
        _inputMap = new InputMap();
        _inputMap.Map.Move.performed += (context) => { OnMoveCommand.Execute(); };
        _inputMap.Map.Move.canceled += (context) => { OnStopCommand.Execute(); };
    }

    private void Start()
    {
        ManagerUniRx.AddObjectDisposable(OnMoveCommand);
        ManagerUniRx.AddObjectDisposable(OnStopCommand);
    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }

    private void OnDisable()
    {
        _inputMap.Disable();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnMoveCommand);
        ManagerUniRx.Dispose(OnStopCommand);
    }
}
