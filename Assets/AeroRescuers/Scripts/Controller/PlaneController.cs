using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController
{
    private Plane _plane;
    private PlaneView _planeView;
    private Vector3 _tragetPosition;

    public PlaneController(Plane plane, PlaneView planeView)
    {
        _plane = plane;
        _planeView = planeView;
    }

    public void Up()
        => _plane.Up();

    public void Down()
        => _plane.Down();

    public void RotationPlane(int speedRotation, float sensitivityRotation)
    {
        var angleRotation = Quaternion.Lerp(_planeView.transform.rotation, Quaternion.Euler(0, 0, _plane.CurrentHeight * speedRotation), sensitivityRotation);
        _planeView.Rotation(angleRotation);
    }

    public void SetPlaneStatic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Static);
        AudioManager.Instance.StopSoundEnginePlane();
        AudioManager.Instance.StopSoundPlaneFall();
    }

    public void SetPlaneDynamic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Dynamic);
        AudioManager.Instance.PlayEnginePlane();
    }

    public void LossHeight(float speedIncreaseGravity, float sensitivityChangeVelocity, float sensitivityChangePosition)
    {
        if (_plane.CurrentHeight > 0)
            _plane.IsFall = false;
        else if (_plane.CurrentHeight <= 0 && _plane.IsFall == false)
        {
            _plane.IsFall = true;
            AudioManager.Instance.PlayAirplaneFall();
        }

        _plane.LossHeight(speedIncreaseGravity);
        _planeView.UpdatePosition(new Vector2(_planeView.transform.position.x, _plane.CurrentHeight), sensitivityChangePosition);
        _planeView.UpdateForce(_plane.ForceGravity, sensitivityChangeVelocity);
    }

    public async void StartFly()
    {
        AudioManager.Instance.PlayEnginePlane();
        _tragetPosition = new Vector3(-193, 28, 0);
        _planeView.PlayAnimationUp();
        await Task.Delay(2000);
        _plane.IsFly = true;
        SetPlaneDynamic();
    }

    public void Flying()
    {
        var target = Vector3.MoveTowards(_planeView.transform.localPosition, _tragetPosition, 0.7f);
        _planeView.UpdateLocalPosition(target);
    }

    public void RecoveryHeight(float speedIncreaseGravity)
        => _plane.RecoveryHeight(speedIncreaseGravity);
}
