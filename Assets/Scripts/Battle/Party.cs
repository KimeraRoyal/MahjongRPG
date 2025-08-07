using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class Party
    {
        [ShowInInspector] [ReadOnly] private List<Character> m_members;

        [SerializeField] private Color m_color;

        public IReadOnlyList<Character> Members => m_members;

        public Color Color => m_color;
        
        public Party(Color _color)
        {
            m_members = new List<Character>();
            m_color = _color;
        }

        public void AddMember(Character _character)
        {
            m_members.Add(_character);
            _character.Party = this;
        }

        public void RemoveMember(Character _character)
        {
            if(!m_members.Remove(_character)) { return; }
            _character.Party = null;
        }
    }
}