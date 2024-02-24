using UnityEngine;

public class PlayerLook : MonoBehaviour
{
	[Header("Player Head With Camera")]
	[SerializeField] private GameObject _playerHead;
	
	[Header("Sensetivity")]
	[SerializeField] private float _verticalSensetivity = 10f;
	[SerializeField] private float _horizontalSensetivity = 10f;

	[Header("LookAngle")]
	[SerializeField] private float _maxVerticalAngle = 90f;
	[SerializeField] private float _minVerticalAngle = -90f;
	
	private float _verticalRotation;
	private float _horizontalRotation;
	
	private void Start()
	{
		_verticalRotation = _playerHead.transform.rotation.x;
		_horizontalRotation = _playerHead.transform.rotation.y;
	}
	
	public void InitialLook(Vector2 input)
	{
		float mouseX = input.x;
		float mouseY = input.y;

		LookVectical(mouseY);
		LookHorizontal(mouseX);
	}
	
	private void LookVectical(float yInput)
	{
		_verticalRotation -= _verticalSensetivity * yInput * Time.deltaTime;
		_verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

		Quaternion rotation = Quaternion.Euler(Vector3.right * _verticalRotation);
		_playerHead.transform.localRotation = rotation;
	}
	
	private void LookHorizontal(float xInput)
	{
		_horizontalRotation -= _horizontalSensetivity * xInput * Time.deltaTime;

		Quaternion rotation = Quaternion.Euler(Vector3.down * _horizontalRotation);
		transform.localRotation = rotation;
	}
}