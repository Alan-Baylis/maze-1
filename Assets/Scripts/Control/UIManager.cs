using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	Text _goldText;

	public string countText = "Coins: ";

	void Awake () {
		_goldText = GetComponentInChildren<Text>();
		subscribeEvents();
	}

	void subscribeEvents () {
		EventController.OnNamedEvent += handleNamedEvent;
	}

	void unsubscribeEvents () {
		EventController.OnNamedEvent -= handleNamedEvent;
	}

	void handleNamedEvent (string eventName) {
		if (eventName == Events.CoinCollected) {
			collectCoin();
		}
	}

	void collectCoin () {
		GameManager.CollectCoin();
		updateCoinText();
	
	}

	void updateCoinText () {
		_goldText.text = countText + GameManager.Game.CoinsCollected;
	}
}
