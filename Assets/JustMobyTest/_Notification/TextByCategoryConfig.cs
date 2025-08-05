using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "GameConfigs/GameTexts/CategoryTexts", fileName = "CubesActionsTexts")]
public class TextByCategoryConfig : ScriptableObject
{
    [SerializeField] private TextCategoryID categoryID;
    [SerializeField] private TextData[] textDatas;

    public TextCategoryID CategoryID => categoryID;

    public bool TryGetTextByID(TextID id, out string text)
    {
        if (textDatas != null)
        {
            foreach (var data in textDatas)
            {
                if (data.ID == id)
                {
                    text = data.Text;
                    return true;
                }
            }
        }

        text = string.Empty;
        return false;
    }
}