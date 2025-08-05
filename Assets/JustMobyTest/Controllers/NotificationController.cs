using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class NotificationController : MonoBehaviour
{
    [SerializeField] private float showTime = 3f;
    [SerializeField] private FailReasonTextIDData[] idMatchingDatas;

    private GameTextConfig _textConfig;
    private NotificationPanel _notificationPanel;
    private TowerController _towerController;
    private HoleAreaController _holeAreaController;
    private CancellationTokenSource _taskCancelation;

    public void Init(TowerController towerController, HoleAreaController holeAreaController,
        NotificationPanel notificationPanel, GameTextConfig textConfig)
    {
        _notificationPanel = notificationPanel;
        _towerController = towerController;
        _holeAreaController = holeAreaController;
        _textConfig = textConfig;

        _towerController.onCubeAdded += OnCubeAddedInTower;
        _towerController.onCubeRemoved += OnCubeRemovedFromTower;
        _towerController.onCubeFall += OnCubeFallFromTower;
        _holeAreaController.onUploadCube += OnUploadCube;
        _holeAreaController.onWrongDragSource += OnWrongDragToHole;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        _taskCancelation?.Cancel();

        if (_towerController)
        {
            _towerController.onCubeAdded -= OnCubeAddedInTower;
            _towerController.onCubeRemoved -= OnCubeRemovedFromTower;
            _towerController.onCubeFall -= OnCubeFallFromTower;
        }

        if (_holeAreaController)
        {
            _holeAreaController.onUploadCube -= OnUploadCube;
        }
    }

    private void OnCubeAddedInTower()
    {
        if (_textConfig.TryGetTextByID(
                new GameTextID { CategoryID = TextCategoryID.CubeActions, TextID = TextID.CubeAdded }, out var text))
        {
            ShowNotification(text);
        }
    }

    private void OnCubeRemovedFromTower()
    {
        if (_textConfig.TryGetTextByID(
                new GameTextID { CategoryID = TextCategoryID.CubeActions, TextID = TextID.CubeRemoved }, out var text))
        {
            ShowNotification(text);
        }
    }

    private void OnCubeFallFromTower(FailureReason reason)
    {
        if (TryGetCorrespondenceTextID(reason, out var gameMessageID))
        {
            if (_textConfig.TryGetTextByID(gameMessageID, out var text))
            {
                ShowNotification(text);
            }
        }
    }

    private void OnUploadCube(bool result)
    {
        var textID = new GameTextID { CategoryID = TextCategoryID.CubeActions, TextID = TextID.UploadInHol };

        if (!result) textID = new GameTextID { CategoryID = TextCategoryID.CubeActions, TextID = TextID.DontHitHol };

        if (_textConfig.TryGetTextByID(textID, out var text))
        {
            ShowNotification(text);
        }
    }
    
    private void OnWrongDragToHole()
    {
        if (_textConfig.TryGetTextByID(
                new GameTextID { CategoryID = TextCategoryID.CubeActions, TextID = TextID.WrongDragToHol }, out var text))
        {
            ShowNotification(text);
        }
    }

    private bool TryGetCorrespondenceTextID(FailureReason failureReason, out GameTextID textID)
    {
        if (idMatchingDatas != null)
        {
            foreach (var data in idMatchingDatas)
            {
                if (data.FailureReason == failureReason)
                {
                    textID = data.TextID;
                    return true;
                }
            }
        }

        textID = new GameTextID();
        return false;
    }

    private void ShowNotification(string message)
    {
        _taskCancelation?.Cancel();
        _taskCancelation = new CancellationTokenSource();
        ShowNotificationAsync(message, _taskCancelation.Token);
    }

    private async UniTask ShowNotificationAsync(string gameText, CancellationToken token)
    {
        _notificationPanel.Message = gameText;
        _notificationPanel.Show();
        await UniTask.WaitForSeconds(showTime, cancellationToken: token);
        _notificationPanel.Hide();
    }
}