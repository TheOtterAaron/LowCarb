using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameChipFactory : MonoBehaviour
{
    public GameObject ChipMeshPrefab;
    public GameObject Breadboard;
    public WireRenderer wireRenderer;
    public Button pinBtnPrefab;
    public Canvas canvas;

    public GameObject CreateGameChip<C>(
        uint boardX = 0,
        uint boardY = 0,
        bool makeUI = true) where C : LowCarb.IChip, new()
    {
        GameBoard gameBoard = Breadboard.GetComponent<GameBoard>();
        if (gameBoard == null)
        {
            return null;
        }

        GameObject chipObject = new GameObject(typeof(C).Name);
        GameChip gameChip = chipObject.AddComponent<GameChip>();

        uint hChip = gameBoard.Board.AddChip<C>();
        gameChip.Handle = hChip;

        GameObject chipMesh = GameObject.Instantiate<GameObject>(
            ChipMeshPrefab,
            Vector3.zero,
            Quaternion.identity
        );
        gameChip.Mesh = chipMesh;

        uint segmentCount = CalculateSegmentCount(gameBoard.Board.GetChip(hChip));
        Bounds meshBounds = gameChip.Mesh.GetComponent<ChipMeshBuilder>().Build(
            segmentCount,
            m_labelIndexFromChipType[typeof(C)]
        );
        gameChip.Segments = (int)segmentCount;

        BoxCollider collider = gameChip.gameObject.AddComponent<BoxCollider>();
        collider.center = meshBounds.center;
        collider.size = meshBounds.size;

        if (makeUI)
        {
            int numInputPins = gameBoard.Board.GetChip(hChip).GetInputPins().Count;
            int numOutputPins = gameBoard.Board.GetChip(hChip).GetOutputPins().Count;

            for (int i = 0; i < numInputPins + numOutputPins; i++)
            {
                LowCarb.PinHandle hPin = new LowCarb.PinHandle(
                    hChip,
                    i < numInputPins ? i : i - numInputPins,
                    i < numInputPins ? LowCarb.EPinType.Input : LowCarb.EPinType.Output);

                Button pinBtn = Instantiate<Button>(
                    pinBtnPrefab,
                    Vector3.zero,
                    Quaternion.identity);
                pinBtn.transform.SetParent(canvas.transform);
                pinBtn.transform.SetAsFirstSibling();

                ChipPinUI pinUI = pinBtn.GetComponent<ChipPinUI>();
                pinUI.wireRenderer = wireRenderer;
                pinUI.owningChip = gameChip;
                pinUI.owningBoard = gameBoard;
                pinUI.pinHandle = hPin;

                Text pinText = pinBtn.GetComponentInChildren<Text>();
                pinText.text = gameBoard.Board.GetPin(hPin).Label;
            }
        }

        gameChip.position = new BoardPosition(boardX, boardY);

        return chipObject;
    }

    private uint CalculateSegmentCount(LowCarb.IChip chip)
    {
        return (uint)Math.Max(
            chip.GetInputPins().Count + 2,
            chip.GetOutputPins().Count + 2);
    }

    private Dictionary<Type, uint> m_labelIndexFromChipType = new Dictionary<Type, uint>()
    {
        { typeof(LowCarb.BuiltIns.AndChip), 0 },
        { typeof(LowCarb.BuiltIns.OrChip), 1 },
        { typeof(LowCarb.BuiltIns.XorChip), 2 },
        { typeof(LowCarb.BuiltIns.NotChip), 3 },
        { typeof(LowCarb.BuiltIns.MuxChip), 4 },
        { typeof(LowCarb.BuiltIns.DmuxChip), 5 },
        { typeof(LowCarb.BuiltIns.NandChip), 6},
        { typeof(LowCarb.BuiltIns.ConstHighChip), 7},
        { typeof(LowCarb.BuiltIns.ConstLowChip), 8},
        { typeof(LowCarb.BuiltIns.ForwardChip), 9 }
    };
}
