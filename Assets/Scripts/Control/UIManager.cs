using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
	Text _goldText;

	public string countText = "Coins: ";

	public GameObject WinScreen;

	void Awake () {
		_goldText = GetComponentInChildren<Text>();
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
			collectCoin();
		} else if (eventName == Events.GameWon) {
			showWinScreen();
		}
	}

	void collectCoin () {
		GameManager.CollectCoin();
		updateCoinText();
	
	}

	void updateCoinText () {
		_goldText.text = countText + GameManager.Game.CoinsCollected;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && WinScreen.activeSelf) {
			Restart();
		}
	}

	void showWinScreen () {
		WinScreen.SetActive(true);
	}

	public void Restart () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
