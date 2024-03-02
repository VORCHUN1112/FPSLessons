using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
	[SerializeField] private GameObject _hitEffectPrefab;
	
	public void TakeHit(WeaponHit hit)
	{
		ApplyHitEffect(hit);
		
		if (TryGetComponent(out IDamageable damageable))
		{
			damageable.ApplyDamage(hit.damage);
		}
	}
	
	private void ApplyHitEffect(WeaponHit hit)
	{
		if (!_hitEffectPrefab) return;

		Vector3 hitPosition = hit.worldPosition;
		Quaternion hitRotation = Quaternion.LookRotation(hit.normal);

		GameObject hitEffect = Instantiate(_hitEffectPrefab, hitPosition, hitRotation);
		hitEffect.transform.SetParent(transform, true);
	}
}