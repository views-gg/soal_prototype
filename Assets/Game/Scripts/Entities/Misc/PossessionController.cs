using System;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Misc
{
	public class PossessionController : MonoBehaviour
	{
		[SerializeField] public Transform Camera;
		[SerializeField] private float _moveSpeed = 6f;
		[SerializeField] private float _jumpForce = 10f;
		[SerializeField] private float _jumpCooldown = 1f;

		private Rigidbody _rb;
		private float _lastJump;

		public static event Action<Rigidbody> OnMove;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) && Time.time - _lastJump >= _jumpCooldown)
				Jump();
		}

		private void Jump()
		{
			OnMove?.Invoke(_rb);
			_lastJump = Time.time;
			_rb.AddExplosionForce(_jumpForce, transform.position - Vector3.up, 2f);
			_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
		}

		private void ApplyGravity()
		{
			_rb.AddForce(Vector3.down);
		}

		private void FixedUpdate()
		{
			if (Camera == null)
				return;

			Vector3 direction = Vector3.zero;

			direction += Vector3.ProjectOnPlane(Camera.right, Vector3.up).normalized * Input.GetAxis("Horizontal");
			direction += Vector3.ProjectOnPlane(Camera.forward, Vector3.up).normalized * Input.GetAxis("Vertical");

			Debug.Log($"mag {_rb.velocity.magnitude} sqrt: {_rb.velocity.sqrMagnitude}");
			ApplyGravity();
			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				OnMove?.Invoke(_rb);
				_rb.velocity = new Vector3(direction.x * _moveSpeed, _rb.velocity.y, direction.z * _moveSpeed);
			}
			else
			{
				_rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(0, _rb.velocity.y, 0), 0.123f);
			}
		}
	}
}
