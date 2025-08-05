using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using UnityEngine;

public class GameScreen : UIScreen
{
    [SerializeField] private ScrollPanelView scrollPanelView;
    [SerializeField] private DragViewPanel dragViewPanel;
    [SerializeField] private TowerView towerView;
    [SerializeField] private HoleViewArea holeArea;
    [SerializeField] private NotificationPanel notificationPanel;
    public ScrollPanelView ScrollViewPanel => scrollPanelView;
    public DragViewPanel DragViewPanel => dragViewPanel;
    public TowerView TowerView => towerView;
    public HoleViewArea HoleArea => holeArea;
    public NotificationPanel NotificationPanel => notificationPanel;

    protected override void OnInit()
    {
        scrollPanelView.Init();
        dragViewPanel.Init();
        towerView.Init();
        holeArea.Init();
        notificationPanel.Init();
    }
}
