using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]

// This color palette class manages all the colors and themes for the UI

public class VRUIColorPalette : MonoBehaviour
{
	public static VRUIColorPalette Instance;

	public bool isDarkTheme = true;

	// Colors from GUI

	public Color accentColor;
	public Color hoverColor;
	public Color pressedColor;
	public Color disabledColor;

	// Colors hardcoded for Dark and Light themes

	private Color textColorDark = new Color(1f, 1f, 1f, 1f);
	private Color textColorLight = new Color(0.1058824f, 0.1058824f, 0.1058824f, 1f);
	private Color bgColorDark = new Color(0.2588235f, 0.2588235f, 0.2588235f, 1f);
	private Color bgColorLight = new Color(1f, 1f, 1f, 1f);
	private Color outlineDark = new Color (0.8784314f, 0.8784314f, 0.8784314f, 1f);
	private Color outlineLight = new Color (0.6196979f, 0.6196979f, 0.6196979f, 1f);

	private Color accentUIDark = new Color(1f, 1f, 1f, 0.0f); 
	private Color hoverUIDark = new Color(1f, 1f, 1f, 0.2f); 
	private Color pressedUIDark = new Color(1f, 1f, 1f, 0.4f); 
	private Color disabledUIDark =new Color(1f, 1f, 1f, 0.1f); 

	private Color accentUILight = new Color(0f, 0f, 0f, 0.0f); 
	private Color hoverUILight = new Color(0f, 0f, 0f, 0.1f); 
	private Color pressedUILight = new Color(0f, 0f, 0f, 0.2f); 
	private Color disabledUILight = new Color(0f, 0f, 0f, 0.05f);


	void Awake() {

		// Update colors on wakeup
		if (Instance == null)
		{
			Instance = this;
		}

		UpdateColors ();
	}

