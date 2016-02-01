using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject helpPanel;							//Store a reference to the Game Object HelpPanel 
	public GameObject menuTint;								//Store a reference to the Game Object MenuTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;							//Store a reference to the Game Object PausePanel 


	//Call this function to activate and display the Options panel during the main menu
	public void ShowHelpPanel()
	{
		helpPanel.SetActive(true);
		menuTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideHelpPanel()
	{
		helpPanel.SetActive(false);
		menuTint.SetActive(false);
	}

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}

	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		menuTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		menuTint.SetActive(false);

	}
}
