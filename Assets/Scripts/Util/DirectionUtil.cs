using UnityEngine;
using System.Collections;

public static class DirectionUtil {

	public static Direction Reverse (Direction direction) {
		switch (direction) {

		case Direction.North:
			return Direction.South;

		case Direction.South:
			return Direction.North;

		case Direction.East:
			return Direction.West;

		case Direction.West:
			return Direction.East;

		default:
			return Direction.North;

		}
	}

	public static Direction [] Perpendiculars (Direction direction) {
		switch (direction) {

		case Direction.North:
			return new Direction[]{Direction.West, Direction.East};

		case Direction.South:
			return new Direction[]{Direction.West, Direction.East};

		case Direction.East:
			return new Direction[]{Direction.North, Direction.South};

		case Direction.West:
			return new Direction[]{Direction.North, Direction.South};

		default:
			return new Direction[0];

		}
	}

}
