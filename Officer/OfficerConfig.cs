using PhoenixPoint.Modding;

namespace Officer
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class OfficerConfig : ModConfig
	{
		/// Only public fields are serialized.
		/// Supported types for in-game UI are:
				
		[ConfigField(text: "Officer Sophia", description: "Sets Sophia as an Officer on New Game start.")]
		public bool OfficerSophia = true;

		[ConfigField(text: "Make Officers a Dominant Class", description: "Hides the Primary class icon for Officers in tactical.")]
		public bool DominantClass = false;
	}
}
