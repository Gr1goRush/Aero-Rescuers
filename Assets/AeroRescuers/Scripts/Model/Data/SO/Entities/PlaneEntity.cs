using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "PlaneEntity", menuName = "ScriptableObject/PlaneEntity")]
public class PlaneEntity : Entity
{
    private Plane _plane;
    private PlaneView _planeView;
    private PlaneController _planeController;

    [SerializeField] private PlaneView _planeViewPrefab;
    [SerializeField] private SkinPlane _skinPlane;
    [SerializeField] private float _forceGravity;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _speedIncreaseHeight;
    [SerializeField] private float _speedIncreaseHeightMax;
    [SerializeField] private float _sensitivityChangeVelocity;
    [SerializeField] private float _sensitivityChangePosition;
    [SerializeField] private int _speedRotation;
    [SerializeField] private float _sensitivityRotation;

    public override ViewEntity View => (PlaneView)_planeView;
    public PlaneController Controller => _planeController;
    public SkinPlane SkinPlane => _skinPlane;

    public override void Initialize(Transform parent)
    {
        _plane = new Plane(Vector2.down * _forceGravity, _maxHeight);
        _planeView = Instantiate(_planeViewPrefab, parent);
        _planeView.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(400, 480, 0));
        _planeView.transform.localPosition = new Vector3(_planeView.transform.localPosition.x, _planeView.transform.localPosition.y, 0);

       _planeController = new PlaneController(_plane, _planeView);
        _planeView.OnCollisionAirTunnelCommand.Subscribe(_ => { _planeController.RecoveryHeight(_speedIncreaseHeightMax); });
    }

    public override void FixedUpdate()
    {
        if (_plane.IsFly)
        {
            _planeController.LossHeight(_speedIncreaseHeight, _sensitivityChangeVelocity, _sensitivityChangePosition);
            _planeController.RotationPlane(_speedRotation, _sensitivityRotation);
        }
        else
            _planeController.Flying();
    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_planeView.SaveSkydriverCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.GetMoneyCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.GetDamageCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.OnDisappearedWithMapCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.OnCollisionAirTunnelCommand);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(_planeView.SaveSkydriverCommand);
        ManagerUniRx.Dispose(_planeView.GetMoneyCommand);
        ManagerUniRx.Dispose(_planeView.GetDamageCommand);
        ManagerUniRx.Dispose(_planeView.OnDisappearedWithMapCommand);
        ManagerUniRx.Dispose(_planeView.OnCollisionAirTunnelCommand);
    }
}
