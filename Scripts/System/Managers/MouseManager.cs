using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    public Texture2D muzzle;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
    }

    private void Start()
    {
        Cursor.SetCursor(muzzle, new Vector2(8, 8), CursorMode.Auto);
    }
}
