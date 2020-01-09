﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draws the Light bulb icon at position of the object.
        // Because we draw it inside OnDrawGizmos the icon is also pickable
        // in the scene view.

        Gizmos.DrawIcon(transform.position, "ikGoal", true);
    }
}
