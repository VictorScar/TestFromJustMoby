using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class GameScreen : UIScreen
{
    [SerializeField] private ScrollPanelView scrollPanelView;
    [SerializeField] private DragViewPanel dragViewPanel;
    [SerializeField] private TowerView towerView;
    public ScrollPanelView ScrollViewPanel => scrollPanelView;
    public DragViewPanel DragViewPanel => dragViewPanel;
    public TowerView TowerView => towerView;

    protected override void OnInit()
    {
        scrollPanelView.Init();
        dragViewPanel.Init();
        towerView.Init();
    }
}
