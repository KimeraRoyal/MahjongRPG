using Battle;
using DG.Tweening;
using Map;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Piece : Feature
{
    [ShowInInspector] [ReadOnly] private Character m_character;

    public Character Character
    {
        get => m_character;
        set
        {
            if(m_character == value) { return; }
            m_character = value;
            OnCharacterAssigned?.Invoke(m_character);
        }
    }

    public UnityEvent<Character> OnCharacterAssigned;

    private void Awake()
    {
        OnCharacterAssigned.AddListener(UpdateValues);
        
        OnMove.AddListener(MovePiece);
        OnMoveInstant.AddListener(MovePieceInstant);
    }

    private void MovePiece(Vector2Int _startPosition, Vector2Int _position)
    {
        var distance = _position - _startPosition;
        var stepCount = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y));
        
        var sequence = DOTween.Sequence();
        var skippedOccupiedTile = false;

        void MoveToTile(Vector2Int _position)
        {
            if (Map.HasFeatureAt(_position) && Map.GetFeatureAt(_position) != this)
            {
                skippedOccupiedTile = true;
                return;
            }

            var height = skippedOccupiedTile ? 2.5f : 1f;
            var duration = skippedOccupiedTile ? 0.55f : 0.35f;
            sequence.Append(transform.DOJump(Map.TileToWorldPosition(_position), height, 1, duration).SetEase(Ease.InSine));
            skippedOccupiedTile = false;
        }
        
        for (var x = 0; x < stepCount.x; x++)
        {
            MoveToTile(_startPosition + new Vector2Int((x + 1) * (distance.x > 0 ? 1 : -1), 0));
        }
        for (var y = 0; y < stepCount.y; y++)
        {
            MoveToTile(_startPosition + new Vector2Int(distance.x, (y + 1) * (distance.y > 0 ? 1 : -1)));
        }
    }

    private void MovePieceInstant(Vector2Int _position)
        => transform.position = Map.TileToWorldPosition(_position);

    private void UpdateValues(Character _character)
    {
        gameObject.name = $"{_character.name} Piece";
    }
}