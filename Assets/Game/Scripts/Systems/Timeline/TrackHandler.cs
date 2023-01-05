using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.Game.Scripts.Systems.Timeline
{
	/// <summary>
	/// Play through timelines with logical states.
	/// </summary>
	/// <typeparam name="T">The state type.</typeparam>
	public abstract class TrackHandler<T> : MonoBehaviour
	{
		[System.Serializable]
		private struct LogicOutcome
		{
			public T TriggerValue;
			public PlayableDirector[] TrackDirectors;
		}

		[SerializeField] private LogicOutcome[] _outcomes;

		protected PlayableDirector _director;

		protected virtual void Awake()
		{
			_director = GetComponent<PlayableDirector>();
		}

		/// <summary>
		/// Plays next tracks.
		/// </summary>
		protected void PlayNextTracks()
		{
			T state = CompileState();
			IEnumerable<LogicOutcome> outcomes = _outcomes.Where(x => state.Equals(x.TriggerValue));

			foreach (var outcome in outcomes)
			{
				if (outcome.TrackDirectors == null)
					continue;

				foreach (var track in outcome.TrackDirectors)
					track.gameObject.SetActive(true);
			}
		}

		/// <summary>
		/// Execute conditional logic and returns a state value.
		/// </summary>
		/// <returns></returns>
		protected abstract T CompileState();
	}
}
