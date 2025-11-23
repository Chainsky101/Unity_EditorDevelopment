using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePanelBase : MonoBehaviour
    {
        //first Part: declare UI field
		public Image GamePanelImage;
		public Image btn1Image;
		public Button btn1Button;
		public Image btn2Image;
		public Button btn2Button;
		public Text txt3Text;
		public Image dpl1Image;
		public Image ArrowImage;
		public Image if1Image;

        
        
        protected virtual void Start()
        {
            //second Part: binding UI field with its corresponding component
			GamePanelImage = transform.Find("GamePanelImage").GetComponent<Image>();
			btn1Image = transform.Find("btn1Image").GetComponent<Image>();
			btn1Button = transform.Find("btn1Button").GetComponent<Button>();
			btn2Image = transform.Find("btn2Image").GetComponent<Image>();
			btn2Button = transform.Find("btn2Button").GetComponent<Button>();
			txt3Text = transform.Find("txt3Text").GetComponent<Text>();
			dpl1Image = transform.Find("dpl1Image").GetComponent<Image>();
			ArrowImage = transform.Find("ArrowImage").GetComponent<Image>();
			if1Image = transform.Find("if1Image").GetComponent<Image>();

            
            //third Part: add invoked event for specular UI control
			btn1Button.onClick.AddListener(Onbtn1ButtonListener);
			btn2Button.onClick.AddListener(Onbtn2ButtonListener);

        }

        //fourth Part: write invoked function
		protected virtual void Onbtn1ButtonListener()
		{}

		protected virtual void Onbtn2ButtonListener()
		{}


    }
}