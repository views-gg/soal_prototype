using System;
using Assets.Game.Scripts.Entities.Misc;
using CMF;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Player
{
	public class PlayerPossession : MonoBehaviour
	{
		[SerializeField] private MonoBehaviour[] _disableOnPossess;

		private Possessable _possession;

		private float _cooldown = 0.5f;
		private float _lastPossess;

		private void Start()
		{
			Cursor.visible = false;
		}

		private void OnEnable()
		{
			Possessable.OnPossessessionStart += StartPossession;
			Possessable.OnPossessionStopped += OnPossessionStopped;
		}

		private void OnDisable()
		{
			Possessable.OnPossessessionStart -= StartPossession;
			Possessable.OnPossessionStopped -= OnPossessionStopped;
		}

		private void Update()
		{
			if (_possession != null && Input.GetKeyDown(KeyCode.E) && Time.time - _lastPossess >= _cooldown)
			{
				_possession.UnPossess();
			}
		}

		private void StartPossession(Possessable possessable)
		{
			_lastPossess = Time.time;
			_possession = possessable;
			foreach (var monoBehaviour in _disableOnPossess)
			{
				monoBehaviour.enabled = false;
			}
		}

		private void OnPossessionStopped(Possessable obj)
		{
			foreach (var monoBehaviour in _disableOnPossess)
			{
				monoBehaviour.enabled = true;
			}

			transform.position = obj.transform.position;
			_possession = null;
		}
	}
}
