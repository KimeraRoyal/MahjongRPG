using System;
using UnityEngine;

namespace Mahjong
{
    [CreateAssetMenu(fileName = "Tile Faces", menuName = "Mahjong/Tile Faces")]
    public class TileFaces : ScriptableObject
    {
        [SerializeField] private Texture2D[] m_faceTextures = new Texture2D[Tiles.c_tileCount];

        public Texture2D GetFaceTexture(int _id)
        {
            if (_id < 0 || _id >= m_faceTextures.Length) { return null; }
            return m_faceTextures[_id];
        }

        private void OnValidate()
        {
            if(m_faceTextures.Length == Tiles.c_tileCount) { return; }
            Array.Resize(ref m_faceTextures, Tiles.c_tileCount);
        }
    }
}