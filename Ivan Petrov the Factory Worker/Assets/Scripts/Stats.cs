using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stats : MonoBehaviour {
	public static float health = 100;
	public static float satiety = 100;
	public static float happiness = 100;
	public static float drunkeness = 0;
	public static float patriotism = 100;
	public Slider healthSlider;
	public Slider satietySlider;
	public Slider happinessSlider;
	public Slider drunkenessSlider;
	public Slider patriotismSlider;

	private void UpdateSliders () {
		healthSlider.value = health;
		satietySlider.value = satiety;
		happinessSlider.value = happiness;
		drunkenessSlider.value = drunkeness;
		patriotismSlider.value = patriotism;
	}

	void Start () {
		healthSlider = GameObject.Find ("/HUD/Health/Slider").GetComponent<Slider>();
		satietySlider = GameObject.Find ("/HUD/Satiety/Slider").GetComponent<Slider>();
		happinessSlider = GameObject.Find ("/HUD/Happiness/Slider").GetComponent<Slider>();
		drunkenessSlider = GameObject.Find ("/HUD/Drunkeness/Slider").GetComponent<Slider>();
		patriotismSlider = GameObject.Find ("/HUD/Patriotism/Slider").GetComponent<Slider>();
		UpdateSliders ();
	}

	void Update () {
		health -= 0.01f;
		satiety -= 0.01f;
		happiness -= 0.01f;
		patriotism -= 0.01f;
		if (satiety < 20)
			health -= 0.01f;
		if (happiness < 20)
			patriotism -= 0.01f;
		if (drunkeness > 80) {
			health -= 0.01f;
			satiety -= 0.01f;
			patriotism += 0.01f;
		}
		UpdateSliders ();
	}
}
