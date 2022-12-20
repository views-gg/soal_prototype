using Assets.Game.Scripts.Entities.Misc;
using UnityEngine;

namespace Assets.Game.Scripts.Entities
{
	public class DisableOnPosess : MonoBehaviour
	{
		[SerializeField] private MonoBehaviour[] _disableOnPossess;

		private void OnEnable()
		{
			Possessable.OnPossessessionStart += StartPossession;
			Possessable.OnPossessionStopped += StopPossession;
		}

		private void OnDisable()
		{
			Possessable.OnPossessessionStart -= StartPossession;
			Possessable.OnPossessionStopped -= StopPossession;
		}

		private void StartPossession(Possessable possessable)
		{
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
		}
	}
}
