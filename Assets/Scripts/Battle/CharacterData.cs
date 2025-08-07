using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(fileName = "Character", menuName = "Battle/Character")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string m_name = "Character";

        [SerializeField] private int m_health = 10;
        [SerializeField] private int m_movement = 5;

        [SerializeField] private Renderer m_gamePieceModel;

        public string Name => m_name;
        
        public int Health => m_health;
        public int Movement => m_movement;

        public Renderer GamePiecePrefab => m_gamePieceModel;
    }
}
