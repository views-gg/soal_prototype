using System;
using Assets.Game.Scripts.Entities.Misc;
using CMF;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Camera
{
	public class PossessionCamera : MonoBehaviour
	{
		private UnityEngine.Camera _camera;
		private SmoothPosition _follow;

		private void Awake()
		{
			_camera = GetComponentInChildren<UnityEngine.Camera>();
			_follow = GetComponent<SmoothPosition>();
		}

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

		private void StopPossession(Possessable possessable)
		{
			_camera.enabled = false;
		}

		private void StartPossession(Possessable possessable)
		{
			_camera.enabled = true;
			_follow.target = possessable.transform;

			var controller = possessable.GetComponent<PossessionController>();
			
			if (controller != null)
				controller.Camera = _camera.transform;
		}
	}
}
