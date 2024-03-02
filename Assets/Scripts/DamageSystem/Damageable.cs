using UnityEngine;

[System.Serializable]
public class Damageable : MonoBehaviour, IDamageable
{
	[SerializeField, Min(0)] private int _health = 1;
	
	public void ApplyDamage(int damage)
	{
		if (damage <= 0) return;

		_health -= damage;

		DeathCheck();
	}

	
	private void DeathCheck()
	{
		if (_health <=0)
		{
			Die();
		}
	}
	
	private void Die()
	{
		_health = 0;
		Debug.Log($"{name} погиб!");
	}
}