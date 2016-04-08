using UnityEngine;
using System.Collections;

public class BlockGenerator {

	public virtual WorldBlock[,,] Generate (params object [] args) {
		int width = GameManager.Game.PlaneWidth;
		int length = GameManager.Game.PlaneHeight;
		int height = GameManager.Game.PlaneDepth;

		WorldBlock[,,] blocks = new WorldBlock[width, length, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < height; z++) {

					blocks[x,y,z] = GetBlock();

				}
			}
		}

		CutPath(blocks, GameManager.Game.PathWidth);

		return blocks;
	}

	protected virtual void CutPath (WorldBlock[,,] blocks, int width, int? startBlock = null) {
		if (startBlock == null) {
			startBlock = GameManager.Game.PlaneWidth/2;
		}

		int minX = clamp(Axis.X, (int) startBlock - width/2);

		int maxX = clamp(Axis.X, (int) startBlock + width/2);


		for (int x = minX; x < maxX; x++) {
			for (int z = 0; z < blocks.GetLength(2); z++) {
				CutColumn(blocks, x, z);
			}
		}
	}

	protected int clamp (Axis axis, int value) {
		return Mathf.Clamp(value, 0, GameManager.Game.MaxDimension(axis));
	}

	protected virtual void CutColumn (WorldBlock[,,] blocks, int x, int z) {
		for (int y = 0; y < blocks.GetLength(1); y++) {
			blocks[x,y,z].SetType(BlockType.Empty);
		}
	}

	public virtual WorldBlock GetBlock (params object [] args) {
		BlockType blockType;

		if (Random.Range(0, 10) < 2) {
			blockType = BlockType.Cube;	
		} else {
			blockType = BlockType.Empty;
		}

		return new WorldBlock (
			blockType
		);
	}
}
