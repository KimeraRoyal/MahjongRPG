using System;
using DG.Tweening;
using Mahjong;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TileInfo : MonoBehaviour
{
    private Tile m_tile;

    private CanvasGroup m_canvasGroup;
    private TMP_Text m_text;
    
    private void Awake()
    {
        m_tile = GetComponentInParent<Tile>();
        m_tile.OnDataAssigned.AddListener(OnDataAssigned);
        m_tile.OnMovedToZone.AddListener(OnMovedToZone);
        
        m_canvasGroup = GetComponent<CanvasGroup>();
        
        m_text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        if (!m_tile || m_tile.CurrentZone == null)
        {
            m_canvasGroup.alpha = 0.0f;
            return;
        }
        m_canvasGroup.alpha = m_tile.CurrentZone.Type is ZoneType.Grip or ZoneType.Hand ? 1.0f : 0.0f;
    }

    private void OnMovedToZone(Zone _zone)
    {
        if(_zone == null) { return; }
        var alpha = _zone.Type is ZoneType.Grip or ZoneType.Hand ? 1.0f : 0.0f;
        m_canvasGroup.DOFade(alpha, 0.3f);
    }

    private void OnDataAssigned(TileData _data)
    {
        m_text.text = $"{_data.Type}\n{_data.GetValueString()}";
    }
}
