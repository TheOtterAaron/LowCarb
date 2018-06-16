using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public LowCarb.Board Board = new LowCarb.Board();

	void Start()
	{
        m_refreshDelta = 0.0f;
    }
	
	void Update()
	{
        m_refreshDelta += Time.deltaTime;

		if (m_refreshDelta >= Constants.BoardRefreshRate)
		{
            Board.StepSimulation();
            m_refreshDelta = 0.0f;
        }
    }

    private float m_refreshDelta;
}
