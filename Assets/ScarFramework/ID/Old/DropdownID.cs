using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScarFramework
{
    [Serializable]
    public struct DropdownID
    {
        private bool _isInitialized;

        public string IDName;
        public int ID;


        public bool IsInitialized => _isInitialized;

        public void Initialize()
        {
            _isInitialized = true;
            Debug.Log("ID Initialized! ID: " + ID);

            if (IDName == String.Empty)
            {
                IDName = "default";
            }
            // ID = IDName.GetHashCode();
        }
    }
}