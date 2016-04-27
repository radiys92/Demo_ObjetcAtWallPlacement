using System;
using UnityEngine;
using System.Collections;

public class ObjectPanelController : MonoBehaviour
{
    public void AddNewObjectButtonPress()
    {
        App.Instance.ScenesManager.OpenShop();
    }

    public void ClearObjectsButtonPress()
    {
        throw new NotImplementedException();
    }
}
