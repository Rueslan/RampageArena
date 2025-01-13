using System.Collections;
using UnityEngine;

public abstract class AttackHandlerBase : MonoBehaviour
{
    public Weapon weapon;
    public float fireRate = 1;
    protected bool allowAttack = true;

    protected Vector3 weaponStartPosition;
    protected readonly float offsetY = 1.2f;
    protected readonly float offsetZ = 1.5f;

    public virtual void Attack()
    {
        if (allowAttack && weapon is not null)
            StartCoroutine(AttackCoroutine());
    }

    protected IEnumerator AttackCoroutine()
    {
        allowAttack = false;
        SoundManager.instance.PlaySound(SoundManager.audioClip.Throw);

        weaponStartPosition = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z) + transform.forward * offsetZ;
        Instantiate(weapon, weaponStartPosition, Quaternion.Euler(new Vector3(0, 0, 90)), transform)
            .With(w => w.transform.localScale = Vector3.one * 3)
            .With(w => w.owner = transform.gameObject);

        yield return new WaitForSeconds(fireRate);
        allowAttack = true;
    }
}
