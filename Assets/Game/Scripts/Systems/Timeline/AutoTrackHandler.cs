using UnityEngine.Playables;

namespace Assets.Game.Scripts.Systems.Timeline
{
	public class AutoTrackHandler : TrackHandler<bool>
	{
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
			PlayNextTracks();
		}

		protected override bool CompileState() => true;
	}
}