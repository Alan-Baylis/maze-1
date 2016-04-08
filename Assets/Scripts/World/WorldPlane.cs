using UnityEngine;
using System.Collections;

public class WorldPlane : MonoBehaviour {

	WorldBlock[,,] blocks;

	void Start () {
		init();
	}
	
	void spawnBlocks () {

		int width = GameManager.Game.PlaneWidth;
		int length = GameManager.Game.PlaneHeight;
		int height = GameManager.Game.PlaneDepth;


		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				for (int z = 0; z < height; z++) {
					spawnBlock(blocks[x, y, z].Type(), x, y, z);
				}
			}
		}
	}

	void init () {
		blocks = new BlockGenerator().Generate();

		spawnBlocks();
	}

	void spawnBlock (BlockType type, int x, int y, int z) {
		GameObject block;
		if (type == BlockType.Cube) {
			block =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			block.transform.SetParent(transform);
			block.transform.position = getBlockPosition(x, y, z);
		}
	}

	Vector3 getBlockPosition (int x, int y, int z) {
		return new Vector3(
			x - GameManager.Game.PlaneWidth/2, 
			y, 
			z - GameManager.Game.PlaneDepth/2
		);
	}
}
