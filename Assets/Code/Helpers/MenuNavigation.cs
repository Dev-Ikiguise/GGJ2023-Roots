using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
	private Button previousButton;
	[SerializeField] private float scaleAmount = 1.4f;
	[SerializeField] private GameObject defaultButton = null;
	bool disableMouse = true;

	void Start()
	{
		if (disableMouse)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		if (defaultButton != null)
		{
			EventSystem.current.SetSelectedGameObject(defaultButton);
		}
	}
	void Update()
	{

		GameObject selected = EventSystem.current.currentSelectedGameObject;

		if (disableMouse && Input.GetButton("MouseAny"))
		{
			//Hides selecting of gameobjects through mouse clicks.  Simplistic, but effective in most cases.
			EventSystem.current.SetSelectedGameObject(previousButton.gameObject);
		}

		if (selected == null)
        {
			if (previousButton != null)
            {
				selected = previousButton.gameObject;
            }
			else
            {
				return;
            }
        }

		Button selectedAsButton = selected.GetComponent<Button>();
		if (selectedAsButton != null && selectedAsButton != previousButton)
		{
			if (selectedAsButton.transform.name != "PauseButton")
				HighlightButton(selectedAsButton);
		}

		if (previousButton != null && previousButton != selectedAsButton)
		{
			UnHighlightButton(previousButton);
		}
		previousButton = selectedAsButton;
	}

    public void Quit()
    {
        Application.Quit();
		Debug.Log("Quitting Application");
    }

    void OnDisable()
	{
		if (previousButton != null) UnHighlightButton(previousButton);
	}

	void HighlightButton(Button btn)
	{
		btn.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
	}

	void UnHighlightButton(Button btn)
	{
		btn.transform.localScale = new Vector3(1, 1, 1);
	}
}