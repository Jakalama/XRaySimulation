using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RayReciever : MonoBehaviour
{
    private MeshContainer container;
    private MeshController controller;

    private bool isActive;

    private void Start()
    {
        isActive = false;

        container = new MeshContainer(this.transform);
        controller = new MeshController(container, this.transform);
    }

    private void FixedUpdate()
    {
        if (isActive)
            GetAndApplyDose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isActive = !isActive;
            DoseInfo.Instance.Controller.SetSourceActiveText(isActive);
        }
            
    }

    private void GetAndApplyDose()
    {
        if (RaySource.Instance == null)
            return;

        controller.UpdateVertices(RaySource.Instance.transform.position);
        controller.SortOutUnhittedVertices(RaySource.Instance.RayTracer);

        float[] distances = RaySource.Instance.RayTracer.GetDistances(controller.GetRelevantVerticePositions());
        float[] addedDoses = DoseCalculator.Calculate(distances, RaySource.Instance.BaseEnergy, Time.deltaTime);

        controller.StoreDoses(addedDoses);

        float[] accumulatedDoses = controller.VerticeData.Select(x => x.Dose).ToArray();

        Debug.Log(accumulatedDoses.Length);

        float avgDose = DoseCalculator.GetAVGDose(accumulatedDoses);
        Color32[] colors = ColorCalculator.Calculate(accumulatedDoses);

        container.ApplyColors(colors);
        DoseInfo.Instance.Controller.SetAVGDose(avgDose);
    }
}