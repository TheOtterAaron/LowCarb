using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPreviewRenderer : MonoBehaviour
{
    public GameChipFactory gameChipFactory;

    void Start()
    {
        m_rendering = false;
        m_texture = null;
    }

    public void SetupRenderer(int width, int height)
    {
        m_currentWidth = width;
        m_currentHeight = height;

        m_renderTexture = new RenderTexture(
            m_currentWidth,
            m_currentHeight,
            10,
            RenderTextureFormat.ARGB32);

        m_camera = gameObject.GetComponent<Camera>();
        if (m_camera == null)
        {
            m_camera = gameObject.AddComponent<Camera>();
        }

        m_camera.orthographic = true;
        m_camera.orthographicSize = 1;
        m_camera.farClipPlane = 10;
        m_camera.targetTexture = m_renderTexture;
        m_camera.clearFlags = CameraClearFlags.Color;
        m_camera.enabled = false;

        m_rendering = true;
        Camera.onPostRender += SavePreview;
    }

    public bool RenderPreview<C>(out Texture2D destination) where C : LowCarb.IChip, new()
	{
        if (!m_rendering)
        {
            destination = null;
            return false;
        }            

        GameObject chip = gameChipFactory.CreateGameChip<C>(0, 0, false);
        chip.transform.SetParent(transform, false);
        chip.transform.Rotate(Vector3.right, -30.0f, Space.World);
        chip.transform.Rotate(Vector3.up, 30.0f, Space.World);

        Vector3 lChipCenter = chip.GetComponentInChildren<BoxCollider>().center;
        Vector3 wChipCenter = chip.transform.TransformPoint(lChipCenter);
        Vector3 wCameraCenter = m_camera.ScreenToWorldPoint(new Vector3(
            m_currentWidth / 2.0f,
            m_currentHeight / 2.0f,
            (m_camera.nearClipPlane + m_camera.farClipPlane) / 2.0f));
        chip.transform.Translate(wCameraCenter - wChipCenter, Space.World);

        destination = new Texture2D(
                m_currentWidth,
                m_currentHeight,
                TextureFormat.ARGB32,
                false,
                true);

        m_texture = destination;
        m_camera.Render();
        m_texture = null;

        // Destroy doesn't remove objects until the end of the update loop,
        // so we need to move the chip back out of the camera's view
        chip.transform.Translate(Vector3.back * 10, Space.World);
        Destroy(chip);

        return true;
    }

    public void TeardownRenderer()
    {
        if (m_rendering)
        {
            Camera.onPostRender -= SavePreview;
            m_rendering = false;

            Destroy(m_camera);
            m_renderTexture.Release();
        }
    }

    private void SavePreview(Camera camera)
    {
        if (m_rendering)
        {
            if (m_texture != null)
            {
                m_texture.ReadPixels(
                    new Rect(0, 0, m_currentWidth, m_currentHeight), 0, 0, false);
                m_texture.Apply();
            }
        }
    }

    private int m_currentWidth;
    private int m_currentHeight;
    private bool m_rendering;
    private Camera m_camera;
    private RenderTexture m_renderTexture;
    private Texture2D m_texture;
}
