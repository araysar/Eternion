using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract Vector3 MoveDirection();
    public abstract Vector3 LookRotation();
}
