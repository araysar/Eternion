using UnityEngine;
using UnityEngine.EventSystems;

public class StickMove : Controller, IDragHandler, IEndDragHandler
{
    private Vector3 moveDir;
    private Vector3 initialStickPosition;
    private Vector3 offset;
    private float joystickMagnitude = 100;

    void Awake()
    {
        initialStickPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - initialStickPosition, joystickMagnitude);
        transform.position = moveDir + initialStickPosition;
        //GameManager.instance.playerMoving = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moveDir = Vector3.zero;
        transform.position = initialStickPosition;
    }

    public override Vector3 MoveDirection()
    {
        Vector3 magnitude = moveDir / joystickMagnitude;
        return new Vector3(magnitude.x, 0 , magnitude.y);
    }

    public override Vector3 LookRotation()
    {
        Vector3 vectorAngle = transform.position - initialStickPosition;
        offset = Quaternion.AngleAxis(vectorAngle.x * 2, Vector3.up) * offset;
        return offset;
    }
}
