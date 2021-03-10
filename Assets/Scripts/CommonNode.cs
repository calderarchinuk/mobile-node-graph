using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CommonNode : MonoBehaviour
{
	public CommonNodeData data;
	public Text displayText;
	public InputField InputField;
	public Image Background;

	public void Initialize()
	{
		InputField.text = data.Name;
	}

	public void PointerClick()
	{
		//TODO callback to some state manager to set this as selected
	}

	public void Hide ()
	{
		GetComponent<Image>().enabled = false;
	}

	public void Show ()
	{
		GetComponent<Image>().enabled = true;
	}

	public void Fade ()
	{
		GetComponent<Image>().color = GetComponent<Image>().color.SetAlpha(0.5f);
	}

	public void Appearify ()
	{
		GetComponent<Image>().color = GetComponent<Image>().color.SetAlpha(1);
	}

	public void SetSelected (bool b)
	{
		InputField.enabled = b;
		if (b)
		{
			Background.color = Color.green;
		}
		else
		{
			Background.color = Color.white;
		}
	}

	public void SetDisplayName(string text)
	{
		data.Name = text;
	}
}
