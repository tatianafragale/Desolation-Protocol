using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class ScPlayer : MonoBehaviour
{
    private ScEntity _entity;
    private Rigidbody _rigidbody;
    private Transform _transform;
    [SerializeField] private float sens = 1;


    private void Awake()
    {
        _entity = GetComponentInParent<ScEntity>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _transform = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        _rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("MouseX") * sens, Space.World);
        _transform.transform.Rotate(_transform.right, -1 * Input.GetAxis("MouseY") * sens, Space.World);
    }

    public void Movement(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed || CallbackContext.canceled)
        {
            _entity.movement = new Vector3(CallbackContext.ReadValue<Vector2>().x, 0f, CallbackContext.ReadValue<Vector2>().y);
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
        if (CallbackContext.performed) _entity.abilityHolder.TryAbility(Selected);
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