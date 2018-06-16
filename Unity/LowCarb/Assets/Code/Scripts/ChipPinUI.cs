using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ChipPinUI : MonoBehaviour
{
    public WireRenderer wireRenderer;
    public GameChip owningChip;
    public GameBoard owningBoard;
    public LowCarb.PinHandle pinHandle;

    private static ChipPinUI LastPin;

    void Start()
	{
        LastPin = null;
        m_source = null;

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate
        {
			if (LastPin == null)
			{
                LastPin = this;
            }
			else
			{
                ChipPinUI src;
                ChipPinUI dst;

                if (LastPin.pinHandle.type == LowCarb.EPinType.Output)
				{
                    src = LastPin;
                    dst = this;
                }
				else
				{
                    src = this;
                    dst = LastPin;
                }

                bool success = true;
                try
				{
                    owningBoard.Board.ConnectPins(src.pinHandle, dst.pinHandle);
                }
				catch (Exception e)
				{
                    success = false;
                    Debug.Log(e.Message);
                }

				if (success)
				{
                    LastPin = null;
                    dst.m_source = src;
                }
			}
        });
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetMouseButtonUp(0))
        {
            LastPin = null;
        }
    }

    void LateUpdate()
	{
        if (owningChip != null)
        {
            Vector3 worldPos = CalculateWorldPosition();
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            RectTransform rect = GetComponent<RectTransform>();
            rect.position = screenPos;

            Image btnImg = GetComponent<Image>();
            btnImg.color = DetermineColor();
			
			if (LastPin == this)
			{
                wireRenderer.RequestWireRender(
                    worldPos,
                    BoardCamera.main.GetMouseOnBoard(),
                    DetermineColor());
            }

            if (m_source != null)
            {
                wireRenderer.RequestWireRender(
                    worldPos,
                    m_source.CalculateWorldPosition(),
                    DetermineColor()
                );
            }
        }
    }

    private Vector3 CalculateWorldPosition()
    {
        Vector3 worldPos = owningChip.transform.position;
        
        worldPos.y += Constants.PinUIHeight;

        if (pinHandle.type == LowCarb.EPinType.Input)
        {
            worldPos.x += (float)pinHandle.index * Constants.PinDistance;
        }
        else if (pinHandle.type == LowCarb.EPinType.Output)
        {
            worldPos.x += (float)(owningChip.Segments - pinHandle.index - 1) * Constants.PinDistance;
            worldPos.z += Constants.PinDistance * 3.0f;
        }

        return worldPos;
    }

	private Color DetermineColor()
	{
		switch (owningBoard.Board.GetPin(pinHandle).Signal)
		{
			case LowCarb.ESignal.Low:
                return Constants.LowCarbBlue;
            case LowCarb.ESignal.High:
                return Constants.LowCarbGold;
            default:
                return Constants.LowCarbClay;
        }
	}

    private ChipPinUI m_source;
}
