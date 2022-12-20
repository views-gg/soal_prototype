using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Systems.Selection
{
	/// <summary>
	/// Selector implementation using <see cref="RaycastHit"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class RaycastSelector<T> : Selector<T>
		where T : ISelectable
	{
		[SerializeField] protected bool AllowMultipleSelection = false;
		[SerializeField] protected float MaxDistance = 5f;
		[SerializeField] protected LayerMask Mask;

		protected void Select(Ray ray)
		{
			var result = Physics.RaycastAll(ray, MaxDistance, Mask)
				.OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
				.Select(x => x.collider.GetComponent<T>()).ToList();

			if (!AllowMultipleSelection)
			{
				if (result.Any(x => Selection.Contains(x)))
					return;
				RemoveFromSelection(Selection);
				AddToSelection(result.FirstOrDefault());
			}
			else
			{
				foreach (var selectable in Selection)
				{
					if (!result.Contains(selectable))
						RemoveFromSelection(selectable);
				}
				foreach (T selectable in result)
				{
					if (selectable == null)
						continue;
					if (!Selection.Contains(selectable))
						AddToSelection(selectable);
				}
			}
		}
	}
}