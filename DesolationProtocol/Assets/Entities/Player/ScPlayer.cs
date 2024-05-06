using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class ScPlayer : MonoBehaviour
{
    private ScEntity _entity;
    private Rigidbody _rigidbody;
    private Transform _transform;
    [SerializeField] private float sens = 1;
    private Animator _anim;
    public Vector3 movement;

    private void Awake()
    {
        _entity = GetComponentInParent<ScEntity>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _transform = GetComponentInChildren<Transform>();
        _anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        _rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("MouseX") * sens, Space.World);
        _transform.Rotate(_transform.right, Mathf.Clamp(-1 * Input.GetAxis("MouseY") * sens, -90, 90), Space.World);

    }

    private void FixedUpdate()
    {

        if (movement != Vector3.zero)
        {

            if (new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude <= _entity.Stats.movementSpeed)
            {
                if (_entity.landed)
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * _entity.Stats.movementSpeed * 4, ForceMode.Acceleration);
                }
                else
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * _entity.Stats.movementSpeed * 4 * _entity.airControl, ForceMode.Acceleration);
                }
            }
            else
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _entity.Stats.movementSpeed;
            }
        }
        else
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x * 0.9f, _rigidbody.velocity.y, _rigidbody.velocity.z * 0.9f);
        }

        _anim.SetFloat("XAxis", movement.x, 0.1f, Time.deltaTime);
        _anim.SetFloat("ZAxis", movement.z, 0.1f, Time.deltaTime);
    }

    public void Test(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            
        }
    }

    public void Movement(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed || CallbackContext.canceled)
        {
            movement = new Vector3(CallbackContext.ReadValue<Vector2>().x, 0f, CallbackContext.ReadValue<Vector2>().y);
        }
    }

    public void Jump(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            _entity.Jump();
        }
    }

    public void Interact(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            
        }
    }

    private void TryAbility(InputAction.CallbackContext CallbackContext, int Selected)
    {
        if (CallbackContext.performed) _entity.TryAbility(Selected);
    }

    public void TryAbility0(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 0);
    }

    public void TryAbility1(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 1);
    }

    public void TryAbility2(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 2);
    }

    public void TryAbility3(InputAction.CallbackContext CallbackContext)
    {
        TryAbility(CallbackContext, 3);
    }
}