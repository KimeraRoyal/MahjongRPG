using System;
using UnityEngine;

namespace Mahjong
{
    [CreateAssetMenu(fileName = "Tile Faces", menuName = "Mahjong/Tile Faces")]
    public class TileFaces : ScriptableObject
    {
        [SerializeField] private Texture2D[] m_faceTextures = new Texture2D[Tiles.c_tileCount];

        private void OnValidate()
        {
            if(m_faceTextures.Length == Tiles.c_tileCount) { return; }
            Array.Resize(ref m_faceTextures, Tiles.c_tileCount);
        }
    }
}