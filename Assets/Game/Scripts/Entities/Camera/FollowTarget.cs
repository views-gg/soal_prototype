using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Camera
{
	public class FollowTarget : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset;
		[SerializeField, Range(0f, 1f)] private float _smooth = 1.123f;

		private void LateUpdate()
		{
			transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _smooth);
		}
	}
}
