using Mahjong;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    private Board m_board;
    
    [ShowInInspector] [ReadOnly] private TileData m_data;

    private Zone m_currentZone;

    private bool m_selected;

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

    public ZoneType CurrentZoneType => m_currentZone?.Type ?? ZoneType.None;

    public Zone CurrentZone
    {
        get => m_currentZone;
        set
        {
            if(m_currentZone == value) { return; }
            m_currentZone?.Remove(this);
            m_currentZone = value;
            m_currentZone?.Add(this);
            OnMovedToZone?.Invoke(m_currentZone);
        }
    }

    public bool Selected
    {
        get => m_selected;
        set
        {
            if(m_selected == value) { return; }

            m_selected = value;
            if (m_selected)
            {
                OnSelected?.Invoke();
            }
            else
            {
                OnDeselected?.Invoke();
            }
        }
    }

    public bool Selectable => CurrentZoneType is ZoneType.Grip or ZoneType.Hand;

    public UnityEvent<TileData> OnDataAssigned;

    public UnityEvent<Zone> OnMovedToZone;

    public UnityEvent OnSelected;
    public UnityEvent OnDeselected;

    private void Awake()
    {
        m_board = GetComponentInParent<Board>();
    }

    public void Select()
        => Selected = true;

    public void Deselect()
        => Selected = false;

    public bool MoveToDeck()
        => MoveToZone(ZoneType.Deck);

    public bool MoveToGrip()
        => MoveToZone(ZoneType.Grip);

    public bool MoveToHand()
        => MoveToZone(ZoneType.Hand);

    public bool MoveToDiscard()
        => MoveToZone(ZoneType.Discard);

    public bool MoveToZone(ZoneType _zone)
    {
        if (CurrentZoneType == _zone) { return false; }
        CurrentZone = m_board.GetZone(_zone);
        return true;
    }
}
