using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [Header("Camera properties")]
    public float RotationAngleX = 55;
    public float Distance = 20;
    public float OffsetY = 10;

    [SerializeField]
    private Transform _following;

    private void Awake()
    {
        EventManager.PlayerDead.AddListener(DisableCamera);
        EventManager.PlayerRestore.AddListener(EnableCamera);
    }

    private void LateUpdate()
    {
        if (_following == null)
            return;

        Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
        Vector3 followingPosition = _following.position;
        followingPosition.y += OffsetY;
        Vector3 position = rotation * new Vector3(0, 0, -Distance) + followingPosition;

        transform.rotation = rotation;
        transform.position = position;
    }

    public void Follow(GameObject followingObject)
    {
        _following = followingObject.transform;
    }

    private void DisableCamera()
    {
        enabled = false;
    }
    private void EnableCamera()
    {
        enabled = true;
    }
}
