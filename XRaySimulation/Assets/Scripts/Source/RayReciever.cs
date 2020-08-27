using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RayReciever : MonoBehaviour
{
    private MeshContainer container;
    private MeshController controller;

    private void Start()
    {
        container = new MeshContainer(this.transform);
        controller = new MeshController(container, this.transform);
    }

    private void FixedUpdate()
    {
        GetAndApplyDose();
    }

    private void GetAndApplyDose()
    {
        if (RaySource.Instance == null)
            return;

        controller.UpdateVertices(RaySource.Instance.transform.position);
        controller.SortOutUnhittedVertices(RaySource.Instance.RayTracer);

        float[] distances = RaySource.Instance.RayTracer.GetDistances(controller.GetRelevantVerticePositions());
        float[] addedDoses = DoseCalculator.Calculate(distances);

        controller.StoreDoses(addedDoses);

        float[] accumulatedDoses = controller.VerticeData.Select(x => x.Dose).ToArray();
        Color32[] colors = ColorCalculator.Calculate(accumulatedDoses);

        container.ApplyColors(colors);
    }
}