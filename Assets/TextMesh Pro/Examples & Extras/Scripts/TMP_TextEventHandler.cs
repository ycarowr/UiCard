using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TMPro
{
    public class TMP_TextEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Camera m_Camera;
        private Canvas m_Canvas;
        private int m_lastCharIndex = -1;
        private int m_lastLineIndex = -1;
        private int m_lastWordIndex = -1;

        [SerializeField] private CharacterSelectionEvent m_OnCharacterSelection = new CharacterSelectionEvent();

        [SerializeField] private LineSelectionEvent m_OnLineSelection = new LineSelectionEvent();

        [SerializeField] private LinkSelectionEvent m_OnLinkSelection = new LinkSelectionEvent();

        [SerializeField] private SpriteSelectionEvent m_OnSpriteSelection = new SpriteSelectionEvent();

        [SerializeField] private WordSelectionEvent m_OnWordSelection = new WordSelectionEvent();

        private int m_selectedLink = -1;


        private TMP_Text m_TextComponent;


        /// <summary>
        ///     Event delegate triggered when pointer is over a character.
        /// </summary>
        public CharacterSelectionEvent onCharacterSelection
        {
            get => m_OnCharacterSelection;
            set => m_OnCharacterSelection = value;
        }


        /// <summary>
        ///     Event delegate triggered when pointer is over a sprite.
        /// </summary>
        public SpriteSelectionEvent onSpriteSelection
        {
            get => m_OnSpriteSelection;
            set => m_OnSpriteSelection = value;
        }


        /// <summary>
        ///     Event delegate triggered when pointer is over a word.
        /// </summary>
        public WordSelectionEvent onWordSelection
        {
            get => m_OnWordSelection;
            set => m_OnWordSelection = value;
        }


        /// <summary>
        ///     Event delegate triggered when pointer is over a line.
        /// </summary>
        public LineSelectionEvent onLineSelection
        {
            get => m_OnLineSelection;
            set => m_OnLineSelection = value;
        }


        /// <summary>
        ///     Event delegate triggered when pointer is over a link.
        /// </summary>
        public LinkSelectionEvent onLinkSelection
        {
            get => m_OnLinkSelection;
            set => m_OnLinkSelection = value;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            //Debug.Log("OnPointerEnter()");
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            //Debug.Log("OnPointerExit()");
        }

        private void Awake()
        {
            // Get a reference to the text component.
            m_TextComponent = gameObject.GetComponent<TMP_Text>();

            // Get a reference to the camera rendering the text taking into consideration the text component type.
            if (m_TextComponent.GetType() == typeof(TextMeshProUGUI))
            {
                m_Canvas = gameObject.GetComponentInParent<Canvas>();
                if (m_Canvas != null)
                {
                    if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                        m_Camera = null;
                    else
                        m_Camera = m_Canvas.worldCamera;
                }
            }
            else
            {
                m_Camera = Camera.main;
            }
        }


        private void LateUpdate()
        {
            if (TMP_TextUtilities.IsIntersectingRectTransform(m_TextComponent.rectTransform, Input.mousePosition,
                m_Camera))
            {
                #region Example of Character or Sprite Selection

                var charIndex =
                    TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, Input.mousePosition, m_Camera, true);
                if (charIndex != -1 && charIndex != m_lastCharIndex)
                {
                    m_lastCharIndex = charIndex;

                    var elementType = m_TextComponent.textInfo.characterInfo[charIndex].elementType;

                    // Send event to any event listeners depending on whether it is a character or sprite.
                    if (elementType == TMP_TextElementType.Character)
                        SendOnCharacterSelection(m_TextComponent.textInfo.characterInfo[charIndex].character,
                            charIndex);
                    else if (elementType == TMP_TextElementType.Sprite)
                        SendOnSpriteSelection(m_TextComponent.textInfo.characterInfo[charIndex].character, charIndex);
                }

                #endregion


                #region Example of Word Selection

                // Check if Mouse intersects any words and if so assign a random color to that word.
                var wordIndex = TMP_TextUtilities.FindIntersectingWord(m_TextComponent, Input.mousePosition, m_Camera);
                if (wordIndex != -1 && wordIndex != m_lastWordIndex)
                {
                    m_lastWordIndex = wordIndex;

                    // Get the information about the selected word.
                    var wInfo = m_TextComponent.textInfo.wordInfo[wordIndex];

                    // Send the event to any listeners.
                    SendOnWordSelection(wInfo.GetWord(), wInfo.firstCharacterIndex, wInfo.characterCount);
                }

                #endregion


                #region Example of Line Selection

                // Check if Mouse intersects any words and if so assign a random color to that word.
                var lineIndex = TMP_TextUtilities.FindIntersectingLine(m_TextComponent, Input.mousePosition, m_Camera);
                if (lineIndex != -1 && lineIndex != m_lastLineIndex)
                {
                    m_lastLineIndex = lineIndex;

                    // Get the information about the selected word.
                    var lineInfo = m_TextComponent.textInfo.lineInfo[lineIndex];

                    // Send the event to any listeners.
                    var buffer = new char[lineInfo.characterCount];
                    for (var i = 0;
                        i < lineInfo.characterCount && i < m_TextComponent.textInfo.characterInfo.Length;
                        i++)
                        buffer[i] = m_TextComponent.textInfo.characterInfo[i + lineInfo.firstCharacterIndex].character;

                    var lineText = new string(buffer);
                    SendOnLineSelection(lineText, lineInfo.firstCharacterIndex, lineInfo.characterCount);
                }

                #endregion


                #region Example of Link Handling

                // Check if mouse intersects with any links.
                var linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextComponent, Input.mousePosition, m_Camera);

                // Handle new Link selection.
                if (linkIndex != -1 && linkIndex != m_selectedLink)
                {
                    m_selectedLink = linkIndex;

                    // Get information about the link.
                    var linkInfo = m_TextComponent.textInfo.linkInfo[linkIndex];

                    // Send the event to any listeners. 
                    SendOnLinkSelection(linkInfo.GetLinkID(), linkInfo.GetLinkText(), linkIndex);
                }

                #endregion
            }
        }


        private void SendOnCharacterSelection(char character, int characterIndex)
        {
            if (onCharacterSelection != null)
                onCharacterSelection.Invoke(character, characterIndex);
        }

        private void SendOnSpriteSelection(char character, int characterIndex)
        {
            if (onSpriteSelection != null)
                onSpriteSelection.Invoke(character, characterIndex);
        }

        private void SendOnWordSelection(string word, int charIndex, int length)
        {
            if (onWordSelection != null)
                onWordSelection.Invoke(word, charIndex, length);
        }

        private void SendOnLineSelection(string line, int charIndex, int length)
        {
            if (onLineSelection != null)
                onLineSelection.Invoke(line, charIndex, length);
        }

        private void SendOnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (onLinkSelection != null)
                onLinkSelection.Invoke(linkID, linkText, linkIndex);
        }

        [Serializable]
        public class CharacterSelectionEvent : UnityEvent<char, int>
        {
        }

        [Serializable]
        public class SpriteSelectionEvent : UnityEvent<char, int>
        {
        }

        [Serializable]
        public class WordSelectionEvent : UnityEvent<string, int, int>
        {
        }

        [Serializable]
        public class LineSelectionEvent : UnityEvent<string, int, int>
        {
        }

        [Serializable]
        public class LinkSelectionEvent : UnityEvent<string, string, int>
        {
        }
    }
}