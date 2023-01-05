using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Systems.Grab
{
	public class Grabber : MonoBehaviour
	{
		[SerializeField] private Transform[] _grabHandlers;

		private Dictionary<Transform, Grabbable> _grabDict = new();

		private void Start()
		{
			foreach (var item in _grabHandlers)
			{
				_grabDict.TryAdd(item, null);
			}
		}

		public void Grab(Grabbable grabbable)
		{
			var available = _grabDict.FirstOrDefault(x => x.Value == null);

			if (available.Key == null)
				return;
			_grabDict[available.Key] = grabbable;
			grabbable.GetGrabbed(available.Key);
		}

		public void Release(Grabbable grabbable)
		{
			var slot = _grabDict.FirstOrDefault(x => x.Value == grabbable);

			if (slot.Value != grabbable)
				return;
			_grabDict[slot.Key] = null;
			grabbable.GetReleased();
		}
	}
}
