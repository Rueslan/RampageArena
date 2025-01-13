using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<int> CoinPickedUp = new();
    public static UnityEvent<int> DamagePackPickedUp = new();
    public static UnityEvent<int> MedicineChestPickedUp = new();

    public static UnityEvent GamePaused = new();
    public static UnityEvent GameResumed = new();

    public static UnityEvent<NavMeshAgent> EnemyAttacks = new();
    public static UnityEvent<GameObject> TargetFound = new();
    public static UnityEvent<GameObject> TargetLost = new();
    public static UnityEvent<GameObject, GameObject> EnemyDead = new();

    public static UnityEvent QuestWindowClosed = new();

    public static UnityEvent<GameObject, int> PlayerReceiveGold = new();
    public static UnityEvent PlayerShoot = new();

    public static UnityEvent PlayerDead = new();
    public static UnityEvent PlayerRestore = new();
    public static UnityEvent PlayerWin = new();

    public static UnityEvent PlayerHeals = new();
    public static UnityEvent<float> PlayerLoseControl = new();

    public static UnityEvent AmmoOver = new();
    public static UnityEvent<int> AmmoPickedUp = new();

    public static void CallQuestWindowClosed()
    {
        QuestWindowClosed?.Invoke();
    }

    public static void CallTargetLost(GameObject sender)
    {
        TargetLost?.Invoke(sender);
    }
    public static void CallTargetFound(GameObject sender)
    {
        TargetFound?.Invoke(sender);
    }

    public static void CallPlayerLoseControl(float seconds)
    {
        PlayerLoseControl?.Invoke(seconds);
    }

    public static void CallPlayerHeals()
    {
        PlayerHeals?.Invoke();
    }

    public static void CallAmmoOver()
    {
        AmmoOver?.Invoke();
    }

    public static void CallAmmoPickedUp(int ammoAmount)
    {
        AmmoPickedUp?.Invoke(ammoAmount);
    }

    public static void CallCoinPickedUp(int reward)
    {
        CoinPickedUp?.Invoke(reward);
    }

    public static void CallMedecineChestPickedUp(int healValue)
    {
        MedicineChestPickedUp?.Invoke(healValue);
    }

    public static void CallDamagePackPickedUp(int healValue)
    {
        DamagePackPickedUp?.Invoke(healValue);
    }

    public static void CallEnemyAttacks(NavMeshAgent agent)
    {
        EnemyAttacks?.Invoke(agent);
    }

    public static void CallPlayerReceiveGold(GameObject sender, int goldAmount)
    {
        PlayerReceiveGold?.Invoke(sender, goldAmount);
    }

    public static void CallPlayerShoot()
    {
        PlayerShoot?.Invoke();
    }

    public static void CallGamePaused()
    {
        GamePaused?.Invoke();
    }

    public static void CallGameResumed()
    {
        GameResumed?.Invoke();
    }

    public static void CallPlayerDead()
    {
        PlayerDead?.Invoke();
    }

    public static void CallPlayerRestore()
    {
        PlayerRestore?.Invoke();
    }

    public static void CallPlayerWin()
    {
        PlayerWin?.Invoke();
    }

    public static void CallEnemyDead(GameObject killer, GameObject victum)
    {
        EnemyDead?.Invoke(killer, victum);
    }

}
