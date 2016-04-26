using UnityEngine;
using System.Collections;

public interface IObjectController
{
	bool IsEditing { get; set; }
	bool IsMoving { get; set; }
	bool IsSelected { get; set; }
}