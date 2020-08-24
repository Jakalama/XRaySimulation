﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class FurnitureTriggerInfo
{
    public static FurnitureType Type;

    public static void SetActiveFurniture(FurnitureType type, FurnitureInfo info)
    {
        Type = type;

        SetUI(info);
    }

    private static void SetUI(FurnitureInfo info)
    {
        GameObject infoGameObject = GameObject.Find("GUI/FurnitureInfo");

        if (Type != FurnitureType.None)
        {
            infoGameObject.SetActive(true);

            TextMeshProUGUI name = infoGameObject.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = infoGameObject.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>();

            name.text = info.Name;
            description.text = info.Description;

            SetKeys(info.KeyCodes);
        }
        else
        {
            infoGameObject.SetActive(false);
        }
    }

    private static void SetKeys(KeyCode[] keys)
    {
        GameObject keysObject = GameObject.Find("GUI/FurnitureInfo/Keys");

        if (keys.Length > 2)
            Debug.LogWarning("The GUI can only handle two different keys!");

        int iterationSteps = (keys.Length > 2) ? 2 : keys.Length;
        
        for (int i = 0; i < iterationSteps; i++)
        {
            Transform keyTransform = keysObject.transform.GetChild(i);
            TextMeshProUGUI keyText = keyTransform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

            keyText.text = keys[i].ToString();
        }
    }
}
