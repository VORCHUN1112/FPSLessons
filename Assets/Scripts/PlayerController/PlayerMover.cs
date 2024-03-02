using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[Header("Character Controller")]
	[SerializeField] private CharacterController _controller;
	
	[Header("Player Speed")]
	[SerializeField] private float _moveSpeed = 10f;

	[Header("Jump Force")]
	[SerializeField] private float _jumpForce = 10f;
	
	[Header("Gravity Settings")]
	[SerializeField] private float _gravity = -9.81f;
	
	[Space(5f)]
	[SerializeField] private Transform _groundChecker;
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private float _groundDistance = 0.2f;

	private Vector3 _currentVelocity;

	private void Update()
	{
		bool grounded = IsGrounded();
			
		if(!grounded)
		{
			AffectByGravity();
		}
		else
		{
			_currentVelocity.y = 0f;
		}
	}
	
	public void InitialMove(Vector2 input)
	{
		Vector3 moveDirection = Vector3.zero;
		moveDirection.x = input.x;
		moveDirection.z = input.y;

		moveDirection = _moveSpeed * moveDirection * Time.deltaTime;

		Vector3 motion = transform.TransformDirection(moveDirection);
		_controller.Move(motion);
	}

	public void InitialJump()
	{
		bool grounded = IsGrounded();
		if (!grounded) return;
		
		_currentVelocity.y += _jumpForce;
		_controller.Move(_currentVelocity * Time.deltaTime);
	}
	
	private void AffectByGravity()
	{
		_currentVelocity.y += _gravity * Time.deltaTime;
		_currentVelocity.y = Mathf.Clamp(_currentVelocity.y, -200f, 200f);
		_controller.Move(_currentVelocity * Time.deltaTime);
	}
	
	private bool IsGrounded()
	{
		bool grounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundMask);
		return grounded;
	}
}