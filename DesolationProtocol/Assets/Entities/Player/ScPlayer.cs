using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class ScPlayer : ScEntity
{
    [SerializeField] private Transform FocusRotator;
    [SerializeField] public float sens = 1;
    [SerializeField] private ScHud Hud;
    public Vector3 movement;
    public int totaljumps = 1;
    private int _jumps = 1;
    public float airControl = 0.8f;
    public bool landed = true;
    public float experience = 0f;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _anim = GetComponentInParent<Animator>();
    }
    protected override void Start()
    {
        base.Start();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _jumps = totaljumps;
        Hud.CountHP();
    }

    private void Update()
    {
        _rigidbody.transform.Rotate(Vector3.up, Input.GetAxis("MouseX") * sens, Space.World);
        FocusRotator.Rotate(FocusRotator.right, Mathf.Clamp(-1 * Input.GetAxis("MouseY") * sens, -90, 90), Space.World);
    }

    private void FixedUpdate()
    {

        if (movement != Vector3.zero)
        {

            if (new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude <= Stats.movementSpeed)
            {
                if (landed)
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * 100, ForceMode.Acceleration);
                }
                else
                {
                    _rigidbody.AddForce(Quaternion.LookRotation(_rigidbody.transform.forward, _rigidbody.transform.up) * movement * Stats.movementSpeed * 100 * airControl, ForceMode.Acceleration);
                }
            }
            else
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * Stats.movementSpeed;
            }
        }
        else
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x * 0.9f, _rigidbody.velocity.y, _rigidbody.velocity.z * 0.9f);
        }

        _anim.SetFloat("XAxis", movement.x, 0.1f, Time.deltaTime);
        _anim.SetFloat("ZAxis", movement.z, 0.1f, Time.deltaTime);
    }


    public void OnLand()
    {
        _anim.SetTrigger("Land");
        _jumps = totaljumps;
        landed = true;
        _anim.SetBool("Landed", landed);
    }

    public void OnAir()
    {
        _jumps = totaljumps - 1;
        landed = false;
        _anim.SetBool("Landed", landed);
    }

    public override void TakeDamage(float incomingDamage, float incomingPenLinear = 0, float incomingPenPerc = 0)
    {
        base.TakeDamage(incomingDamage, incomingPenLinear, incomingPenPerc);
        Hud.CountHP();
    }

    public override void Heal(float heal)
    {
        base.Heal(heal);
        Hud.CountHP();
    }

    //inputs
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
            if (_jumps > 0)
            {
                _anim.SetTrigger("Jump");

                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                _rigidbody.AddForce(_rigidbody.transform.up * Stats.jumpForce * 5f, ForceMode.VelocityChange);
                if (_jumps > 0)
                {
                    _jumps--;
                }
            }
        }
    }

    public void Interact(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            
        }
    }

    public void Shoot(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            GetComponent<ScWeapon>().SetShooting(true);
        }
        if (CallbackContext.canceled)
        {
            GetComponent<ScWeapon>().SetShooting(false);
        }
    }

    private void TryAbility(InputAction.CallbackContext CallbackContext, int Selected)
    {
        if (CallbackContext.performed) TryAbility(Selected);
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

    public void Pause(InputAction.CallbackContext CallbackContext)
    {
        if (CallbackContext.performed)
        {
            Hud.TogglePause();
        }
    }
}