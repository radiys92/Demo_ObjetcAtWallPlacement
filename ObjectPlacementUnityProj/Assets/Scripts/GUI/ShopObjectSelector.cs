using UnityEngine;

public class ShopObjectSelector : MonoBehaviour
{
    public GameObject PrefabToSelect;

    public void OnSelect()
    {
        App.Instance.SelectedPrefab = PrefabToSelect;
        App.Instance.ScenesManager.CloseShop();
    }
}
