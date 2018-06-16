using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameChip : MonoBehaviour
{
    public void Start()
    {
        m_validClick = false;
        m_seated = true;
        m_yVelocity = Vector3.zero;
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            m_validClick = true;
        }
    }

    public void OnMouseExit()
    {
        m_validClick = false;
    }

    public void OnMouseUp()
    {
        if (m_validClick && !BoardCamera.main.Jittered())
        {
            if (m_seated)
            {
                m_seated = false;
            }
        }
        m_validClick = false;
    }

    public void Update()
    {
        if (!m_seated)
        {
            Vector3 mouseOnBoard = BoardCamera.main.GetMouseOnBoard();
            transform.position = Vector3.SmoothDamp(
                transform.position,
                new Vector3(
                    mouseOnBoard.x,
                    Constants.UnseatedChipHeight,
                    mouseOnBoard.z),
                ref m_yVelocity,
                0.1f
            );

            if (Input.GetMouseButtonDown(0))
            {
                m_boardPosition = BoardPosition.FromWorld(mouseOnBoard);
                m_seated = true;
            }
        }
        else
        {
            Vector3 boardWorldPos = m_boardPosition.ToWorld();

            transform.position = Vector3.SmoothDamp(
                transform.position,
                new Vector3(
                    boardWorldPos.x,
                    0.0f,
                    boardWorldPos.z),
                ref m_yVelocity,
                0.05f);
        }
    }

    public uint Handle { get; set; }

    public int Segments { get; set; }

    public GameObject Mesh
    {
        get
        {
            return m_mesh;
        }
        set
        {
            ChipMeshBuilder builder = value.GetComponent<ChipMeshBuilder>();
            if (builder)
            {
                m_mesh = value;
                m_mesh.transform.SetParent(transform, true);
                m_mesh.transform.position = transform.position;
            }
        }
    }

    public BoardPosition position
    {
        get
        {
            return m_boardPosition;
        }
        set
        {
            m_boardPosition = value;
            transform.position = m_boardPosition.ToWorld();
        }
    }

    private GameObject m_mesh;
    private BoardPosition m_boardPosition;

    private bool m_validClick;
    private bool m_seated;
    private Vector3 m_yVelocity;
}
