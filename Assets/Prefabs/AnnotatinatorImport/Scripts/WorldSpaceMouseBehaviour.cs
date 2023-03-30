using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldSpaceMouseBehaviour : MonoBehaviour {
	public static WorldSpaceMouseBehaviour Instance;
	[SerializeField] private float MoveAmountBeforeDrag = 1, MoveAmount = 0;
	private Vector2 mousePos { get => Camera.main.ScreenToWorldPoint(Input.mousePosition); }
	private Vector2 mouseLastPos = Vector2.zero;
	private Interactable currentDraggedInteractable;
	private Interactable CurrentDraggedInteractable { get => currentDraggedInteractable; set => currentDraggedInteractable = value; }

	private Interactable currentSelectedInteractable;
	private Interactable CurrentSelectedInteractable { get => currentSelectedInteractable; set => currentSelectedInteractable = value; }

	private Interactable currentHoveredInteractable;
	public Interactable CurrentHoveredInteractable {
		get
		{
			return currentHoveredInteractable;
		}
		set
		{
			currentHoveredInteractable?.onHoverExit.Invoke(mousePos);
			currentHoveredInteractable = value;
			currentHoveredInteractable?.onHoverEnter.Invoke(mousePos);
		}
	}

	private void Awake()
	{
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}


	private void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

		if (hit && hit.transform.TryGetComponent(out Interactable interactable) && !EventSystem.current.IsPointerOverGameObject()) {
			interactable.onHoverStay.Invoke(mousePos);
			if(interactable != CurrentHoveredInteractable) {
				CurrentHoveredInteractable = interactable;
			}
			if(Input.GetMouseButtonDown(0)) {
				currentSelectedInteractable = interactable;
				mouseLastPos = Input.mousePosition;
			}
			if(Input.GetMouseButton(0)) {
				var mouseDelta = mouseLastPos - (Vector2)Input.mousePosition;
				MoveAmount += mouseDelta.magnitude;
				if(MoveAmount > MoveAmountBeforeDrag && !currentDraggedInteractable) {
					currentDraggedInteractable = currentSelectedInteractable;
					currentSelectedInteractable = null;
				}
				mouseLastPos = Input.mousePosition;
			}
			if(Input.GetMouseButtonUp(0)) {
				currentDraggedInteractable = null;
				MoveAmount = 0;
				if(currentSelectedInteractable == interactable) {
					interactable.onMousePrimary.Invoke(mousePos);
				}
			}

			if(Input.GetMouseButtonDown(1)) {
				currentSelectedInteractable = interactable;
			}
			if(Input.GetMouseButtonUp(1) & currentSelectedInteractable == interactable) {
				interactable.onMouseSecondary.Invoke(mousePos);
			}
		} else {
			CurrentHoveredInteractable = null;
		}


		currentDraggedInteractable?.onDrag.Invoke(mousePos);
	}
}