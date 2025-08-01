using Mahjong;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    [ShowInInspector] [ReadOnly] private TileData m_data;

    public TileData Data
    {
        get => m_data;
        set
        {
            if(m_data == value) { return; }
            m_data = value;
            OnDataAssigned?.Invoke(m_data);
        }
    }

    public UnityEvent<TileData> OnDataAssigned;
}
