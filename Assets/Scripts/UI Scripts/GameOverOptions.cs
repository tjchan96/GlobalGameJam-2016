﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GameOverOptions : MonoBehaviour {

	public GameObject menuPanel;

	public int sceneToStart = 0;										//Index number in build settings of scene to load if changeScenes is true
	//public bool changeMusicOnStart;										//Choose whether to continue playing menu music or start a new music clip


	[HideInInspector] public bool inMainMenu = true;					//If true, pause button disabled in main menu (Cancel in input manager, default escape key)
	[HideInInspector] public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
	public AnimationClip fadeColorAnimationClip;		//Animation clip fading to color (black default) when changing scenes

	//private PlayMusic playMusic;										//Reference to PlayMusic script
	private float fastFadeIn = .01f;									//Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
	private ShowPanels showPanels;										//Reference to ShowPanels script on UI GameObject, to show and hide panels

	
	void Awake()
	{
		//Get a reference to ShowPanels attached to UI object
		showPanels = GetComponent<ShowPanels> ();

		//Get a reference to PlayMusic attached to UI object
		//playMusic = GetComponent<PlayMusic> ();
	}


	public void MainMenuButtonClicked()
	{
		//If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of fadeColorAnimationClip as time. 
		//To change fade time, change length of animation "FadeToColor"
		/*if (changeMusicOnStart) 
		{
			playMusic.FadeDown(fadeColorAnimationClip.length);
		}		//If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
		*/
		//Use invoke to delay calling of LoadDelayed by half the length of fadeColorAnimationClip
		Invoke ("LoadDelayed", fadeColorAnimationClip.length * .5f);

		//Set the trigger of Animator animColorFade to start transition to the FadeToOpaque state.
		animColorFade.SetTrigger ("fade");
	}

	//Once the level has loaded, check if we want to call PlayLevelMusic
	void OnLevelWasLoaded()
	{
		//if changeMusicOnStart is true, call the PlayLevelMusic function of playMusic
		/*
		if (changeMusicOnStart)
		{
			playMusic.PlayLevelMusic ();
		}
		*/
	}


	public void LoadDelayed()
	{
		//Pause button now works if escape is pressed since we are no longer in Main menu.
		inMainMenu = false;

		//Hide the main menu UI element
		menuPanel.SetActive (false);

		//Load the selected scene, by scene index number in build settings
		SceneManager.LoadScene (sceneToStart);
	}

	public void HideDelayed()
	{
		//Hide the main menu UI element after fading out menu for start game in scene
		menuPanel.SetActive (false);
	}

	/*
	public void PlayNewMusic()
	{
		//Fade up music nearly instantly without a click 
		playMusic.FadeUp (fastFadeIn);
		//Play music clip assigned to mainMusic in PlayMusic script
		playMusic.PlaySelectedMusic (1);
	}
	*/
}
