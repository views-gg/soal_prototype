using Assets.Game.Scripts.Systems.Selection;
using UnityEngine;

namespace Assets.Game.Scripts.Entities.Player
{
	public class PlayerSelection : RaycastSelector<ISelectable>
	{
		private UnityEngine.Camera _camera;

		private void Awake()
		{
			_camera = GetComponentInChildren<UnityEngine.Camera>();
		}

		private void Update()
		{
			Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

			Select(ray);
		}
	}
}