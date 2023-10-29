using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolkitMessageSystem
{
    public class Message : MonoBehaviour
    {
        private VisualElement _root;
        private Label _messageLabel;
        private string _offClass;

        public Message(VisualElement root, string msg, float timer, MessageColor color, Font font, int size, string offClass)
        {
            _root = root;
            var body  = root.Q<VisualElement>("message");
            body.AddToClassList(color.ToString().ToLower());
            _messageLabel = root.Q<Label>("message-label");

            _messageLabel.text = msg;
            _messageLabel.style.fontSize = size;
            _messageLabel.style.unityFont = font;

            _offClass = offClass;
            Dissapear(timer);
        }

        private async void Dissapear(float time)
        {
            //��� ms ������ int���� �޴µ�
            await Task.Delay(Mathf.FloorToInt(time*1000));
            _root.AddToClassList(_offClass);
            await Task.Delay(1000);
            _root.RemoveFromHierarchy();
        }
    }
}
