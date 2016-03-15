using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour 
{
	public Text timerText;
	public Image pausedText, instructions;
	public Slider healthBar;
	public Button MainMenuBtn, helpButton, okay;
	public float lightIntensity;

	public bool paused = false;
	[HideInInspector]
	public float timer;
	private MouseLook mouseLook;
	public Light playerLantern;

	void Start()
	{
		Screen.lockCursor = true;
		Time.timeScale = 1.0f;
		InvokeRepeating("RegenerateHealth", 1f, 0.1f);
		pausedText.gameObject.SetActive(false);
		instructions.gameObject.SetActive(false);
		MainMenuBtn.gameObject.SetActive(false);
		helpButton.gameObject.SetActive(false);
		okay.gameObject.SetActive(false);
		mouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
		playerLantern = GameObject.FindGameObjectWithTag("Lantern").GetComponent<Light>();

		if (Application.loadedLevel == 1) {
			timer = 0f;
		}
		else if (Application.loadedLevel == 4 || Application.loadedLevel == 5 || Application.loadedLevel == 6 || Application.loadedLevel == 7) {
			GameObject count = GameObject.Find ("Counter");
			TimerScript tempTimer = count.GetComponent<TimerScript>();
			timer = tempTimer.timer;
		}
	}

	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			paused = !paused;
			mouseLook.enabled = !paused; //disable mouse look script when paused
			pausedText.gameObject.SetActive(paused); //display pause text
			MainMenuBtn.gameObject.SetActive(paused); //display main menu button
			helpButton.gameObject.SetActive(paused); //display help button

			//freeze everything when paused, including game objects, gui timer
			if(paused)
			{
				Time.timeScale = 0f;
				Screen.lockCursor = false;
			}
			else
			{
				Time.timeScale = 1.0f;
				Screen.lockCursor = true;
			}
		}

		timer += Time.deltaTime;
		timerText.text = timer.ToString("F0");

	}

	// regenerates the health and gives speed based on the current health
	void RegenerateHealth()
	{
		healthBar.value += 0.01f;
		playerLantern.intensity = healthBar.value * 1.4f;
		GameObject player = GameObject.Find("Player");
		if(healthBar.value <= 0.67f && healthBar.value > .34) {
			player.GetComponent<Rigidbody>().mass = 2.3f;
		}
		else if(healthBar.value <= 0.34) {
			player.GetComponent<Rigidbody>().mass = 2.1f;
		}
		else {
			player.GetComponent<Rigidbody>().mass = 2.5f;
		}
	}

	public void BackToMainMenu()
	{
		Application.LoadLevel(0);
	}

	public void ToggleHelp()
	{
		instructions.gameObject.SetActive(true);
		okay.gameObject.SetActive(true);
	}

	public void DisableHelp()
	{
		instructions.gameObject.SetActive(false);
		okay.gameObject.SetActive(false);
	}
}