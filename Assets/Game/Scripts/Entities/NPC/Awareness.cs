using Assets.Game.Scripts.Entities.Misc;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Entities.NPC
{
	[RequireComponent(typeof(Eyesight), typeof(Navigator))]
	public class Awareness : MonoBehaviour
	{
		[SerializeField] private Transform _hidingSpot;
		[SerializeField] private float _spottedRadius;
		[SerializeField] private float _spottedVelocity;
		[SerializeField] private bool _drawGizmos;

		private PlayableDirector[] _timelines;
		private Navigator _navigator;
		private Eyesight _eyesight;
		private bool _isHiding;

		private void Awake()
		{
			_navigator = GetComponent<Navigator>();
			_eyesight = GetComponent<Eyesight>();
		}

		private void Start()
		{
			_timelines = GameObject.FindObjectsOfType<PlayableDirector>(true);
		}

		private void OnEnable()
		{
			PossessionController.OnMove += PossessionController_OnMove;
		}

		private void OnDisable()
		{
			PossessionController.OnMove -= PossessionController_OnMove;
		}

		private void PossessionController_OnMove(Rigidbody possessed)
		{
			if (_isHiding || !_eyesight.IsInSight(possessed.transform))
				return;
			else if (Vector3.Distance(possessed.transform.position, transform.position) < _spottedRadius)
			{
				if (possessed.velocity.sqrMagnitude >= _spottedVelocity)
					Hide();
			}
		}

		private void Hide()
		{
			foreach (var item in _timelines)
				item.gameObject.SetActive(false);
			_isHiding = true;
			_navigator.CancelPath();
			_navigator.GoToPosition(_hidingSpot.position, Navigator.Pace.HURRY, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
		}

		private void OnDrawGizmos()
		{
			if (_drawGizmos)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(transform.position, _spottedRadius);
			}
		}
	}
}
