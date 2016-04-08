using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	public static Game Game {
		get {
			return Instance.currentGame;
		}
	}

	public int PlaneWidth = 100;
	public int PlaneHeight = 100;
	public int PlaneDepth = 10;
	public int PathWidth = 5;

	Game currentGame;

	void Awake () {
		if (SingletonUtil.TryInit(ref Instance, this, gameObject)) {
			init();
		}
	}

	void init () {
		currentGame = new Game(PlaneWidth, PlaneHeight, PlaneDepth, PathWidth);
	}
}
