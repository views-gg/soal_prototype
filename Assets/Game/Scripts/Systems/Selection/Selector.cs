using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Systems.Selection
{
	/// <summary>
	/// Provides the ability of selecting. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Selector<T>: MonoBehaviour, ISelector
		where T : ISelectable
	{
		/// <summary>
		/// Selection.
		/// </summary>
		private readonly List<T> _selection = new();

		/// <summary>
		/// Gets the current _selection.
		/// </summary>
		public IReadOnlyList<T> Selection => _selection;

		/// <summary>
		/// Adds an <see cref="T"/> to _selection.
		/// </summary>
		/// <param name="selectable"></param>
		protected void AddToSelection(T selectable)
		{
			if (selectable == null)
				return;
			selectable.Select(this);
			_selection.Add(selectable);
		}

		/// <summary>
		/// Adds multiple <see cref="T"/> to _selection.
		/// </summary>
		/// <param name="selectables"></param>
		protected void AddToSelection(IEnumerable<T> selectables)
		{
			foreach (var selectable in selectables.ToList())
			{
				AddToSelection(selectables);
			}
		}

		/// <summary>
		/// Removes a <see cref="T"/> from the _selection.
		/// </summary>
		/// <param name="selectable"></param>
		protected void RemoveFromSelection(T selectable)
		{
			if (selectable == null)
				return;
			_selection.Remove(selectable);
			selectable.Deselect(this);
		}

		/// <summary>
		/// Removes multiple <see cref="T"/> from the _selection.
		/// </summary>
		/// <param name="selectable"></param>
		protected void RemoveFromSelection(IEnumerable<T> selectables)
		{
			foreach(var selectable in selectables.ToList())
				RemoveFromSelection(selectable);
		}
	}
}
