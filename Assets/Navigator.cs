using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    private NavMeshAgent _agent;
	private NavMeshPath _path;
	private List<Action> _reachedCallback = new();
	private Animator _animator;

	private void Awake()
	{
		_animator = transform.GetChild(0).GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_path = new NavMeshPath();
	}

	private void Update()
	{
		_animator.SetFloat("Speed", Mathf.Lerp(_agent.velocity.magnitude, _agent.hasPath ? 1 : 0, .123f));

		if (Vector3.Distance(_agent.destination, transform.position) < _agent.stoppingDistance && _reachedCallback.Count > 0)
		{
			_reachedCallback[0].Invoke();
			_reachedCallback.RemoveAt(0);
		}
	}

	public void GoToPosition(Vector3 position, Action onDestinationReached = null, Action onCantReach = null)
	{
		if (_agent.CalculatePath(position, _path) && _path.status == NavMeshPathStatus.PathComplete)
		{
			_agent.SetPath(_path);
			_reachedCallback.Add(onDestinationReached);
		}
		else
			onCantReach?.Invoke();
	}

	private void OnDrawGizmos()
	{
		if (_agent)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(_agent.transform.position, _agent.destination);
		}
	}
}
