using UnityEngine;
using System.Collections;

public class AnimatedObjectController : MonoBehaviour, IObjectController 
{
	[Header("Animator options")]
	public string IsEditingAnimatorParamName = "IsEditing";
	public string IsMovingAnimatorParamName = "IsMoving";
	public string IsSelectedAnimatorParamName = "IsSelected";
	public string UpdateTriggerParamName = "UpdateTrigger";

	[Header("Object options")]
	public Animator Animator;

	private bool _isEditing;
	private bool _isMoving;
	private bool _isSelected;

	private void Start()
	{
		if (!Animator) 
		{
			Animator = GetComponent<Animator> () ?? GetComponentInChildren<Animator> ();
		}
	}

	private void UpdateObject()
	{
		if (Animator) 
		{
			Animator.SetBool (IsEditingAnimatorParamName, _isEditing);
			Animator.SetBool (IsSelectedAnimatorParamName, _isSelected);
			Animator.SetBool (IsMovingAnimatorParamName, _isMoving);
			Animator.SetTrigger (UpdateTriggerParamName);
		}
	}

	#region IObjectController implementation

	public bool IsEditing {
		get {
			return _isEditing;
		}
		set {
			if (_isEditing != value) {
				_isEditing = value;
				UpdateObject ();
			}
		}
	}

	public bool IsMoving {
		get {
			return _isMoving;
		}
		set {
			if (_isMoving != value) {
				_isMoving = value;
				UpdateObject ();
			}
		}
	}

	public bool IsSelected {
		get {
			return _isSelected;
		}
		set {
			if (_isSelected != value) {
				_isSelected = value;
				UpdateObject ();
			}
		}
	}

    public Transform GetTransform()
    {
        return transform;
    }

    #endregion
}
