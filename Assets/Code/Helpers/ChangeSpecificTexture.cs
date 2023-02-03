using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSpecificTexture : MonoBehaviour
{
    public List<string> materialNames;
    public bool generateOnStart = true;
    public List<Material> materials = new List<Material>();
    public Color color;

    public void Start()
    {
        if (generateOnStart)
        {
            ApplyColor(color);
        }
    }


    public void ApplyColor(Color color)
    {
        Renderer[] renderers = this.gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                foreach (string materialName in materialNames)
                {
                    if (materialName + " (Instance)" == renderer.materials[i].name)
                    {
                        materials.Add(renderer.materials[i]);
                    }
                }
            }
        }

        foreach (Material mat in materials)
        {
            mat.color = color;
        }

        this.enabled = false;
    }

}



