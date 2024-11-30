using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	[Header("UI Elements")]
	//for Button list
	public List<Button> buttons;

    //for Sprite
    public Sprite greenSprite;
    public Sprite greenSprite2;
    public Sprite baseSprite;
    public Sprite baseSprite2;

    //for Gameobjects
    public GameObject[] disabledPanels;

    //for image
    public Image[] glowImages;

	[Header("Counter")]
	//For Counter
	/// <summary>
	/// For storing current button count. Which reward is lastly collected.
	/// --> It starts from '0'(zero) like for Day 1 button counter is 0, for Day 2 counter is 1 and so on upto for day 7 counter will be 6. 
	/// </summary>
	[SerializeField] int counter=0;
	private const int maxDays = 7;

	[Header("Effects")]
	//for Particle
	public ParticleSystem confettiParticles;



	private void Start()
	{
		InitializeButtons();
		InitializePanels();
		InitializeGlowImages();

		// Highlight the first button
		UpdateButtonAppearance(counter, greenSprite);
		glowImages[counter].SetTransparency(1);
	}

	private void InitializeButtons()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].interactable = i == counter; // Enable only the current button
		}
	}

	private void InitializePanels()
	{
		foreach (var panel in disabledPanels)
		{
			panel.SetActive(false);
		}
	}

	private void InitializeGlowImages()
	{
		for (int i = 0; i < glowImages.Length; i++)
		{
			glowImages[i].SetTransparency(0); // Make all glow images transparent initially
		}
	}
	public void OnButtonPressed()
    {
		// Disable current button and set its sprite to the base
		buttons[counter].interactable = false;
		UpdateButtonAppearance(counter, counter < 6 ? baseSprite : baseSprite2);
		glowImages[counter].SetTransparency(0);
		disabledPanels[counter].SetActive(true);

		counter++;

		if (counter < maxDays ) // Day 1 to Day 6
		{
			UpdateButtonAppearance(counter, counter < 6 ? greenSprite : greenSprite2);
			buttons[counter].interactable = true;
			glowImages[counter].SetTransparency(1);
		}
		else if (counter == maxDays ) // Day 7
		{
			confettiParticles.Play();
		}
	}

	private void UpdateButtonAppearance(int index, Sprite sprite)
	{
		if (index < buttons.Count)
		{
			buttons[index].image.sprite = sprite;
		}
	}
}
