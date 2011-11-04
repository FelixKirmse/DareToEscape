using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Dialogue
{
    /// <summary>
    /// The Structure for a dialog
    /// </summary>
    public abstract class DialogScript
    {
        /// <summary>
        /// The Dialog's Text to output
        /// </summary>
        public string Text { get; protected set; }

        /// <summary>
        /// The Mugshot that should be displayed
        /// </summary>
        public Texture2D MugShot { get; protected set; }

        /// <summary>
        /// The dialog that plays after this one, use "STOPDIALOG" if there is none
        /// </summary>
        public string NextDialog { get; protected set; }

        /// <summary>
        /// Name of the object speaking
        /// </summary>
        public string SpeakerName { get; protected set; }
    }
}