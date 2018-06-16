using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRenderer : MonoBehaviour
{
    public GameObject wirePrefab;

    void Start ()
	{
        m_requestedRenders = new List<WireDesc>();
        m_rendererPool = new List<GameObject>();
    }

    public void RequestWireRender(Vector3 start, Vector3 end, Color color)
    {
        WireDesc desc = new WireDesc();
        desc.start = start;
        desc.end = end;
        desc.color = color;

        m_requestedRenders.Add(desc);
    }

    void LateUpdate ()
	{
        int i = 0;
        while (i < m_requestedRenders.Count)
        {
            if (i >= m_rendererPool.Count)
            {
                GameObject wireRenderer = Instantiate<GameObject>(wirePrefab);
                m_rendererPool.Add(wireRenderer);
            }

            LineRenderer lineRenderer = m_rendererPool[i].GetComponent<LineRenderer>();
            WireDesc desc = m_requestedRenders[i];

            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] {
				desc.start,
				desc.end
            });
            lineRenderer.startColor = desc.color;
            lineRenderer.endColor = desc.color;

            i++;
        }

        while (i < m_rendererPool.Count)
		{
            m_rendererPool[i].GetComponent<LineRenderer>().enabled = false;
            i++;
        }

		m_requestedRenders.Clear();
    }

	protected struct WireDesc
	{
        public Vector3 start;
		public Vector3 end;
        public Color color;
    }

    private List<WireDesc> m_requestedRenders;
    private List<GameObject> m_rendererPool;
}
