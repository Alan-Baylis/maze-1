﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WorldPlane : MonoBehaviour {
	public static float PlaneSize = 100;
	public static int PlanesAwayLoadOutCount = 3;
	public static int TotalLoadCount = 0;
	public static int MaximumLoadDifference = 8;

	public bool ShouldSpawnBlocks = true;
	public bool SpawnBlocksAsync = true;
	public int LoadsPerFrame = 5;
	static List<WorldPlane> allPlanes = new List<WorldPlane>();

	public WorldPlane North;
	public WorldPlane South;
	public WorldPlane East;
	public WorldPlane West;

	WorldBlock[,,] blocks;

	int loadCount;

	bool isPlayerStandingOnPlane = false;

	void Start () {
		init();
	}

	void spawnBlocks () {

		int width = GameManager.Game.PlaneWidth/(int)GameManager.Game.BlockScale;
		int length = GameManager.Game.PlaneHeight/(int)GameManager.Game.BlockScale;
		int height = GameManager.Game.PlaneDepth/(int)GameManager.Game.BlockScale;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < height; z++) {
					spawnBlock(blocks[x, y, z].Type(), x, y, z);
				}
			}
		}
	}

	IEnumerator spawnBlocksCoroutine () {
		int width = GameManager.Game.PlaneWidth/(int)GameManager.Game.BlockScale;
		int length = GameManager.Game.PlaneHeight/(int)GameManager.Game.BlockScale;
		int height = GameManager.Game.PlaneDepth/(int)GameManager.Game.BlockScale;
		int loadsSinceLastFrame = 0;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < height; z++) {
					spawnBlock(blocks[x, y, z].Type(), x, y, z);
					loadsSinceLastFrame++;

					if (loadsSinceLastFrame >= LoadsPerFrame) {
						loadsSinceLastFrame = 0;
						yield return new WaitForEndOfFrame();
					}
				}
			}
		}
	}

	void init () {
		blocks = new BlockGenerator().Generate();
		loadCount = TotalLoadCount++;

		allPlanes.Add(this);

		if (ShouldSpawnBlocks) {
			if (SpawnBlocksAsync) {
				StartCoroutine(spawnBlocksCoroutine());
			} else {
				spawnBlocks();
			}
		}
	}

	void spawnBlock (BlockType type, int x, int y, int z) {
		GameObject block = null;
		if (type == BlockType.Cube || type == BlockType.Coin) {
			if (type == BlockType.Cube) {
				block =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			} else if (type == BlockType.Coin) {
				block = (GameObject) Instantiate(GameManager.Instance.CoinPrefab);
			}

			block.transform.SetParent(transform);
			if (type == BlockType.Cube) {
				block.transform.localScale *= GameManager.Game.BlockScale;
			}
			block.transform.localPosition = getBlockPosition(x, y, z);
			if (type == BlockType.Cube) {
				block.GetComponent<Renderer>().material.color = color(block.transform.position);
			}
		}
	}

	Vector3 getBlockPosition (int x, int y, int z) {
		return new Vector3(
			((float)x - (float)GameManager.Game.PlaneWidth/2f/GameManager.Game.BlockScale) / GameManager.Game.BlockScale * GameManager.Game.BlockScale/2f,
			(((float)y) / GameManager.Game.BlockScale * GameManager.Game.BlockScale/2f) + GameManager.Game.BlockScale/32f, 
			((float)z - (float)GameManager.Game.PlaneDepth/2f/GameManager.Game.BlockScale) / GameManager.Game.BlockScale * GameManager.Game.BlockScale/2f
		);
	}

	Color color (Vector3 worldDistance) {
		float distanceFromOrigin = Vector3.Distance(Vector3.zero, worldDistance);
		float max = GameManager.Game.MaxDistance;

		return new Color(
			Random.Range(0, 1.0f),
			1 - distanceFromOrigin/max,
			1 - distanceFromOrigin/max
		);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			checkDirections();
			checkAllDirectionsForLoadOut();
			checkLoadCounts();
			isPlayerStandingOnPlane = true;
		}
	}

	void OnCollisionExit (Collision collision) {
		if (collision.gameObject.tag == "Player") {
			isPlayerStandingOnPlane = false;
		}
	}

	void checkDirections () {
		if (North == null) {
			spawnNewPlane(Direction.North);
		}

		if (South == null) {
			spawnNewPlane(Direction.South);
		}

		if (East == null) {
			spawnNewPlane(Direction.East);
		}

		if (West == null) {
			spawnNewPlane(Direction.West);
		}
	}

	void spawnNewPlane (Direction direction, bool recurse = true) {
		GameObject planeObject = (GameObject) Instantiate(GameManager.PlanePrefab, transform.position + getOffset(direction), Quaternion.identity);
		planeObject.name = gameObject.name + direction.ToString();
		planeObject.transform.SetParent(GameManager.World.transform);
		WorldPlane plane = planeObject.GetComponent<WorldPlane>();

		setPlaneByDirection(direction, plane);
		plane.setPlaneByDirection(DirectionUtil.Reverse(direction), this);

		checkPerpendiculars(plane, direction, recurse);
	}

	void checkPerpendiculars (WorldPlane plane, Direction direction, bool recurse = true) {
		Direction[] directionsToCheck = DirectionUtil.Perpendiculars(direction);

		for (int i = 0; i < directionsToCheck.Length; i++) {
			WorldPlane targetPlane = getPlaneByDirection(directionsToCheck[i]);
			if (targetPlane != null) {
				targetPlane.setPlaneByDirection(direction, plane);
				plane.setPlaneByDirection(DirectionUtil.Reverse(direction), targetPlane);
				if (recurse) {
					targetPlane.spawnNewPlane(direction, false);
				}
			}
		}
	}

	WorldPlane getPlaneByDirection (Direction direction) {
		switch (direction) {

		case Direction.North:
			return North;

		case Direction.South:
			return South;

		case Direction.East:
			return East;

		case Direction.West:
			return West;

		default:
			return null;
		}
	}

	void setPlaneByDirection (Direction direction, WorldPlane plane) {
		switch (direction) {

		case Direction.North:
			North = plane;
			break;

		case Direction.South:
			South = plane;
			break;

		case Direction.East:
			East = plane;
			break;

		case Direction.West:
			West = plane;
			break;

		}
	}

	void checkLoadCounts () {
		List<WorldPlane> toDelete = allPlanes.FindAll(i => loadCount - i.loadCount > MaximumLoadDifference);
		allPlanes.RemoveAll(i => loadCount - i.loadCount > MaximumLoadDifference);

		foreach (WorldPlane plane in toDelete) {
			if (plane != null && plane.gameObject != null) {
				plane.requestDestroy();
			}
		}
	}

	void requestDestroy () {
		if (!isPlayerStandingOnPlane) {
			Destroy(gameObject);
		}
	}

	void checkAllDirectionsForLoadOut () {
		checkForLoadOut(Direction.North);
		checkForLoadOut(Direction.South);
		checkForLoadOut(Direction.East);
		checkForLoadOut(Direction.West);
	}

	void checkForLoadOut (Direction direction, int planesAwayFromPlayer = 0) {

		if (planesAwayFromPlayer >= PlanesAwayLoadOutCount) {
			requestDestroy();
			return;
		}

		if (getPlaneByDirection(direction) != null) {
			getPlaneByDirection(direction).checkForLoadOut(direction, planesAwayFromPlayer + 1);
		}
	}

	Vector3 getOffset (Direction direction) {
		switch (direction) {

		case Direction.North:
			return new Vector3(0, 0, -PlaneSize);

		case Direction.South:
			return new Vector3(0, 0, PlaneSize);

		case Direction.East:
			return new Vector3(-PlaneSize, 0, 0);

		case Direction.West:
			return new Vector3(PlaneSize, 0, 0);

		default:
			return Vector3.zero;
			
		}
	}
}