	public void UpdateColors ()
	{
		
		// Find all the UI components in the scene and update their colors and themes

		GameObject[] bgs = GameObject.FindGameObjectsWithTag ("VRUIBackground");

		foreach (GameObject bg in bgs) 
		{
			bg.GetComponent<Image>().color = isDarkTheme ? bgColorDark : bgColorLight;
		}

        GameObject[] gfxs = GameObject.FindGameObjectsWithTag("VRUIAccent");

        foreach (GameObject gfx in gfxs)
        {
            gfx.GetComponent<RawImage>().color = accentColor;
        }

        GameObject[] texts = GameObject.FindGameObjectsWithTag ("VRUIText");

		foreach (GameObject txt in texts) 
		{
			if (txt.GetComponent<Text>() != null)
			{
				txt.GetComponent<Text>().color = isDarkTheme ? textColorDark : textColorLight;
			}
			if (txt.GetComponent<TMPro.TextMeshProUGUI>() != null)
			{
				txt.GetComponent<TMPro.TextMeshProUGUI>().color = isDarkTheme ? textColorDark : textColorLight;
			}
		}

		GameObject[] icons = GameObject.FindGameObjectsWithTag ("VRUIIcon");

		foreach (GameObject ico in icons) 
		{
			if (ico.GetComponent<RawImage>() != null)
			{
				ico.GetComponent<RawImage>().color = isDarkTheme ? textColorDark : textColorLight;
			}
			if (ico.GetComponent<Image>() != null)
			{
				ico.GetComponent<Image>().color = isDarkTheme ? textColorDark : textColorLight;
			}
		}

		GameObject[] outlines = GameObject.FindGameObjectsWithTag ("VRUIOutline");

		foreach (GameObject line in outlines) 
		{
			line.GetComponent<Image>().color = isDarkTheme ? outlineDark : outlineLight;
		}

		GameObject[] buttons = GameObject.FindGameObjectsWithTag("VRUIButton");

		foreach (GameObject btn in buttons) 
		{
			ColorBlock colors = btn.GetComponent<Button>().colors;

			btn.GetComponent<Button>().colors =  SetColorBlockFromGUI(colors);
		}

        GameObject[] buttonIcons = GameObject.FindGameObjectsWithTag("VRUIButtonIcon");

        foreach (GameObject btnIcon in buttonIcons)
        {
            ColorBlock colors = btnIcon.GetComponent<Button>().colors;

            btnIcon.GetComponent<Button>().colors = SetColorBlockFromTheme(colors);
        }

        GameObject[] tabBtns = GameObject.FindGameObjectsWithTag ("VRUIButtonTab");

		foreach (GameObject tab in tabBtns) 
		{
			
			ColorBlock colors = tab.GetComponent<Button>().colors;

			tab.GetComponent<VRUITabButton>().colors = SetColorBlockFromTheme(colors);

			tab.GetComponent<VRUITabButton>().setColors(accentColor);
		}

		GameObject[] controlBtns = GameObject.FindGameObjectsWithTag ("VRUIButtonControlBar");

		foreach (GameObject control in controlBtns) 
		{
			ColorBlock colors = control.GetComponent<Button>().colors;

			colors = SetColorBlockFromTheme (colors);

			colors.highlightedColor = isDarkTheme ? new Color (1f, 1f, 1f, 0f) : new Color (0f, 0f, 0f, 0f);

			control.GetComponent<VRUIControlBarButton>().colors = colors;

			control.GetComponent<VRUIControlBarButton>().setColors(accentColor, isDarkTheme ? textColorDark : textColorLight);
		}

		GameObject[] listBtns = GameObject.FindGameObjectsWithTag ("VRUIButtonList");

		foreach (GameObject listBtn in listBtns) 
		{
			ColorBlock colors = listBtn.GetComponent<Button>().colors;

			listBtn.GetComponent<Button>().colors =  SetColorBlockFromTheme(colors);
		}

		GameObject[] checkBoxes = GameObject.FindGameObjectsWithTag ("VRUICheckbox");

		foreach (GameObject check in checkBoxes) 
		{
			ColorBlock colors = check.GetComponent<VRUICheckbox>().colors;

			check.GetComponent<VRUICheckbox>().colors = SetColorBlockFromTheme(colors);

			check.GetComponent<VRUICheckbox>().setColors(accentColor);
		}

		GameObject[] toggles = GameObject.FindGameObjectsWithTag ("VRUIToggle");

		foreach (GameObject toggle in toggles) 
		{
			ColorBlock colors = toggle.GetComponent<VRUIToggle>().colors;

			toggle.GetComponent<VRUIToggle>().colors = SetColorBlockFromGUI(colors);
		}

		GameObject[] radioBtns = GameObject.FindGameObjectsWithTag ("VRUIRadio");

		foreach (GameObject radio in radioBtns) 
		{
			ColorBlock colors = radio.GetComponent<VRUIRadio>().colors;

			radio.GetComponent<VRUIRadio>().colors = SetColorBlockFromTheme(colors);

			radio.GetComponent<VRUIRadio>().setColors(accentColor, isDarkTheme ? outlineDark : outlineLight);
		}

		GameObject[] sliders = GameObject.FindGameObjectsWithTag ("VRUISlider");

		foreach (GameObject slide in sliders) 
		{
			slide.GetComponent<VRUISlider>().setColors(accentColor);
		}

		GameObject[] dropdowns = GameObject.FindGameObjectsWithTag("VRUIDropdown");

		foreach (GameObject dropdown in dropdowns)
		{
			dropdown.GetComponent<VRUIDropdown>().setColors(accentColor);
		}
	}

	// Update ColorBlock with colors from GUI inputs

	private ColorBlock SetColorBlockFromGUI (ColorBlock cb) 
	{
		cb.normalColor = accentColor;
		cb.highlightedColor = hoverColor;
		cb.pressedColor = pressedColor;
		cb.disabledColor = disabledColor;

		return cb;
	}

	// Update ColorBlocks with colors from theme

	private ColorBlock SetColorBlockFromTheme (ColorBlock cb) 
	{
		cb.normalColor = isDarkTheme ? accentUIDark : accentUILight;
		cb.highlightedColor = isDarkTheme ? hoverUIDark : hoverUILight;
		cb.pressedColor = isDarkTheme ? pressedUIDark : pressedUILight;
		cb.disabledColor = isDarkTheme ? disabledUIDark : disabledUILight;

		return cb;
	}
		
}
