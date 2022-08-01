using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int width;
    public int height;

    public void SetWidth(int w)
    {
        width = w;
    }

    public void SetHeight(int h)
    {
        height = h;
    }

    public void SetRes()
    {
        Screen.SetResolution(width, height, false);
    }
}
