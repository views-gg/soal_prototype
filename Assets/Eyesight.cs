using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyesight : MonoBehaviour
{
    [SerializeField] private Vector3 _eyesOffset;

	public bool IsInSight(Transform target)
	{
		if (Physics.Linecast(transform.position + _eyesOffset, target.position, out RaycastHit hit))
			return hit.transform == target;
		return true;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position + _eyesOffset, .2f);
	}
}
