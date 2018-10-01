using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour , IDragHandler, IPointerDownHandler, IPointerUpHandler 
{
	public Image bgImg; //The base of virtual joystick
	public Image joystickImg;//The virtual joystick

	public Vector3 InputDirection{ set; get; }//Variable that describes the input

	//Always starts at zero
	private void Start()
	{
		InputDirection = Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos = Vector2.zero;
		//When is dragged, it moves the joystick and changes the InputDirection
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) 
		{
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 +1: pos.x *2 -1;
			float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 +1: pos.y *2 -1;

			InputDirection = new Vector3 (x, 0, y);
			InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

			joystickImg.rectTransform.anchoredPosition = new Vector3 (InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3),
				InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
	}
	//When it stops dragging, it returns to its positional position
	public virtual void OnPointerUp(PointerEventData ped)
	{
		InputDirection = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}















}
