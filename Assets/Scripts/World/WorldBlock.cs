using UnityEngine;
using System.Collections;

public class WorldBlock {
	BlockType _type;
	Color _color = Color.white;

	public Color Color {
		get {
			return _color;
		}

		set {
			_color = value;
		}
	}

	// Use this for initialization
	public WorldBlock (BlockType type, Color color) {
		this._type = type;
		this._color = color;
	}

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
