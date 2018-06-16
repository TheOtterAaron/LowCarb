using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMeshBuilder : MonoBehaviour
{
	public GameObject[] SegmentPrefabs;
    public Material[] LabelMaterials;

    public Bounds Build(uint segmentCount, uint labelIndex)
	{
        Material chipMaterial = SelectMaterial(labelIndex);
        Vector3 chipMinBounds = Vector3.zero;
        Vector3 chipMaxBounds = Vector3.zero;

        for (uint iSegment = 0; iSegment < segmentCount; iSegment++)
		{
            GameObject segmentPrefab = SelectSegmentPrefab(segmentCount, iSegment);
            GameObject segment = InstantiateSegment(segmentPrefab, iSegment);

            Vector2 uvOffset = CalculateUVOffset(labelIndex, iSegment);
            OffsetSegmentUVs(segment, uvOffset);

            MeshRenderer[] segmentMeshes = segment.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in segmentMeshes)
            {
                meshRenderer.material = chipMaterial;
                chipMinBounds = Vector3.Min(meshRenderer.bounds.min, chipMinBounds);
                chipMaxBounds = Vector3.Max(meshRenderer.bounds.max, chipMaxBounds);
            }
        }

        Bounds bounds = new Bounds();
        bounds.center = (chipMaxBounds + chipMinBounds) / 2;
        bounds.size = chipMaxBounds - chipMinBounds;

        return bounds;
    }

    private Material SelectMaterial(uint labelIndex)
    {
        return LabelMaterials[Mathf.FloorToInt((float)labelIndex / 6.0f)];
    }

    private GameObject SelectSegmentPrefab(uint segmentCount, uint segmentIndex)
    {
        if (segmentIndex == 0)
        {
            return SegmentPrefabs[0];
        }
        else if (segmentIndex < segmentCount - 1)
        {
            return SegmentPrefabs[1];
        }
        else
        {
            return SegmentPrefabs[2];
        }
    }

    private GameObject InstantiateSegment(GameObject segmentPrefab, uint segmentIndex)
    {
        Vector3 translation = CalculateTranslation(segmentIndex);

        GameObject segment = Instantiate<GameObject>
        (
            segmentPrefab,
            translation,
            Quaternion.identity
        );
        segment.transform.SetParent(transform, false);

        return segment;
    }

    private Vector3 CalculateTranslation(uint segmentIndex)
    {
        return new Vector3(segmentIndex * Constants.PinDistance, 0, 0);
    }

    private Vector2 CalculateUVOffset(uint labelIndex, uint segmentIndex)
    {
        Vector2 offset = Vector2.zero;

        if (segmentIndex > 0)
        {
            if (segmentIndex < 6)
            {
                offset.x += (segmentIndex - 1) * 0.1f;
            }
            else
            {
                offset.x += -1.0f;
            }
        }

        uint labelIndexInMaterial = labelIndex % 6;
        offset.x += Mathf.Floor((float)labelIndexInMaterial / 3) * 0.5f;
        offset.y += (labelIndexInMaterial % 3) * -0.3f;

        return offset;
    }

    private void OffsetSegmentUVs(GameObject segment, Vector2 offset)
    {
        MeshFilter meshFilter = segment.GetComponentsInChildren<MeshFilter>()[0];

        List<Vector2> uvs = new List<Vector2>();
        Mesh mesh = meshFilter.mesh;
        mesh.GetUVs(0, uvs);

        List<Vector2> newUVs = new List<Vector2>();
        foreach (Vector2 uv in uvs)
        {
            newUVs.Add(uv + offset);
        }

        mesh.uv = newUVs.ToArray();
    }
}
