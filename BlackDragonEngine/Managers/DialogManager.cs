using System.Collections.Generic;
using BlackDragonEngine.Dialogue;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.Managers
{
    public static class DialogManager
    {
        #region Declarations

        private static Dictionary<string, DialogScript> dialog;

        private static readonly Vector2 textPosition = new Vector2(100, 500);
        private static readonly Vector2 mugShotPosition = new Vector2(600, 500);

        private static string currentDialogue;

        private static string displayText = "";
        private static int currentChar;

        private static DialogueStates dialogState;

        private static SpriteFont font;

        public static bool DrawMugshot = true;

        #endregion

        #region Properties

        private static int TextLength
        {
            get { return dialog[currentDialogue].Text.Length; }
        }

        private static char NextChar
        {
            get { return dialog[currentDialogue].Text[currentChar++]; }
        }

        private static Texture2D CurrentMugShot
        {
            get { return dialog[currentDialogue].MugShot; }
        }

        private static string CurrentName
        {
            get { return dialog[currentDialogue].SpeakerName; }
        }

        #endregion

        public static void Initialize()
        {
            font = FontProvider.GetFont("Mono14");
        }

        public static void PlayDialog(Dictionary<string, DialogScript> dialogue, string startDialog)
        {
            currentChar = 0;
            dialog = dialogue;
            currentDialogue = startDialog;
            EngineStates.DialogState = DialogueStates.Active;
            dialogState = DialogueStates.Talking;
        }

        public static void Update()
        {
            if (dialogState == DialogueStates.Talking)
            {
                if (currentChar < TextLength)
                {
                    displayText += NextChar;
                }
                else
                {
                    dialogState = DialogueStates.Pause;
                }
            }
            else if (InputMapper.StrictAction)
            {
                displayText = "";
                currentChar = 0;
                currentDialogue = dialog[currentDialogue].NextDialog;
                if (currentDialogue == "STOPDIALOG")
                {
                    EngineStates.DialogState = DialogueStates.Inactive;
                }
                else
                {
                    dialogState = DialogueStates.Talking;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                font,
                displayText,
                ShortcutProvider.Vector2Point(textPosition),
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.2f);

            if (DrawMugshot)
                spriteBatch.Draw(
                    dialog[currentDialogue].MugShot,
                    ShortcutProvider.Vector2Point(mugShotPosition),
                    new Rectangle(0, 0, CurrentMugShot.Width, CurrentMugShot.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0.2f);

            spriteBatch.DrawString(
                font,
                CurrentName,
                new Vector2(83, 480),
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                .2f);

            spriteBatch.Draw(
                VariableProvider.WhiteTexture,
                new Vector2(80, 480),
                new Rectangle(0, 0, 640, 120),
                Color.Black,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0.3f);
        }
    }
}