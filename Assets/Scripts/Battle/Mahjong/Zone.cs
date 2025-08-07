using System;
using System.Collections.Generic;
using System.Linq;
using Mahjong;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Serializable]
public class Zone
{
    [ShowInInspector] [ReadOnly] private List<Tile> m_tiles = new();

    [ShowInInspector] [ReadOnly] private ZoneType m_type;

    public IReadOnlyList<Tile> Tiles => m_tiles;
    public int Size => m_tiles.Count;
    public bool Empty => m_tiles.Count < 1;

    public ZoneType Type => m_type;

    public UnityEvent<Tile> OnTileAdded;
    public UnityEvent<Tile> OnTileRemoved;

    public Zone(ZoneType _zone)
    {
        m_type = _zone;
    }

    public void Add(Tile _tile)
    {
        m_tiles.Add(_tile);
        OnTileAdded?.Invoke(_tile);
    }

    public void Remove(Tile _tile)
    {
        m_tiles.Remove(_tile);
        OnTileRemoved?.Invoke(_tile);
    }

    public Tile Draw()
    {
        if (Empty) { return null; }
        var tile = m_tiles[^1];
        tile.CurrentZone = null;
        return tile;
    }

    public Tile DrawTo(Zone _to)
    {
        var tile = Draw();
        if (!tile) { return null;}
        tile.CurrentZone = _to;
        return tile;
    }

    public Tile DrawFrom(Zone _from)
        => _from.DrawTo(this);

    public void Shuffle()
    {
        var count = m_tiles.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            (m_tiles[i], m_tiles[r]) = (m_tiles[r], m_tiles[i]);
        }
    }

    public void Sort()
    {
        m_tiles = m_tiles
            .OrderBy(_tile => _tile.Data.Type)
            .ThenBy(_tile => _tile.Data.Value)
            .ToList();
    }
}