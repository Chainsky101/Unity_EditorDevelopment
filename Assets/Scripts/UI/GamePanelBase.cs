using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePanelBase : MonoBehaviour
    {
        //first Part: declare UI field
		public Image GamePanelImage;
		public Toggle tog1Toggle;
		public Slider slider1Slider;
		public Image btn1Image;
		public Button btn1Button;
		public Image dd1Image;
		public TMP_Dropdown dd1TMP_Dropdown;
		public Image if1Image;
		public TMP_InputField if1TMP_InputField;

        
        
        protected virtual void Start()
        {
            //second Part: binding UI field with its corresponding component
			GamePanelImage = transform.Find("GamePanelImage").GetComponent<Image>();
			tog1Toggle = transform.Find("tog1Toggle").GetComponent<Toggle>();
			slider1Slider = transform.Find("slider1Slider").GetComponent<Slider>();
			btn1Image = transform.Find("btn1Image").GetComponent<Image>();
			btn1Button = transform.Find("btn1Button").GetComponent<Button>();
			dd1Image = transform.Find("dd1Image").GetComponent<Image>();
			dd1TMP_Dropdown = transform.Find("dd1TMP_Dropdown").GetComponent<TMP_Dropdown>();
			if1Image = transform.Find("if1Image").GetComponent<Image>();
			if1TMP_InputField = transform.Find("if1TMP_InputField").GetComponent<TMP_InputField>();

            
            //third Part: add invoked event for specular UI control
			tog1Toggle.onValueChanged.AddListener(Ontog1ToggleListener);
			slider1Slider.onValueChanged.AddListener(Onslider1SliderListener);
			btn1Button.onClick.AddListener(Onbtn1ButtonListener);
			dd1TMP_Dropdown.onValueChanged.AddListener(Ondd1TMP_DropdownListener);
			if1TMP_InputField.onSubmit.AddListener(Onif1TMP_InputFieldListener);

        }

        //fourth Part: write invoked function
		protected virtual void Ontog1ToggleListener(bool isOn)
		{}

		protected virtual void Onslider1SliderListener(float val)
		{}

		protected virtual void Onbtn1ButtonListener()
		{}

		protected virtual void Ondd1TMP_DropdownListener(int val)
		{}

		protected virtual void Onif1TMP_InputFieldListener(string content)
		{}


    }
}