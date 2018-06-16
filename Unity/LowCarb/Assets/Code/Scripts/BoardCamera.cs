using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class BoardCamera : MonoBehaviour
{
    public int DollyNear = 2;
    public int DollyFar = -4;
    public float DollyStepLength = 3.0f;
    public float DollySpeed = 35.0f;
    public float DollySnapping = 0.01f;

    public float JitterThreshold = 0.5f;

    void Start()
	{
        m_camera = gameObject.GetComponent<Camera>();
        if (Camera.main == m_camera)
        {
            main = this;
        }

        m_currentDolly = 0.0f;
        m_targetDolly = 0.0f;

        m_validMouseDown = false;
    }

	void Update()
	{
        bool overUI = false;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            overUI = true;
        }

        if (!overUI)
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (scrollDelta != 0)
            {
                float dollyMin = (float)DollyFar * DollyStepLength;
                float dollyMax = (float)DollyNear * DollyStepLength;

                m_targetDolly += scrollDelta * DollyStepLength;
                m_targetDolly = Mathf.Min(Mathf.Max(m_targetDolly, dollyMin), dollyMax);
            }

            float dollyDelta = m_targetDolly - m_currentDolly;
            if (Mathf.Abs(dollyDelta) > 0.0f)
            {
                float dollyMagnitude = Mathf.Abs(dollyDelta / DollyStepLength);

                float forwardDelta = DollySpeed * dollyMagnitude * Time.deltaTime;
                forwardDelta = Mathf.Min(Mathf.Max(forwardDelta, DollySnapping), Mathf.Abs(dollyDelta));
                forwardDelta *= Mathf.Sign(dollyDelta);

                transform.Translate(Vector3.forward * forwardDelta, Space.Self);
                m_currentDolly += forwardDelta;
            }
        }

        if (Input.GetMouseButtonDown(0) && !overUI)
        {
            m_validMouseDown = true;
            m_mouseDownOnBoard = GetMouseOnBoard();
            m_jitter = 0.0f;
        }
        else if (Input.GetMouseButton(0) && m_validMouseDown)
        {
            Vector3 mouseOnBoard = GetMouseOnBoard();
            Vector3 offset = new Vector3(
                m_mouseDownOnBoard.x - mouseOnBoard.x,
                0,
                m_mouseDownOnBoard.z - mouseOnBoard.z);

            if (m_jitter > JitterThreshold)
            {
                transform.Translate(offset, Space.World);
            }

            m_jitter += offset.magnitude;
        }
        else if (Input.GetMouseButtonUp(0) && m_validMouseDown)
        {
            m_validMouseDown = false;
        }
    }

    public Vector3 GetMouseOnBoard()
    {
        Ray mouseRay = m_camera.ScreenPointToRay(Input.mousePosition);
        float cosine = Vector3.Dot(Vector3.down, mouseRay.direction);
        float distanceToBoard = transform.position.y / cosine;
        return transform.position + mouseRay.direction * distanceToBoard;
    }

    public bool Jittered()
    {
        return m_jitter > JitterThreshold;
    }

    public static BoardCamera main { get; protected set; }

    private Camera m_camera;
    private Vector3 m_mouseDownOnBoard;

    private float m_currentDolly;
    private float m_targetDolly;

    private bool m_validMouseDown;
    private float m_jitter;
}
