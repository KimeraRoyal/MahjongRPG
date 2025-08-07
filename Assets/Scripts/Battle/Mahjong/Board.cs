using System;
using Mahjong;
using UnityEngine;

[RequireComponent(typeof(TilePool))]
public class Board : MonoBehaviour
{
    private TilePool m_pool;
    
    [SerializeField] private Zone m_deck = new(ZoneType.Deck);
    [SerializeField] private Zone m_grip = new(ZoneType.Grip);
    [SerializeField] private Zone m_hand = new(ZoneType.Hand);
    [SerializeField] private Zone m_discard = new(ZoneType.Discard);

    public Zone Deck => m_deck;
    public Zone Grip => m_grip;
    public Zone Hand => m_hand;
    public Zone Discard => m_discard;

    private void Awake()
    {
        m_pool = GetComponent<TilePool>();
    }

    private void Start()
    {
        m_pool.Populate(m_deck);
        m_deck.Shuffle();

        for (var i = 0; i < 13; i++)
        {
            m_hand.DrawFrom(m_deck);
        }
        Draw();
    }

    public void Draw()
    { 
        m_hand.DrawFrom(m_grip);
        m_hand.Sort();
        
        m_grip.DrawFrom(m_deck);
    }

    public Zone GetZone(ZoneType _type)
        => _type switch
        {
            ZoneType.None => null,
            ZoneType.Deck => Deck,
            ZoneType.Grip => Grip,
            ZoneType.Hand => Hand,
            ZoneType.Discard => Discard,
            _ => throw new ArgumentOutOfRangeException(nameof(_type), _type, null)
        };
}
