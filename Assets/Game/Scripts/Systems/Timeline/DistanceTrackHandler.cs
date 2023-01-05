using Assets.Game.Scripts.Entities.NPC;
using Assets.Game.Scripts.Systems.Grab;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Game.Scripts.Systems.Timeline
{
	public class DistanceTrackHandler : TrackHandler<bool>
	{
		[SerializeField] private Grabbable _object;
		[SerializeField] private Navigator _navigator;
		[SerializeField] private bool _useEyeSight;
		[SerializeField] private bool _goBackAfterGrab;

		private bool _isAccessible;
		private Vector3 _initialPos;

		protected virtual void OnEnable()
		{
			_director.stopped += OnTrackEnd;
		}

		protected virtual void OnDisable()
		{
			_director.stopped -= OnTrackEnd;
		}

		private void OnTrackEnd(PlayableDirector director)
		{
			if (_navigator == null)
				return;
			_initialPos = _navigator.transform.position;

			if (_useEyeSight)
			{
				var eyes = _navigator.GetComponent<Eyesight>();
				if (!eyes.IsInSight(_object.transform))
				{
					_isAccessible = false;
					PlayNextTracks();
					return;
				}
			}

			// Go to the object
			_navigator.GoToPosition(_object.transform.position, Navigator.Pace.CHILL,
				onDestinationReached: () =>
				{
					_navigator.GetComponent<Grabber>()?.Grab(_object);

					if (_goBackAfterGrab)
					{
						_navigator.GoToPosition(_initialPos, Navigator.Pace.CHILL, () =>
						{
							_isAccessible = true;
							PlayNextTracks();
						});
					}
					else
					{
						_isAccessible = true;
						PlayNextTracks();
					}
				}, onCantReach: () =>
				{
					_isAccessible = false;
					PlayNextTracks();
				});
		}

		protected override bool CompileState() => _isAccessible;
	}
}
