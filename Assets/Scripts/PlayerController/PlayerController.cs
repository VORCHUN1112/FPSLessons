using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private PlayerInputs _inputs;
	private PlayerInputs.PlayerActions _player;
	private PlayerLook _playerLook;
	private PlayerMover _playerMover;
	//private WeaponChanger _weaponChanger;

	[SerializeField] private WeaponHolder _holder;
	
	private void Awake()
	{
		
		_inputs = new();
		_player = _inputs.Player;
		
		_playerLook = GetComponent<PlayerLook>();
		_playerMover = GetComponent<PlayerMover>();
		//_weaponChanger = GetComponent<WeaponChanger>();
	}
	
	private void Start()
	{
		//_weaponChanger.SetWeaponHolder(_holder);
		//_weaponChanger.SetFirstWeapon();
		
		LockCursor();
	}
	
	private void OnEnable()
	{
		_inputs.Enable();

		_player.Aim.performed += context => _holder.currentWeapon.BeginAim();
		_player.Aim.canceled += context => _holder.currentWeapon.EndAim();

		_player.Attack.performed += context => _holder.currentWeapon.PerformAttack();
		_player.Attack.canceled += context => _holder.currentWeapon.StopAttack();

		//_player.PreviousWeapon.performed += context => _weaponChanger.SetPreviousWeapon();
		//_player.NextWeapon.performed += context => _weaponChanger.SetNextWeapon();
	}
	
	private void OnDisable()
	{
		_inputs.Disable();

		_player.Aim.performed -= context => _holder.currentWeapon.BeginAim();
		_player.Aim.canceled -= context => _holder.currentWeapon.EndAim();

		_player.Attack.performed -= context => _holder.currentWeapon.PerformAttack();
		_player.Attack.canceled -= context => _holder.currentWeapon.StopAttack();

		//_player.PreviousWeapon.performed -= context => _weaponChanger.SetPreviousWeapon();
		//_player.NextWeapon.performed -= context => _weaponChanger.SetNextWeapon();
	}
	
	private void Update()
	{
		Vector2 move = _player.Move.ReadValue<Vector2>();
		Vector2 look = _player.Look.ReadValue<Vector2>();

		_playerLook.InitialLook(look);
		_playerMover.InitialMove(move);
	}
	
	private void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
}