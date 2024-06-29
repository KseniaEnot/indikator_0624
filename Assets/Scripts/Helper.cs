using System;
using Infrastructure;
using UnityEngine;

public static class Helper
{

    public static Vector2 WorldMousePosition()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
}
