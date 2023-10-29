using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolkitMessageSystem
{
    public enum MessagePosition : ushort
    {
        TopLeft = 1,
        BottomLeft = 2,
        TopRight = 3,
        BottomRight = 4,
    }
    public class MessageSystem : MonoBehaviour
    {
        [SerializeField] private MessagePosition _position;
        [SerializeField] private VisualTreeAsset _messageTemplate;
        [SerializeField] private float _dissaperTime = 1.5f;
        [SerializeField] Font _font;
        [SerializeField][Range(10, 45)] private int _fontSize;

        private UIDocument _uiDocument;
        private VisualElement _msgBoxElement;

        private void Awake()
        {
           _uiDocument = GetComponent<UIDocument>();
        }

        private void OnEnable()
        {
            // 여기서 메시지 발행시 나오는 코드 구독 해야하는데 일단 보류
            MessageHub.OnMessage += HandleMessage;
            var root = _uiDocument.rootVisualElement;
            _msgBoxElement = root.Q<VisualElement>("message-box");

            switch (_position)
            {
                case MessagePosition.TopLeft:
                    _msgBoxElement.style.left = 20;
                    _msgBoxElement.style.top = 20;
                    break;
                case MessagePosition.TopRight:
                    _msgBoxElement.style.right = 20;
                    _msgBoxElement.style.top = 20;
                    break;
                case MessagePosition.BottomLeft:
                    _msgBoxElement.style.left = 20;
                    _msgBoxElement.style.bottom = 20;
                    break;
                case MessagePosition.BottomRight:
                    _msgBoxElement.style.right = 20;
                    _msgBoxElement.style.bottom = 20;
                    break;
            }
        }
        private void OnDisable()
        {
            MessageHub.OnMessage -= HandleMessage;
        }

        private void HandleMessage(string msg,MessageColor color)
        {
            TemplateContainer template = _messageTemplate.Instantiate();
            string offClass = (ushort)_position <= 2 ? "left-off" : "right-off";
            Message message = new Message(template, msg, _dissaperTime, color, _font, _fontSize,offClass);

            _msgBoxElement.Add(template);

        }
    }
}