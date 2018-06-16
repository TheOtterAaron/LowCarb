using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEvents : MonoBehaviour
{
    public Button addChipButton;
    public Button quitButton;
    public CanvasGroup chipList;

    // Use this for initialization
    void Start()
	{
        addChipButton.onClick.AddListener(OnAddChipButton);
        quitButton.onClick.AddListener(OnQuitButton);

        ToggleChipListGroup(false);
    }
	
	void OnAddChipButton()
	{
        bool toggleOn = !chipList.interactable;
        ToggleChipListGroup(toggleOn);
    }
    
    void OnQuitButton()
    {
        Application.Quit();
    }

	private void ToggleChipListGroup(bool toggleOn)
	{
        chipList.alpha = toggleOn ? 1.0f : 0.0f;
        chipList.interactable = toggleOn;
        chipList.blocksRaycasts = toggleOn;
    }
}
