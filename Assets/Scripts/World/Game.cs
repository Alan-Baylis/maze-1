using UnityEngine;
using System.Collections;

public class Game {
	public int PlaneWidth;
	public int PlaneHeight;
	public int PlaneDepth;

	public int PathWidth;

	public float MaxDistance;

	public float BlockScale;

	public int CoinsCollected = 0;
	public int CoinsToWin = 30;

	public Game (int x, int y, int z, int pathWidth, float maxDistance, float blockScale, int coinsToWin) {
		this.PlaneWidth = x;
		this.PlaneHeight = y;
		this.PlaneDepth = z;
		this.PathWidth = pathWidth;
		this.MaxDistance = maxDistance;
		this.BlockScale = blockScale;
		this.CoinsToWin = coinsToWin;
	}

	public float PercentToVictory () {
		return (float) CoinsCollected / (float) CoinsToWin;
	}

	public void Reset () {
		CoinsCollected = 0;
	}

	public int MaxDimension (Axis axis) {
		switch (axis) {

		case Axis.X:
			return PlaneWidth;

		case Axis.Y:
			return PlaneHeight;

		case Axis.Z:
			return PlaneDepth;

		default: return 0;
			
		}
	}
}
