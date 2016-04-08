using UnityEngine;
using System.Collections;

public class WorldPlane : MonoBehaviour {

	WorldBlock[,,] blocks;

	void Start () {
		init();
	}
	
	void spawnBlocks () {

		int width = GameManager.CurrentGame().PlaneWidth;
		int length = GameManager.CurrentGame().PlaneLength;
		int height = GameManager.CurrentGame().PlaneHeight;


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
			block.transform.localPosition = new Vector3(x, y, z);
		}
	}
}
