namespace Assets.Game.Scripts.Systems.Interaction
{
	public interface IInteractionActor
	{
		/// <summary>
		/// Suggests an interaction to the actor.
		/// </summary>
		/// <param name="interactable"></param>
		void SuggestInteraction(IInteractable interactable);

		/// <summary>
		/// Remove the interaction suggestion from the actor.
		/// </summary>
		/// <param name="interactable"></param>
		void UnSuggestInteraction(IInteractable interactable);
	}
}
