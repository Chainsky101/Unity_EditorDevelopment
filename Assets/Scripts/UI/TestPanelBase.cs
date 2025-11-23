using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TestPanelBase : MonoBehaviour
    {
        //first Part: declare UI field
        public Button btn1;
        public Button btn2;
        public Button btn3;
        public TextMeshPro test1;
        public Toggle tog1;
        public Slider slider1;
        public Dropdown dpl;
        public InputField inputField1;
        protected virtual void Start()
        {
            //second Part: binding UI field with its corresponding component
            btn1 = this.transform.Find("gameObject name").GetComponent<Button>();

            //third Part: add invoked event for specular UI control
            btn1.onClick.AddListener(InvokeMethod);
            tog1.onValueChanged.AddListener(OnToggleListener);
            slider1.onValueChanged.AddListener(OnSliderListener);
            dpl.onValueChanged.AddListener(OnDropDownListener);
            inputField1.onSubmit.AddListener(OnInputFieldListener);
        }

        //fourth Part: write invoked function
        protected virtual void InvokeMethod()
        {

        }

        protected virtual void OnToggleListener(bool isOn)
        {
            
        }
        protected virtual void OnSliderListener(float val)
        {
            
        }
        protected virtual void OnDropDownListener(int val)
        {
            
        }
        
        protected virtual void OnInputFieldListener(string content)
        {
            
        }
        
    }
}
