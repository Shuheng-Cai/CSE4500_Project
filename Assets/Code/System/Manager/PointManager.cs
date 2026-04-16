using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    private int points;

    public Action<float> OnPointsChanged;
    public int Points
    {
        get { return points; }
        private set { points = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OnPointsChanged += AddPoints;
    }

    private void OnDisable()
    {
        OnPointsChanged -= AddPoints;
    }

    public void AddPoints(float amount)
    {
        Points += (int)amount;
    }



}
