using UnityEngine;
using System.Collections;

public class WorldBlock {
	BlockType _type;

	// Use this for initialization
	public WorldBlock (BlockType type) {
		this._type = type;
	}

	public BlockType Type () {
		return _type;
	}

	public void SetType (BlockType type) {
		this._type = type;
	}
}
