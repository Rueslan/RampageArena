using System;
using UnityEngine;

public interface IJoystickForMovement
{
    event Action<Vector2> OnJoystickDrag;
}