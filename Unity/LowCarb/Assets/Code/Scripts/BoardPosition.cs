using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition
{
    public uint x;
    public uint y;

	public BoardPosition(uint x = 0, uint y = 0)
	{
        this.x = x;
        this.y = y;
    }

	public Vector3 ToWorld()
	{
        return new Vector3(
			(float)x * Constants.PinDistance,
			0.0f,
			(float)y * Constants.PinDistance);
    }

	public static BoardPosition FromWorld(Vector3 worldPos)
	{
        return new BoardPosition(
            (uint)Mathf.RoundToInt(Mathf.Max(worldPos.x / Constants.PinDistance, 0)),
            (uint)Mathf.RoundToInt(Mathf.Max(worldPos.z / Constants.PinDistance, 0)));
    }
}
