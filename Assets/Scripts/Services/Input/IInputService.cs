using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

public interface IInputService : IService
{
    Vector2 Axis { get; }

    bool isAttackButtonUp();
}
