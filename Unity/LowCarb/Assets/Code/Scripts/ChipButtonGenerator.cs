using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChipButtonGenerator : MonoBehaviour
{
    public Button buttonPrefab;
    public ChipPreviewRenderer previewRenderer;
    public GameChipFactory factory;

    void Start()
	{
        previewRenderer.SetupRenderer(128, 128);

        GenerateButton<LowCarb.BuiltIns.NotChip>("NOT");
        GenerateButton<LowCarb.BuiltIns.AndChip>("AND");
        GenerateButton<LowCarb.BuiltIns.NandChip>("NAND");
        GenerateButton<LowCarb.BuiltIns.OrChip>("OR");
        GenerateButton<LowCarb.BuiltIns.XorChip>("XOR");
        GenerateButton<LowCarb.BuiltIns.MuxChip>("MUX");
        GenerateButton<LowCarb.BuiltIns.DmuxChip>("DMUX");
        GenerateButton<LowCarb.BuiltIns.ConstLowChip>("LO");
        GenerateButton<LowCarb.BuiltIns.ConstHighChip>("HI");
        GenerateButton<LowCarb.BuiltIns.ForwardChip>("FWD");

        previewRenderer.TeardownRenderer();
    }
	
    private void GenerateButton<C>(string label) where C : LowCarb.IChip, new()
    {
        Texture2D preview;
        if (previewRenderer.RenderPreview<C>(out preview))
        {
            Button button = Instantiate<Button>(buttonPrefab);
            button.transform.SetParent(transform);
            button.GetComponentInChildren<RawImage>().texture = preview;
            button.GetComponentInChildren<Text>().text = label;

            button.onClick.AddListener(delegate { factory.CreateGameChip<C>(); });
        }
    }
}
