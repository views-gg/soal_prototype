using UnityEngine;

namespace Assets.Game.Scripts.Entities.Misc
{
	public class PossessionController : MonoBehaviour
	{
		[SerializeField] public Transform Camera;
		[SerializeField] private float _moveSpeed;

		private Rigidbody _rb;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			Vector3 direction = Vector3.zero;

			direction += Vector3.ProjectOnPlane(Camera.right, Vector3.up).normalized * Input.GetAxis("Horizontal");
			direction += Vector3.ProjectOnPlane(Camera.forward, Vector3.up).normalized * Input.GetAxis("Vertical");

			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
				_rb.velocity = direction * _moveSpeed;
			else
			{
				_rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, 0.123f);
			}
		}
	}
}
