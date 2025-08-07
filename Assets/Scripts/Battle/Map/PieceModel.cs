using Battle;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Map
{
    public class PieceModel : MonoBehaviour
    {
        private static readonly int s_colorId = Shader.PropertyToID("_BaseColor");
        
        private Piece m_piece;

        private Renderer m_model;
        
        [OnValueChanged("MakeDirty")] [SerializeField] private Color m_color = Color.green;

        private bool m_dirty;

        private MaterialPropertyBlock m_propertyBlock;

        private void Awake()
        {
            m_piece = GetComponentInParent<Piece>();
            m_piece.OnCharacterAssigned.AddListener(SpawnModel);
        
            m_propertyBlock = new MaterialPropertyBlock();
        }

        private void Update()
        {
            if(!m_dirty) { return; }
            UpdateMaterials();
        }

        private void UpdateMaterials()
        {
            m_model.GetPropertyBlock(m_propertyBlock);
            m_propertyBlock.SetColor(s_colorId, m_color);
            m_model.SetPropertyBlock(m_propertyBlock);

            m_dirty = false;
        }

        private void SpawnModel(Character _character)
        {
            m_model = Instantiate(_character.Data.GamePiecePrefab, transform);
            m_color = _character.Party.Color;
            UpdateMaterials();
        }
        
        private void MakeDirty()
            => m_dirty = true;
    }
}
