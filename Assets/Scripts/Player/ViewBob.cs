using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// attach this to the Player GameObject
/// </summary>
public class ViewBob : MonoBehaviour
{
    public bool enableBobbing;
    [SerializeField]
    private Movement _movement;

    [SerializeField, Range(0, 0.1f)]
    private float _amplitude = 0;

    [SerializeField, Range(0, 30f)]
    private float _frequency = 10.0f;

    [SerializeField]
    private float _threshold = 18f; // this indicated the minimum player speed before it can start view bobbing

    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private Transform _cameraHolder;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = _camera.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enableBobbing) return;
        if (_movement.currentSpeed < _threshold) return;
        if (!_movement.IsGrounded()) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude;
        return pos;
    }

    private void CheckMotion()
    {
        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition = motion;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
