using System;
using UnityEngine;

namespace Assets.Game.Scripts.Systems.Selection
{
	public interface ISelectable
	{
		public event Action<ISelector> OnSelect;
		public event Action<ISelector> OnDeselect;

		public void Select(ISelector selector);
		public void Deselect(ISelector selector);
	}
}
