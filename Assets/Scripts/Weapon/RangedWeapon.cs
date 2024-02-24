using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon, IAimable
{
	[Header("Effectors")]
	[SerializeField] private Animator _animator;
	[SerializeField] private ParticleSystem _muzzleEffect;
	[SerializeField] private AudioSource _shotAudioSource;
	//private ShakeCameraOnWeaponAttack _cameraShaker;

	[Header("Shot")]
	[SerializeField] private LayerMask _layerMask;
	[SerializeField, Min(0)] private float _distance = Mathf.Infinity;
	[SerializeField, Min(0)] private float _bulletsInShot = 1;
	[SerializeField, Min(0)] private int _damage = 10;

	[Header("Shot Spread")]
	[SerializeField] private bool _useSpread;
	[SerializeField, Min(0)] private float _spreadFactor = 1f;
	[SerializeField, Min(0)] private float _aimingSpreadFactor = 0.01f;
	[SerializeField] private float _currentSpreadFactor;

	[Header("FireRate")]
	[SerializeField] private bool _fullAuto = false;
	[SerializeField] private float _fireRate = 15f;
	private float _nextTimeToFire = 0f;
	private bool _isSpray = false;
	
	
	private void Awake()
	{
		//_cameraShaker = Camera.main.GetComponent<ShakeCameraOnWeaponAttack>();
	}
	
	private void Start()
	{
		_currentSpreadFactor = _spreadFactor;	
	}
	
	private void Update()
	{
		if (!_fullAuto) return;
		
		if (_isSpray)
		{
			PerformAttack();
		}	
	}
	
	public void PerformAttack()
	{
		_isSpray = true;
		
		if (Time.time < _nextTimeToFire) return;
		_nextTimeToFire = Time.time + 1f / _fireRate;
		
		for (int i = 0; i < _bulletsInShot; i++)
		{
			_muzzleEffect?.Play();
			_shotAudioSource?.Play();
			//IWeaponAttackReaction reaction = _cameraShaker;
			//reaction.ReactOnBasicAttack();
			PerformRaycast();
		}
	}
	
	public void StopAttack()
	{
		_isSpray = false;
	}

	public void BeginAim()
	{
		bool isAim = true;
		_animator.SetBool("Aiming", isAim);

		_currentSpreadFactor = _aimingSpreadFactor;
	}
	
	public void EndAim()
	{
		bool isAim = false;
		_animator.SetBool("Aiming", isAim);

		_currentSpreadFactor = _spreadFactor;
	}
	
	private void PerformRaycast()
	{
		Vector3 origin = Camera.main.transform.position;
		Vector3 direction = Camera.main.transform.forward;

		if (_useSpread)
		{
			direction += CalculateSpread();
		}
		
		Ray ray = new Ray(origin, direction);

		if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
		{
			Collider hitCollider = hitInfo.collider;
			IHitable Hitable = hitCollider.GetComponent<IHitable>();
			
			if (Hitable != null)
			{
				WeaponHit hit = new WeaponHit(hitInfo.point, hitInfo.normal, _damage);
				Hitable.TakeHit(hit);
			}
		}
	}
	
	private Vector3 CalculateSpread()
	{
		Vector3 spread = new()
		{
			x = Random.Range(-_currentSpreadFactor, _currentSpreadFactor),
			y = Random.Range(-_currentSpreadFactor, _currentSpreadFactor),
			z = Random.Range(-_currentSpreadFactor, _currentSpreadFactor),
		};

		return spread;
	}

}
