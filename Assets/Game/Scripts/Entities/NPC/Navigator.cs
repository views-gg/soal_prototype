using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Entities.NPC
{
	public class Navigator : MonoBehaviour
	{
		public enum Pace
		{
			CHILL = 1,
			HURRY = 2
		}

		private NavMeshAgent _agent;
		private NavMeshPath _path;
		private List<Action> _reachedCallback = new();
		private Animator _animator;
		private float _movePace;
		private float _baseSpeed;

		private void Awake()
		{
			_animator = transform.GetChild(0).GetComponent<Animator>();
			_agent = GetComponent<NavMeshAgent>();
			_path = new NavMeshPath();
			_baseSpeed = _agent.speed;
		}

		private void Update()
		{
			_animator.SetFloat("Speed", Mathf.Lerp(_animator.GetFloat("Speed"), _agent.hasPath ? _movePace : 0, .123f));

			if (Vector3.Distance(_agent.destination, transform.position) < _agent.stoppingDistance && _reachedCallback.Count > 0)
			{
				_reachedCallback[0].Invoke();
				_reachedCallback.RemoveAt(0);
			}
		}

		public void GoToPosition(Vector3 position, Pace pace, Action onDestinationReached = null, Action onCantReach = null)
		{
			Vector3 point = position;

			if (point.y > transform.position.y && point.y < transform.position.y + _agent.height)
				point.y = transform.position.y;

			if (_agent.CalculatePath(position, _path) && _path.status == NavMeshPathStatus.PathComplete)
			{
				_agent.SetPath(_path);
				_reachedCallback.Add(onDestinationReached);
				_movePace = (int)pace;
				_agent.speed = _baseSpeed * _movePace;
			}
			else
				onCantReach?.Invoke();
		}

		public void CancelPath()
		{
			_agent.destination = transform.position;
			_reachedCallback.Clear();
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
}
