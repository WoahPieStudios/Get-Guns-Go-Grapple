using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField]
    private Vector2 _mouseSensitivity;
    [SerializeField]
    private float _maxAngleAlongYAxis = 80f;
    [SerializeField]
    private Transform _player;

    private float _xRotation = 0f;
    private Vector2 _mouseInput;


    private void OnEnable()
    {
        InputManager.onMouseMovement += GetMouseDelta;
    }

    private void OnDisable()
    {
        InputManager.onMouseMovement -= GetMouseDelta;
    }

    void Update()
    {
        _xRotation -= _mouseInput.y * _mouseSensitivity.y * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, -_maxAngleAlongYAxis, _maxAngleAlongYAxis);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _player.Rotate(Vector3.up * _mouseInput.x * _mouseSensitivity.x * Time.deltaTime);
    }

    private void GetMouseDelta(Vector2 delta)
    {
        _mouseInput = delta;
    }
}
