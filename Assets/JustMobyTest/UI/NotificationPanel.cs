using System.Collections;
using System.Collections.Generic;
using ScarFramework.UI;
using TMPro;
using UnityEngine;

public class NotificationPanel : UIView
{
    [SerializeField] private TMP_Text textField;

    public string Message
    {
        set => textField.text = value;
    }
}