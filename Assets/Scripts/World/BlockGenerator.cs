using UnityEngine;
using System.Collections;

public class BlockGenerator {

	public virtual WorldBlock[,,] Generate (params object [] args) {
		int width = GameManager.CurrentGame().PlaneWidth;
		int length = GameManager.CurrentGame().PlaneLength;
		int height = GameManager.CurrentGame().PlaneHeight;

		WorldBlock[,,] blocks = new WorldBlock[width, length, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < height; z++) {

					blocks[x,y,z] = GetBlock();

				}
			}
		}

		return blocks;
	}

	public virtual WorldBlock GetBlock (params object [] args) {
		return new WorldBlock (
			(BlockType) Random.Range(0, 2)
		);
	}
}
