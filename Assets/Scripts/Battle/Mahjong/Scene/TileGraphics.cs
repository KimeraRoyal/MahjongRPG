using System;
using UnityEngine;

namespace Mahjong
{
    [CreateAssetMenu(fileName = "Tile Graphics", menuName = "Mahjong/Tile Graphics")]
    public class TileGraphics : ScriptableObject
    {
        [SerializeField] private Texture2D[] m_faceTextures = new Texture2D[Tiles.c_tileCount];
        [SerializeField] private Color m_backColor = Color.green;

        public Color BackColor => m_backColor;

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