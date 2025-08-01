using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class ScrollPanelView : UIView
{
    [SerializeField] private ScrollCubeView cubeViewPrefab;
    [SerializeField] private RectTransform root;

    public ScrollCubeView CreateView(Sprite elementDataImage)
    {
        if (cubeViewPrefab)
        {
            var view = Instantiate(cubeViewPrefab, root);
            view.Icon = elementDataImage;

            return view;
        }

        return null;
    }

    public void Clear()
    {
        var childrens = root.GetComponentsInChildren<ScrollCubeView>();

        for (int i = childrens.Length - 1; i >= 0; i--)
        {
            Destroy(childrens[i].gameObject);
        }
    }
}