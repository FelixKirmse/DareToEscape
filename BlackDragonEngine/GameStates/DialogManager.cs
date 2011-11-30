﻿using System.Collections.Generic;
using BlackDragonEngine.Dialogue;
using BlackDragonEngine.Helpers;
using BlackDragonEngine.Providers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlackDragonEngine.GameStates
{
    public sealed class DialogManager : IUpdateableGameState, IDrawableGameState
    {
        #region Declarations

        private static Dictionary<string, DialogScript> _dialog;
        public static bool DrawMugshot = true;
        private static DialogManager _instance;
        private readonly SpriteFont _font;

        private readonly Vector2 _mugShotPosition = new Vector2(600, 500);
        private readonly Vector2 _textPosition = new Vector2(100, 500);

        private int _currentChar;
        private string _currentDialogue;

        private DialogueStates _dialogState;
        private string _displayText = "";

        #endregion

        #region Properties

        private int TextLength
        {
            get { return _dialog[_currentDialogue].Text.Length; }
        }

        private char NextChar
        {
            get { return _dialog[_currentDialogue].Text[_currentChar++]; }
        }

        private Texture2D CurrentMugShot
        {
            get { return _dialog[_currentDialogue].MugShot; }
        }

        private string CurrentName
        {
            get { return _dialog[_currentDialogue].SpeakerName; }
        }

        #region IDrawableGameState Members

        public bool DrawCondition
        {
            get { return EngineStates.DialogState == DialogueStates.Active; }
        }

        #endregion

        #region IUpdateableGameState Members

        public bool UpdateCondition
        {
            get { return EngineStates.DialogState != DialogueStates.Inactive; }
        }

        #endregion

        #endregion

        private DialogManager()
        {
            _font = FontProvider.GetFont("Mono14");
        }

        #region IDrawableGameState Members

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                _font,
                _displayText,
                ShortcutProvider.Vector2Point(_textPosition),
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                0.2f);

            if (DrawMugshot)
                spriteBatch.Draw(
                    _dialog[_currentDialogue].MugShot,
                    ShortcutProvider.Vector2Point(_mugShotPosition),
                    new Rectangle(0, 0, CurrentMugShot.Width, CurrentMugShot.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    0.2f);

            spriteBatch.DrawString(
                _font,
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

        #endregion

        #region IUpdateableGameState Members

        public bool Update()
        {
            if (_dialogState == DialogueStates.Talking)
            {
                if (_currentChar < TextLength)
                {
                    _displayText += NextChar;
                }
                else
                {
                    _dialogState = DialogueStates.Pause;
                }
            }
            else if (InputMapper.StrictAction)
            {
                _displayText = "";
                _currentChar = 0;
                _currentDialogue = _dialog[_currentDialogue].NextDialog;
                if (_currentDialogue == "STOPDIALOG")
                {
                    EngineStates.DialogState = DialogueStates.Inactive;
                }
                else
                {
                    _dialogState = DialogueStates.Talking;
                }
            }
            return true;
        }

        #endregion

        public static DialogManager GetInstance()
        {
            return _instance ?? (_instance = new DialogManager());
        }

        public void PlayDialog(Dictionary<string, DialogScript> dialogue, string startDialog)
        {
            _currentChar = 0;
            _dialog = dialogue;
            _currentDialogue = startDialog;
            EngineStates.DialogState = DialogueStates.Active;
            _dialogState = DialogueStates.Talking;
        }
    }
}