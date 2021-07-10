using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.SceneManagement;

//public class PlayerController : MonoBehaviour
public class PlayerController : MonoBehaviour
{
    private const float gravity = 9.81f;

    private AnimationController animationController;

    private CharacterController characterController;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float jumpSpeed = 8f;
    private float verticalSpeed = 0f;
    private float currentHealth = 100f;
    private float maxHealth = 100f;
	
    public SoundManager.SfxType walkSfx;
    public SoundManager.SfxType dmgTakenSfx;

    public PlayerWeaponsManager playerWeaponsManager;
    public float TargetHeight { get; private set; }

    private void Start()
    {
        animationController = GetComponent<AnimationController>();
        characterController = GetComponent<CharacterController>();
        SetMaxHealth(100);
        TargetHeight = characterController.center.y;
    }

    private void Update()
    {
        if (!PauseScript.IsGamePaused)
        {
            Move();
        }
    }

    private void HandleWeaponShooting()
    {
        if (Input.GetAxis("Fire") > 0)
        {
            this.playerWeaponsManager.ShootActiveWeapon();
        }
    }
    
    private void HandleWeaponSwitch()
    {
        if (Input.GetAxis("Weapon Switch") < 0 || Input.GetButtonDown("Weapon Switch Button"))
        {
            this.playerWeaponsManager.NextWeapon();
        }
        else if (Input.GetAxis("Weapon Switch") > 0 || Input.GetButtonDown("Weapon Switch Button"))
        {
            this.playerWeaponsManager.PreviousWeapon();
        }
    }

    private void HandleWeaponControls()
    {
        if (Input.GetAxis("Reload") > 0)
        {
            this.playerWeaponsManager.ReloadActiveWeapon();
        }
        this.HandleWeaponShooting();
        this.HandleWeaponSwitch();
    }
    
    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        animationController.SetFloatAnimation(PlayerAnimationType.MOVE_HOR, xMove);
        animationController.SetFloatAnimation(PlayerAnimationType.MOVE_VER, zMove);

        if(characterController.isGrounded)
        {
			if ((xMove != 0) || (zMove != 0)) {
				SoundManager.Instance.PlaySfxIfFree(walkSfx);
			}
			
			if(Input.GetButton("Jump"))
            {
                verticalSpeed = jumpSpeed;
            }
            else
            {
                verticalSpeed = 0f;
            }
        }
        else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

        Vector3 move = transform.forward * zMove + transform.right * xMove;
        characterController.Move((move * speed + gravityMove) * Time.deltaTime);
        
        this.HandleWeaponControls();
    }

    private void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
    }
    public void TakeDamage(float damage)
    {
        SoundManager.Instance.PlaySfx(dmgTakenSfx);
		currentHealth -= damage;
    }
    public float GetHealthPercentile()
    {
        return currentHealth / maxHealth * 100;
    }
}
