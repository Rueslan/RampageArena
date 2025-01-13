using System.Collections.Generic;
using UnityEngine;

public static class PlayersList
{
    public static List<GameObject> _playersOnline = new List<GameObject>();

    public static void AddPlayer(GameObject player)
    {
        if (!_playersOnline.Contains(player))
        {
            _playersOnline.Add(player);
        }
    }

    public static void RemovePlayer(GameObject player)
    {
        _playersOnline.Remove(player);
    }

    public static void ClearList()
    {
        _playersOnline.Clear();
    }
}
