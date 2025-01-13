using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

public class BuffSpeed : MonoBehaviour
{
    public PlayerAttack _playerAttackHandler;
    public PlayerMovement _playerMovement;
    public bool buffed = false;

    public void IncreaseFireRate()
    {
        if (_playerAttackHandler.fireRate >= 1)
            _playerAttackHandler.fireRate /= 2;
        StartCoroutine(ReturnFireRate());
    }

    private IEnumerator ReturnFireRate()
    {
        buffed = true;
        yield return new WaitForSeconds(5);
        _playerAttackHandler.fireRate = 1;
        buffed = false;
    }
    public void IncreaseSpeed()
    {
        if (_playerMovement.PlayerSpeed <= 8)
            _playerMovement.PlayerSpeed *= 1.5f;
        StartCoroutine(ReturnSpeed());
    }

    private IEnumerator ReturnSpeed()
    {
        buffed = true;
        yield return new WaitForSeconds(5);
        _playerMovement.PlayerSpeed = 8;
        buffed = false;
    }
}
