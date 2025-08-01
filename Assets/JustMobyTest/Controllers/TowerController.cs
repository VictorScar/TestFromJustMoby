using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerController : MonoBehaviour
{
    [SerializeField] private TowerView view;

    public void Init()
    {
        view.onClick += OnViewClicked;
    }

    private void OnViewClicked(UIClickableView clickableView, PointerEventData eventData)
    {
        Debug.Log("Tower view Click");
    }
}
