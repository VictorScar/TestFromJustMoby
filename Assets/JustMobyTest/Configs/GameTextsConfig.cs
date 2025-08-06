using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/GameTexts/GameTextConfig", fileName = "GameTextConfig")]
public class GameTextsConfig : ScriptableObject
{
    [SerializeField] private TextByCategoryConfig[] textCategories;
    
    public bool TryGetTextByID(GameTextID gameTextID, out string gameText)
    {
        foreach (var category in textCategories)
        {
            if (category.CategoryID == gameTextID.CategoryID)
            {
                if (category.TryGetTextByID(gameTextID.TextID, out var text))
                {
                    gameText = text;
                    return true;
                }
            }
        }
        
        gameText = string.Empty;
        return false;
    }
}
