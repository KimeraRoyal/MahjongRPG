using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Battle
{
    public class Character : MonoBehaviour
    {
        private Party m_party;
        
        [ShowInInspector] [ReadOnly] private CharacterData m_data;

        [SerializeField] private int m_currentHealth = 10;
        [SerializeField] private int m_maxHealth = 10;

        public Party Party
        {
            get => m_party;
            set
            {
                if(m_party == value) { return; }

                if (m_party != null) { OnRemovedFromParty?.Invoke(m_party); }
                m_party = value;
                if (m_party != null) { OnAddedToParty?.Invoke(m_party); }
            }
        }
        
        public CharacterData Data
        {
            get => m_data;
            set
            {
                if(m_data == value) { return; }
                m_data = value;
                OnDataAssigned?.Invoke(m_data);
            }
        }

        public int CurrentHealth => m_currentHealth;
        public int MaxHealth => m_maxHealth;

        public UnityEvent<Party> OnAddedToParty;
        public UnityEvent<Party> OnRemovedFromParty;
        
        public UnityEvent<CharacterData> OnDataAssigned;

        private void Awake()
        {
            OnDataAssigned.AddListener(UpdateValues);
        }

        private void UpdateValues(CharacterData _data)
        {
            gameObject.name = m_data.Name;
            
            m_currentHealth = m_data.Health;
            m_maxHealth = m_data.Health;
        }
    }
}