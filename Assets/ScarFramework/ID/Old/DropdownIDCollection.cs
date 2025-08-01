using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScarFramework
{
    [CreateAssetMenu(menuName = "ScarFrameWork/IDManager", fileName = "IDManager")]
    public class DropdownIDCollection : ScriptableObject
    {
        [SerializeField] private List<DropdownID> ids = new List<DropdownID>();
        [SerializeField] private int groupID = 1;
        [SerializeField] private int slasher = 77;

        public int GroupID => groupID;
        
        public List<DropdownID> IDs => ids;
        public int Count => ids.Count;

        public int GetIndex(int refid)
        {
            for (var i = 0; i < ids.Count; i++)
            {
                var id = ids[i];
                if (id.ID == refid)
                {
                    return i;
                }
            }

            return -1;
        }

        public int CombineID(int id)
        {
            var combinedIDStr= string.Empty;
            combinedIDStr += groupID;
            combinedIDStr += slasher;
            combinedIDStr += id;
            return Convert.ToInt32(combinedIDStr);
        }
      
    }
}