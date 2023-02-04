using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
	private Button previousButton;
	public float scaleAmount = 1.4f;
	public GameObject defaultButton = null;
	public bool disableMouse = true;

	[SerializeField]GameObject selected;

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

		selected = EventSystem.current.currentSelectedGameObject;

		if (disableMouse && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
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
				if (defaultButton != null)
                {
					selected = defaultButton;
                }

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
		EventSystem.current.SetSelectedGameObject(btn.gameObject);
		btn.transform.localScale = new Vector3(btn.transform.localScale.x * scaleAmount, btn.transform.localScale.y * scaleAmount, btn.transform.localScale.z * scaleAmount);
	}

	void UnHighlightButton(Button btn)
	{
		btn.transform.localScale = new Vector3(btn.transform.localScale.x / scaleAmount, btn.transform.localScale.y / scaleAmount, btn.transform.localScale.z / scaleAmount);
	}
}