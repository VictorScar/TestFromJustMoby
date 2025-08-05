using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    [SerializeField] private string saveDataKey = "CubesSaveData";
    private TowerData _data;

    public TowerCube[] Data
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
                        XPosition = cube.XPos,
                        Height = cube.Height
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
        if (PlayerPrefs.HasKey(saveDataKey))
        {
            var loadedJson = PlayerPrefs.GetString(saveDataKey);
            Debug.Log(loadedJson);
            _data = JsonConvert.DeserializeObject<TowerData>(loadedJson);
        }
        else
        {
            _data = new TowerData();
        }

        return UniTask.FromResult(_data);
    }

    private void Save()
    {
        var json = JsonConvert.SerializeObject(_data);
        Debug.Log(json);
        PlayerPrefs.SetString(saveDataKey, json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.HasKey(saveDataKey));
    }

    public void ResetSaves()
    {
        _data = new TowerData();
        PlayerPrefs.DeleteKey(saveDataKey);
    }
}