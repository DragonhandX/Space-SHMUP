﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy {
    [Header("Set in Inspector: Enemy_2")]
    // Determines how much the Sine wave will affect movement
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    [Header("Set Dynamically: Enemy_2")]
    // Enemy_2 uses a Sin wave to modify a 2-point linear interpolation
    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;

	// Use this for initialization
	void Start () {
        // Pick any point on the left side of the screen
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        // Pick any point on the right side of the screen
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(-bndCheck.camHeight, bndCheck.camHeight);

        // possibly swap sides
        if (Random.value > 0.5f)
        {
            // Setting the .x of each point to its negative will move it to the other side of the screen
            p0.x *= -1;
            p1.x *= -1;
        }

        birthTime = Time.time;
	}

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        pos = (1 - u) * p0 + u * p1;
    }
}
