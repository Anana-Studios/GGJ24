using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f, throwForce = 3.0f;
    [SerializeField]
    private Transform grabPivot;

    [HideInInspector]
    public GrabbableObj _objInReach;

    private bool _grabbingObj = false;
    private Camera m_Camera;
    private Rigidbody _rigidbody;

    private Image _playerPointer;

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
        _playerPointer = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        MovePlayer();
        GrabObj();
        ThrowObj();
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
    private void GrabObj()
    {
        if(_grabInput && _objInReach != null && !_objInReach.isGrabbed)
        {
            _objInReach.ObjGrabbed(grabPivot);
            _grabbingObj = true;
        }
    }
    private void ThrowObj()
    {
        if(_throwInput && _grabbingObj)
        {
            _grabbingObj = false;
            _objInReach.ResetObj();
            _objInReach.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * throwForce, ForceMode.Impulse);
        }
    }


    public void TeleportPlayer(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void SetPointerColour(Color plyrColour)
    {
        _playerPointer.color = plyrColour;
    }
}
