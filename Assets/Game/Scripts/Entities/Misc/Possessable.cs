using System;
using Assets.Game.Scripts.Systems.Interaction;
using Assets.Game.Scripts.Systems.Selection;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Misc
{
	/// <summary>
	/// Represents an object which can be possessed.
	/// </summary>
	public class Possessable : MonoBehaviour, IInteractable, ISelectable
	{
		public string InteractionTitle => $"Possess {gameObject.name}";

		public event Action<ISelector> OnSelect;
		public event Action<ISelector> OnDeselect;

		public static event Action<Possessable> OnPossessessionStart;
		public static event Action<Possessable> OnPossessionStopped;

		private bool _isPossesed;

		private PossessionController _controller;
		private Grabbable _grabbable;

		private bool _canBePossessed => !(_grabbable && _grabbable.IsGrabbed) && !_isPossesed;

		private void Awake()
		{
			_controller = GetComponent<PossessionController>();
			_controller.enabled = false;
			_grabbable = GetComponent<Grabbable>();
		}

		private void OnEnable()
		{
			if (_grabbable) 
			{
				_grabbable.OnGrabbed += OnGrabbed;
				_grabbable.OnReleased += OnReleased;
			}
		}

		private void OnDisable()
		{
			if (_grabbable)
			{
				_grabbable.OnGrabbed -= OnGrabbed;
				_grabbable.OnReleased -= OnReleased;
			}
		}

		private void OnReleased()
		{
		}

		private void OnGrabbed()
		{
			if (_isPossesed)
				UnPossess();
		}

		/// <summary>
		/// Possess the object
		/// </summary>
		/// <param name="actor"></param>
		public void Interact(IInteractionActor actor)
		{
			if (!_canBePossessed)
				return;
			_controller.enabled = true;
			_isPossesed = true;
			actor.UnSuggestInteraction(this);
			Deselect(null);
			OnPossessessionStart?.Invoke(this);
		}

		public void UnPossess()
		{
			if (!_isPossesed)
				return;
			_isPossesed = false;
			OnPossessionStopped?.Invoke(this);
			_controller.enabled = false;
		}

		public void Select(ISelector selector)
		{
			if (!_canBePossessed)
				return;

			var actor = selector as MonoBehaviour;
			var interaction = actor?.GetComponent<IInteractionActor>();

			OnSelect?.Invoke(selector);
			if (interaction != null)
				interaction.SuggestInteraction(this);
		}

		public void Deselect(ISelector selector)
		{
			var actor = selector as MonoBehaviour;
			var interaction = actor?.GetComponent<IInteractionActor>();

			OnDeselect?.Invoke(selector);
			if (interaction != null)
				interaction.UnSuggestInteraction(this);
		}
	}
}