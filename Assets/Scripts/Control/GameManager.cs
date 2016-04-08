using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;

	public static Game Game {
		get {
			return Instance.currentGame;
		}
	}

	public static GameObject PlanePrefab {
		get {
			return Instance.WorldPlanePrefab;
		}
	}

	public static GameObject World {
		get {
			return Instance.WorldParent;
		}
	}

	public GameObject WorldPlanePrefab;
	public GameObject CoinPrefab;
	public GameObject WorldParent;

	public int PlaneWidth = 100;
	public int PlaneHeight = 100;
	public int PlaneDepth = 10;
	public int PathWidth = 5;
	public float MaximumDistance = 10000;
	public float BlockScale = 1;

	Game currentGame;

	void Awake () {
		if (SingletonUtil.TryInit(ref Instance, this, gameObject)) {
			init();
		}
	}

	void init () {
		currentGame = new Game(PlaneWidth, PlaneHeight, PlaneDepth, PathWidth, MaximumDistance, BlockScale);
	}

	public static void CollectCoin () {
		Game.CoinsCollected++;
	}
}
