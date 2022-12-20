using Assets.Game.Scripts.Systems.Selection;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Misc
{
	[RequireComponent(typeof(Outline))]
	public class OutlineOnSelected : MonoBehaviour
	{
		private ISelectable _selectable;
		private Outline _outline;

		private void Awake()
		{
			_outline = GetComponent<Outline>();
			_selectable = GetComponent<ISelectable>();
			_outline.enabled = false;
		}

		private void OnEnable()
		{
			// Subscribing here since we wanna enable/disable this very component.
			_selectable.OnSelect += Outline;
			_selectable.OnDeselect += UnOutline;
		}
		private void OnDisable()
		{
			_selectable.OnSelect -= Outline;
			_selectable.OnDeselect -= UnOutline;
		}

		private void UnOutline(ISelector obj)
		{
			_outline.enabled = false;
		}

		private void Outline(ISelector obj)
		{
			_outline.enabled = true;
		}
	}
}