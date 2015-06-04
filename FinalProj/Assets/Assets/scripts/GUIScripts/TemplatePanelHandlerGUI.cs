using UnityEngine;
using System.Collections;

namespace GUIScripts
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class TemplatePanelHandlerGUI : MonoBehaviour
    {

        [SerializeField]
        protected GameObject _Button;
        private int _numButtons;

        public const int ButtonHeight = 30;
        public const int ButtonWidth = 160;
        public const int MaxPanelHeight = ButtonHeight * 10;

        void Start()
        {
            Hook1();
            _numButtons = ConstructButtons();
            ResizePanelToButtons(_numButtons);
        }

        protected abstract int ConstructButtons();
        protected virtual void Hook1()
        {}

        private void ResizePanelToButtons(int numButtons)
        {
            
            RectTransform trans = this.GetComponent<RectTransform>();
            float tmp = trans.anchoredPosition3D.x;
            trans.sizeDelta = new Vector2(ButtonWidth, (numButtons * ButtonHeight));

            float temp = numButtons * ButtonHeight / 2;

            trans.anchoredPosition3D = new Vector3(tmp, -temp, 0);
        }
    }
}

