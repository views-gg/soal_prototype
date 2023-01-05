using System;
using UnityEngine;

namespace Assets.Game.Scripts.Systems.Grab
{
	public class Grabbable : MonoBehaviour
	{
		[SerializeField] private Vector3 _inHandPosition;
		[SerializeField] private Vector3 _inHandRotation;

		public bool IsGrabbed { get; private set; }

		public event Action OnGrabbed;
		public event Action OnReleased;
		private Quaternion _initialRotation;

		private void Update()
		{
			if (IsGrabbed)
			{
				transform.localPosition = _inHandPosition;
				transform.localRotation = Quaternion.Euler(_inHandRotation);
			}
		}

		public void GetGrabbed(Transform parent)
		{
			if (IsGrabbed)
				return;
			IsGrabbed = true;
			transform.parent = parent;
			_initialRotation = transform.rotation;
			OnGrabbed?.Invoke();
		}

		public void GetReleased()
		{
			if (!IsGrabbed)
				return;
			IsGrabbed = false;
			transform.parent = null;
			transform.rotation = _initialRotation;
			OnReleased?.Invoke();
		}
	}
}
