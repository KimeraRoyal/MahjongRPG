using Coremera.Interaction.Mouse;
using UnityEngine;

namespace Mahjong
{
    [RequireComponent(typeof(ClickableObject))]
    public class ClickableTile : MonoBehaviour
    {
        private Tile m_tile;
        
        private ClickableObject m_clickable;

        private void Awake()
        {
            m_tile = GetComponentInParent<Tile>();

            m_clickable = GetComponent<ClickableObject>();
            
            m_clickable.OnClicked.AddListener(Select);
        }
        
        private void Select()
        {
            var selection = GetComponentInParent<TileSelection>();
            if(!selection) { return; }
            
            selection.SelectTile(m_tile);
        }
    }
}