using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class AttackButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerAttack _playerAttackHandler;
    private bool _pressed;

    [Inject]
    public void Construct(PlayerAttack playerAttackHandler)
    {
        _playerAttackHandler = playerAttackHandler;
    }

    private void Update()
    {
        if (_pressed)
            _playerAttackHandler.OnAttack();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }
}
