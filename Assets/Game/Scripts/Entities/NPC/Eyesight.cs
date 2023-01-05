using UnityEngine;

namespace Assets.Game.Scripts.Entities.NPC
{
	public class Eyesight : MonoBehaviour
	{
		[SerializeField] private Vector3 _eyesOffset;
		[SerializeField, Range(0, 180)] private float _maxViewAngle = 180;
		[SerializeField] private bool _drawGizmos;

		private Vector3 _eyePos => transform.position + _eyesOffset;

		public bool IsInSight(Transform target)
		{
			Vector3 los = (target.position - _eyePos).normalized;

			if (Mathf.Abs(Vector3.SignedAngle(los, transform.forward, Vector3.up)) > _maxViewAngle)
				return false;
			else if (Physics.Linecast(transform.position + _eyesOffset, target.position, out RaycastHit hit))
				return hit.transform == target;
			return true;
		}

		private void OnDrawGizmos()
		{
			if (!_drawGizmos)
				return;

			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position + _eyesOffset, .05f);
			Gizmos.DrawRay(transform.position + _eyesOffset, transform.forward);

			Gizmos.color = Color.yellow;
			for (int i = 0; i < _maxViewAngle * 2; i++)
			{
				Vector3 los = Quaternion.AngleAxis(i - (_maxViewAngle), Vector3.up) * transform.forward;
				Gizmos.DrawRay(transform.position + _eyesOffset, los);
			}
		}
	}
}
