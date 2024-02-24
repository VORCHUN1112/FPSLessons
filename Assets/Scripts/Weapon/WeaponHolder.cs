using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
	private IWeapon _currentWeapon;
	public RangedWeapon currentWeapon { get => _currentWeapon as RangedWeapon; }
	
	private void Awake()
	{
		_currentWeapon = GetComponentInChildren<IWeapon>();
	}
	
	public void PlaceGun(IWeapon placingWeapon)
	{
		RangedWeapon weapon = _currentWeapon as RangedWeapon;
		if (weapon is RangedWeapon)
		{
			Destroy(weapon.gameObject);
		}
		
		IWeapon newWeapon = Instantiate(placingWeapon as RangedWeapon, transform, false);
		_currentWeapon = newWeapon;
	}
}