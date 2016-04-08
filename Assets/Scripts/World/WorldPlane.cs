using UnityEngine;
using System.Collections;

public class WorldPlane : MonoBehaviour {
	public bool ShouldSpawnBlocks = true;

	public WorldPlane North;
	public WorldPlane South;
	public WorldPlane East;
	public WorldPlane West;

	WorldBlock[,,] blocks;

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

	void init () {
		blocks = new BlockGenerator().Generate();

		if (ShouldSpawnBlocks) {
			spawnBlocks();
		}
	}

	void spawnBlock (BlockType type, int x, int y, int z) {
		GameObject block;
		if (type == BlockType.Cube) {
			block =  GameObject.CreatePrimitive(PrimitiveType.Cube);
			block.transform.SetParent(transform);
			block.transform.localScale *= GameManager.Game.BlockScale;
			block.transform.localPosition = getBlockPosition(x, y, z);
			block.GetComponent<Renderer>().material.color = color(block.transform.position);
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

			Debug.Log("Collided with " + gameObject.name);
		}
	}
}
