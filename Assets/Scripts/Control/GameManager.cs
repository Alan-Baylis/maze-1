using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	public int PlaneWidth = 100;
	public int PlaneLength = 100;
	public int PlaneHeight = 10;

	Game currentGame;

	void Awake () {
		if (SingletonUtil.TryInit(ref Instance, this, gameObject)) {
			init();
		}
	}

	// Use this for initialization
	void Start () {
	
	}

	void init () {
		currentGame = new Game(PlaneWidth, PlaneLength, PlaneHeight);
	}

	public static Game CurrentGame () {
		return Instance.currentGame;
	}
}
