using com.ootii.Graphics;
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

		[ConfigField(text: "Secondary Effect for Resilience Training", description: "Select the Secondary effect for Resilience Training. The primary effect is a 1AP discount for Recover, which is active regardless of which option is picked. \n\nNone: No additional effect. \nDamage_Resistance: 50% Damage Resistance until the next turn. \nRestore_Parts: Restores all disabled Bodyparts on the next turn. \nBoth: All effects.")]
		public ResilienceEffects SecondaryResilienceEffect = ResilienceEffects.Restore_Parts;

		public enum ResilienceEffects
		{
			None, Damage_Resistance, Restore_Parts, Both
		}
	}
}
