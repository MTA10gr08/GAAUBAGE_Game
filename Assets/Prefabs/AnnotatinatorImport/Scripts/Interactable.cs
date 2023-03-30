using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour {
	public UnityEvent<Vector2> onHoverStay, onHoverEnter, onHoverExit, onMouseSecondary, onMousePrimary, onDrag;


	public new Renderer renderer = null;
	private Color currentColor, hoverColor;

	private void Start()
	{
		if(renderer)
			currentColor = renderer.material.color;
		SetHoverColor(0.2f);
	}

	public void ColorDarken()
	{
		if(!renderer)
			return;

		renderer.material.color = hoverColor;
	}

	public void AssignNewColor(Color newColor)
	{

		renderer.material.color = newColor;

		currentColor = renderer.material.color;
	}
	public void ResetColor()
	{
		renderer.material.color = currentColor;
	}

	public void SetHoverColor(float value = 0f)
	{
		if(renderer)
			hoverColor = new Color(renderer.material.color.r - value, renderer.material.color.g - value, renderer.material.color.b - value, 1);
	}
}
