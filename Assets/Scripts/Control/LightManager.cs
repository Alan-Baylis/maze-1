using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {
	Light mainLight;
	public Color StartColor;
	public Color EndColor;

	// Use this for initialization
	void Awake () {
		setReferences();
		subscribeEvents();
	}

	void OnDestroy () {
		unsubscribeEvents();
	}

	void subscribeEvents () {
		EventController.OnNamedEvent += handleNamedEvent;
	}

	void unsubscribeEvents () {
		EventController.OnNamedEvent -= handleNamedEvent;
	}

	void handleNamedEvent (string eventName) {
		if (eventName == Events.CoinCollected) {
			mainLight.color = Color.Lerp (
				StartColor,
				EndColor,
				GameManager.Game.PercentToVictory()
			);
		}
	}

	void setReferences () {
		mainLight = GetComponent<Light>();
	}

}
