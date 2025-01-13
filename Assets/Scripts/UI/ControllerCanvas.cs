using UnityEngine;

public class ControllerCanvas : MonoBehaviour
{
    public static ControllerCanvas instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this);
    }
}
