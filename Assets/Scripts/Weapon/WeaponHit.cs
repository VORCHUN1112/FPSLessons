using UnityEngine;

public class WeaponHit
{
	private Vector3 _hitPositionInWorldSpace;
	private Vector3 _hitNormal;
	private int _hitDamage;
	
	public Vector3 worldPosition { get => _hitPositionInWorldSpace; }
    public Vector3 normal { get => _hitNormal; }
	public int damage { get => _hitDamage; }

    public WeaponHit(Vector3 hitPosition, Vector3 hitNormal, int damage)
	{
		_hitPositionInWorldSpace = hitPosition;
		_hitNormal = hitNormal;
		_hitDamage = damage;
	}
}