using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image BackgroundImage { set; get; }
    public Image HandleImage { set; get; }
    public Vector2 Direction { protected set; get; }
    public Vector3 OriginalPosition { set; get; }
    public Vector3 HandlePosition { set; get; }

    private float radius;

    private void Awake()
    {
        BackgroundImage = transform.Find("Background").GetComponent<Image>();
        HandleImage = transform.Find("Handle").GetComponent<Image>();
        radius = BackgroundImage.rectTransform.sizeDelta.x / 2f;
        OriginalPosition = BackgroundImage.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BackgroundImage.transform.position = eventData.position;
        Direction = Vector3.zero;

    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveJoystick(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HandleImage.transform.localPosition = Vector3.zero;
        Direction = Vector3.zero;
        BackgroundImage.transform.position = OriginalPosition;
        HandlePosition = HandleImage.transform.localPosition;

    }

    void MoveJoystick(Vector3 position)
    {
        Vector2 offset = position - BackgroundImage.transform.position;
        Vector3 realDirection = Vector2.ClampMagnitude(offset, radius);

        Direction = realDirection.normalized;

        Vector3 pos = new Vector3(
            BackgroundImage.transform.position.x + realDirection.x,
            BackgroundImage.transform.position.y + realDirection.y,
            HandleImage.transform.position.z);

        HandleImage.transform.position = pos;
        HandlePosition = HandleImage.transform.localPosition;

    }
}
