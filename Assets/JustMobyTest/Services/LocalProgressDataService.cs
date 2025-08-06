using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JustMobyTest._Model;
using JustMobyTest.Data;
using Newtonsoft.Json;
using UnityEngine;

public class LocalProgressDataService : IProgressDataService
{
    private string _saveDataKey = "CubesSaveData";
    private TowerData _data;

    public LocalProgressDataService(string saveDataKey)
    {
        _saveDataKey = saveDataKey;
    }
    
    public CubeData[] Data
    {
        set
        {
            if (value != null)
            {
                var saveData = new List<CubeDataInfo>();

                foreach (var cube in value)
                {
                    saveData.Add(new CubeDataInfo
                    {
                        CubeType = cube.CubeType,
                        XPosition = cube.Position.x,
                        Height = Mathf.CeilToInt(cube.Position.y)
                    });
                }

                _data = new TowerData
                {
                    CubesInfo = saveData.ToArray(),
                };

                Save();
            }
        }
    }

    public UniTask<TowerData> LoadData()
    {
        if (PlayerPrefs.HasKey(_saveDataKey))
        {
            var loadedJson = PlayerPrefs.GetString(_saveDataKey);
//            Debug.Log(loadedJson);
            _data = JsonConvert.DeserializeObject<TowerData>(loadedJson);
        }
        else
        {
            _data = new TowerData();
        }

        return UniTask.FromResult(_data);
    }
    
    public void ResetSaves()
    {
        _data = new TowerData();
        PlayerPrefs.DeleteKey(_saveDataKey);
    }

    private void Save()
    {
        var json = JsonConvert.SerializeObject(_data);
//        Debug.Log(json);
        PlayerPrefs.SetString(_saveDataKey, json);
        PlayerPrefs.Save();
    }
}