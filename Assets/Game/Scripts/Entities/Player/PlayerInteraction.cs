using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Systems.Interaction;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Player
{
	public class PlayerInteraction : MonoBehaviour, IInteractionActor
	{
		[SerializeField] private KeyCode _interactionKey;
		
		private List<IInteractable> _interactables = new();
		
		private void Update()
		{
			if (Input.GetKeyDown(_interactionKey))
				_interactables.ToList().ForEach(x => x.Interact(this));
		}

		public void SuggestInteraction(IInteractable interactable)
		{
			if (!_interactables.Contains(interactable))
				_interactables.Add(interactable);
		}

		public void UnSuggestInteraction(IInteractable interactable)
		{
			_interactables.Remove(interactable);
		}
	}
}
