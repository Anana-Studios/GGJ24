using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private Camera m_Camera;
    private Rigidbody _rigidbody;

    private Vector2 _moveInput = Vector2.zero;
    private bool _grabInput = false, _throwInput = false;

#region Input Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    public void OnGrab(InputAction.CallbackContext context)
    {
        _grabInput = context.action.triggered;
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        _throwInput = context.action.triggered;
    }
    #endregion

    private void Awake()
    {
        m_Camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_moveInput == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg, 0);
        _rigidbody.velocity = transform.forward * speed;
    }
}
