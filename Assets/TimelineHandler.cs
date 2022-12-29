using Assets.Game.Scripts.Entities.Misc;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineHandler : MonoBehaviour
{
	[SerializeField] private Possessable _bottle;
	[SerializeField] private PlayableDirector _1A;
	[SerializeField] private PlayableDirector _2A;
	[SerializeField] private PlayableDirector _2B;

	private Vector3 _bottleStartPos;

	private void Start()
	{
		_bottleStartPos = _bottle.transform.position;
		_2A.gameObject.SetActive(false);
		_2B.gameObject.SetActive(false);
	}

	public void OnTrack1AEnd()
	{
		if (Vector3.Distance(_bottleStartPos, _bottle.transform.position) <= 1f)
		{
			_2A.gameObject.SetActive(true);
			_bottle.UnPossess();
			_2A.time = _1A.time;
		}
		else
		{
			_2B.gameObject.SetActive(true);
			_2B.time = _1A.time;
		}
	}
}
