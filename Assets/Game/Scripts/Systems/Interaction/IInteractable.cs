namespace Assets.Game.Scripts.Systems.Interaction
{
	/// <summary>
	/// Defines an object as interactable.
	/// </summary>
	public interface IInteractable
	{
		public string InteractionTitle { get; }

		/// <summary>
		/// Interacts with the object.
		/// </summary>
		void Interact(IInteractionActor actor);
	}
}
