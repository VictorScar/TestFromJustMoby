using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class GameScreen : UIScreen
{
    [SerializeField] private ScrollPanelView scrollPanelView;
    [SerializeField] private DragViewPanel dragViewPanel;
    public ScrollPanelView ScrollViewPanel => scrollPanelView;
    public DragViewPanel DragViewPanel => dragViewPanel;

    protected override void OnInit()
    {
        scrollPanelView.Init();
        dragViewPanel.Init();
    }
}
