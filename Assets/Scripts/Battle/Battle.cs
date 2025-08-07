using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class Battle : MonoBehaviour
    {
        [SerializeField] private Character m_characterPrefab;
        
        [SerializeField] private CharacterData[] m_partyMembersA;
        [SerializeField] private CharacterData[] m_partyMembersB;
        
        [SerializeField] private Party m_a, m_b;

        public UnityEvent<int, Party> OnPartySpawned;
        public UnityEvent<Character> OnCharacterSpawned;

        private void Start()
        {
            m_a = SpawnParty(0, m_partyMembersA, Color.green);
            m_b = SpawnParty(1, m_partyMembersB, Color.red);
        }

        private Party SpawnParty(int _id, CharacterData[] _characters, Color _color)
        {
            var party = new Party(_color);
            foreach (var character in _characters)
            {
                SpawnCharacter(character, party);
            }
            OnPartySpawned?.Invoke(_id, party);
            return party;
        }

        private void SpawnCharacter(CharacterData _data, Party _party)
        {
            var character = Instantiate(m_characterPrefab, transform);
            character.Data = _data;
            _party.AddMember(character);
            OnCharacterSpawned?.Invoke(character);
        }
    }
}
