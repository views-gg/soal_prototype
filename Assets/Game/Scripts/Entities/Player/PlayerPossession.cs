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

		private void Start()
		{
			Cursor.visible = false;
		}

		private void OnEnable()
		{
			Possessable.OnPossessessionStart += StartPossession;
		}
		
		private void OnDisable()
		{
			Possessable.OnPossessessionStart -= StartPossession;
		}

		private void Update()
		{
			if (_possession != null && Input.GetKeyDown(KeyCode.E))
			{
				StopPossession(_possession);
			}
		}

		private void StartPossession(Possessable possessable)
		{
			_possession = possessable;
			foreach (var monoBehaviour in _disableOnPossess)
			{
				monoBehaviour.enabled = false;
			}
		}

		private void StopPossession(Possessable possessable)
		{
			foreach (var monoBehaviour in _disableOnPossess)
			{
				monoBehaviour.enabled = true;
			}

			transform.position = possessable.transform.position;
			possessable.UnPossess();
			_possession = null;
		}
	}
}
