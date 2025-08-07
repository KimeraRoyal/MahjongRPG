using Mahjong;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SetFinderDebug : MonoBehaviour
{
    private SetFinder m_finder;
    
    private TMP_Text m_text;

    private void Awake()
    {
        m_finder = FindAnyObjectByType<SetFinder>();
        
        m_text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(!m_finder.IsDirty) { return; }

        var sets = m_finder.Sets;
        var output = $"{sets.Count} Sets Found";
        foreach (var set in sets)
        {
            output += $"\n{set.Type} of Suit: {set.Suit} (";
            for (var i = 0; i < set.Tiles.Length; i++)
            {
                output += $"{(i > 0 ? ", " : "")}{set.Tiles[i].Data.GetValueString()}";
            }
            output += ")";
        }
        m_text.text = output;
    }
}
