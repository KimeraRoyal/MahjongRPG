using Coremera.Interaction.Mouse;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(ClickableObject))]
    public class ClickableMapElement : MonoBehaviour
    {
        private MapSelection m_selection;
        
        private MapElement m_element;
        
        private ClickableObject m_clickable;

        private void Awake()
        {
            m_selection = GetComponentInParent<MapSelection>();

            m_element = GetComponentInParent<MapElement>();
            
            m_clickable = GetComponent<ClickableObject>();
            m_clickable.OnClicked.AddListener(Select);
        }
        
        private void Select()
            => m_selection.SelectTile(m_element.Position);
    }
}