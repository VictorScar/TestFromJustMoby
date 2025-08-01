using System;
using UnityEngine;

namespace ScarFramework.Button
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        private string _buttonName;
        public string ButtonName => _buttonName;
    
        public ButtonAttribute(string buttonName)
        {
            _buttonName = buttonName;
        }
    }
}
