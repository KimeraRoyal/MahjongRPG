using Battle;
using UnityEngine;

namespace Map
{
    public class Pieces : MonoBehaviour
    {
        private Battle.Battle m_battle;
        
        private Map m_map;

        [SerializeField] private Piece m_piecePrefab;

        private void Awake()
        {
            m_battle = FindAnyObjectByType<Battle.Battle>();
            m_battle.OnPartySpawned.AddListener(SpawnPartyPieces);

            m_map = GetComponentInParent<Map>();
        }

        private void SpawnPartyPieces(int _id, Party _party)
        {
            var tile = new Vector2Int(_id == 0 ? 3 : 6, 4);
            foreach (var member in _party.Members)
            {
                SpawnCharacterPiece(member, tile);
                tile += new Vector2Int(0, 1);
            }
        }

        private void SpawnCharacterPiece(Character _character, Vector2Int _position)
        {
            var piece = Instantiate(m_piecePrefab, transform);
            piece.Character = _character;
            m_map.AddFeature(piece, _position);
        }
    }
}
