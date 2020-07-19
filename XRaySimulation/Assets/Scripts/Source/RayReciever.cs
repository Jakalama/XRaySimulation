using System;
using System.Linq;
using UnityEngine;

public class RayReciever : MonoBehaviour
{
    public MeshContainer container;
    private MeshController controller; 

    private void Start()
    {
        controller = new MeshController(container);
    }

    private void Update()
    {
        controller.UpdateRelevantVertices();

        float[] distances = RayTracer.Instance.GetDistances(controller.GetRelevantVerticePositions());
        float[] addedDoses = DoseCalculator.Calculate(distances);

        controller.StoreDoses(addedDoses);
        float[] accumulatedDoses = controller.VerticeData.Select(x => x.Dose).ToArray();

        Color32[] colors = ColorCalculator.Calculate(accumulatedDoses);

        container.ApplyColors(colors);
    }
}
