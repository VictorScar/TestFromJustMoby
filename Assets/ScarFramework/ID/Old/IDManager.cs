using System.Collections;
using System.Collections.Generic;
using ScarFramework;
using UnityEngine;

public class IDManager : ScriptableObject
{
    [SerializeField] private List<DropdownIDCollection> idCollections;
    [SerializeField] private int divider;
}