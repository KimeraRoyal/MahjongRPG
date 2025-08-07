using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Mahjong
{
    public class SetFinder : MonoBehaviour
    {
        private class TileSetInfo
        {
            private Tile m_tile;
            private bool m_isUsedForRun;
            private bool m_isUsedForTriplet;
            
            public Tile Tile => m_tile;
            public bool IsUsedForRun => m_isUsedForRun;
            public bool IsUsedForTriplet => m_isUsedForTriplet;

            public TileSetInfo(Tile _tile)
            {
                m_tile = _tile;
            }
            
            public void MarkUsedForRun()
                => m_isUsedForRun = true;
            
            public void MarkUsedForTriplet()
                => m_isUsedForTriplet = true;
        }
        
        private Board m_board;

        private List<Set> m_sets;

        private bool m_dirty;

        public IReadOnlyList<Set> Sets
        {
            get
            {
                if (m_dirty)
                {
                    m_sets = FindSetsInHand();
                    m_dirty = false;
                }
                return m_sets;
            }
        }

        public bool IsDirty => m_dirty;

        private void Awake()
        {
            m_board = GetComponentInParent<Board>();

            m_sets = new List<Set>();
            
            m_board.Grip.OnTileAdded.AddListener(OnHandChanged);
            m_board.Grip.OnTileRemoved.AddListener(OnHandChanged);
            m_board.Hand.OnTileRemoved.AddListener(OnHandChanged);
        }
        
        private static List<Set> FindSets(IReadOnlyList<TileSetInfo> _tiles)
        {
            var sets = new List<Set>();

            for (var i = 0; i < _tiles.Count; i++)
            {
                i += Mathf.Max(0, FindSet(sets, _tiles, i));
            }
            return sets;
        }

        private List<Set> FindSetsInHand()
        {
            var tilesInHand = m_board.Hand.Tiles.Select(_tile => new TileSetInfo(_tile)).ToList();
            if (!m_board.Grip.Empty)
            {
                tilesInHand.Add(new TileSetInfo(m_board.Grip.Tiles[0]));
            }
            return FindSets(tilesInHand
                .OrderBy(_tile => _tile.Tile.Data.Type)
                .ThenBy(_tile => _tile.Tile.Data.Value)
                .ToList());
        }

        private static int FindSet(List<Set> _sets, IReadOnlyList<TileSetInfo> _tiles, int _index)
        {
            var indexInList = _tiles.Count - _index - 1;
            if (indexInList < 1) { return 0; }

            if (FindRun(_sets, _tiles, indexInList)) { return 0; }
            return FindTriplet(_sets, _tiles, indexInList);
        }

        private static bool FindRun(List<Set> _sets, IReadOnlyList<TileSetInfo> _tiles, int _index)
        {
            if (!_tiles[_index].Tile.Data.Type.IsSuited()) { return false; }
            
            TileType? type = null;

            var tilesInSet = new List<TileSetInfo>();
            var prev = -1;
            var count = 0;
            
            for (var i = _index; i >= 0; i--)
            {
                if (type != null && type != _tiles[i].Tile.Data.Type) { return false; }
                
                if (_tiles[i].Tile.Data.Value == prev) { continue; }
                
                if (count < 1 || (_tiles[i].Tile.Data.Value == prev - 1))
                {
                    //if(_tiles[i].IsUsedForRun) { continue; }
                    
                    tilesInSet.Add(_tiles[i]);
                    prev = _tiles[i].Tile.Data.Value;
                    type = _tiles[i].Tile.Data.Type;
                    count++;
                }
                else
                {
                    return false;
                }
                
                if(count < 3) { continue; }
                break;
            }
            if (tilesInSet.Count < 3) { return false; }
            
            foreach (var tile in _tiles)
            {
                tile.MarkUsedForRun();
            }
            (tilesInSet[0], tilesInSet[2]) = (tilesInSet[2], tilesInSet[0]);
            AddSet(SetType.Run, _sets, tilesInSet);

            return true;
        }

        private static int FindTriplet(List<Set> _sets, IReadOnlyList<TileSetInfo> _tiles, int _index)
        {
            TileType? type = null;

            var tilesInSet = new List<TileSetInfo>();
            var prev = -1;
            var count = 0;

            for (var i = _index; i >= 0; i--)
            {
                if (count < 1 || (_tiles[i].Tile.Data.Value == prev && _tiles[i].Tile.Data.Type == type))
                {
                    //if(_tiles[i].IsUsedForTriplet) { continue; }
                    
                    tilesInSet.Add(_tiles[i]);
                    prev = _tiles[i].Tile.Data.Value;
                    type = _tiles[i].Tile.Data.Type;
                    count++;
                }
                else
                {
                    break;
                }

                if (count < 3) { continue; }
                break;
            }
            if (count < 2) { return 0; }
            
            foreach (var tile in _tiles)
            {
                tile.MarkUsedForTriplet();
            }
            var setType = SetType.Pair + (count - 2);
            AddSet(setType, _sets, tilesInSet);

            return count - 2;
        }

        private static void AddSet(SetType _type, List<Set> _sets, IReadOnlyList<TileSetInfo> _tiles)
        {
            _sets.Add(new Set(_type, _tiles[0].Tile.Data.Type, _tiles.Select(_tile => _tile.Tile).ToArray()));
        }

        private void OnHandChanged(Tile _tile)
        {
            m_dirty = true;
        }
    }
}