using System.Collections.Generic;
using Mahjong;
using Sirenix.OdinInspector;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private Tiles m_tiles;
    [SerializeField] private Tile m_tilePrefab;

    [SerializeField] private int m_tileCount = 4;

    [ShowInInspector] [ReadOnly] private List<Tile> m_tilesInDeck;

    private void Awake()
    {
        m_tilesInDeck = new List<Tile>();
    }

    private void Start()
    {
        Populate();
        Shuffle();
    }

    public void Populate()
    {
        for (var type = TileType.Manzu; type <= TileType.Dragon; type++)
        {
            foreach (var data in m_tiles.GetTilesOfType(type))
            {
                for (var i = 0; i < m_tileCount; i++)
                {
                    var tile = Instantiate(m_tilePrefab, transform);
                    tile.gameObject.name = data.Name;
                    tile.Data = data;
                    m_tilesInDeck.Add(tile);
                }
            }
        }
    }

    private void Shuffle()
    {
        var count = m_tilesInDeck.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            (m_tilesInDeck[i], m_tilesInDeck[r]) = (m_tilesInDeck[r], m_tilesInDeck[i]);
        }
    }
}
