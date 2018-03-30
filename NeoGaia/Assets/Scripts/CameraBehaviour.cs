﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public static readonly Vector3 minResolution = new Vector3(1024f, 576f, 0);
    private float targetRatio = 16f / 9f;

    private static CameraBehaviour instance = null;
    private Camera _camera;
    private int _width;
    private int _height;

    #region Accessors
    // Width of the camera
    public int Width
    {
        get
        {
            return _width;
        }

        set
        {
            if (value != _width)
            {
                _width = value;
                AdjustScreenScale();
            }
        }
    }

    // Height of the camera
    public int Height
    {
        get
        {
            return _height;
        }

        set
        {
            if (value != _height)
            {
                _height = value;
                AdjustScreenScale();
            }
        }
    }


    #endregion

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _camera = GetComponent<Camera>();
        DontDestroyOnLoad(_camera);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Width = Screen.width;
        Height = Screen.height;
    }

    /// <summary>
    /// Computes the new scale of the screen
    /// </summary>
    private void AdjustScreenScale()
    {
        // determine the game window's current aspect ratio
        float windowaspect = Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetRatio;


        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = _camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            _camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = _camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            _camera.rect = rect;
        }
    }
}
