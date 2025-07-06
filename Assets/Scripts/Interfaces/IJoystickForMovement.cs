using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IJoystickForMovement
    {
        event Action<Vector2> OnJoystickDrag;
    }
}