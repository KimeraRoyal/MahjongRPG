using Mahjong;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TileMaterials : MonoBehaviour
{
    private static readonly int s_faceTextureId = Shader.PropertyToID("_BaseMap");
    private static readonly int s_backColorId = Shader.PropertyToID("_BaseColor");
    
    private Tile m_tile;
    [SerializeField] private TileFaces m_faceTextures;
    
    private Renderer m_renderer;
    
    [OnValueChanged("MakeDirty")] [SerializeField] private Texture2D m_faceTexture;
    [OnValueChanged("MakeDirty")] [SerializeField] private Color m_backColor = Color.green;

    private bool m_dirty;

    private MaterialPropertyBlock m_propertyBlock;

    public Texture2D FaceTexture
    {
        get => m_faceTexture;
        set
        {
            m_faceTexture = value;
            MakeDirty();
        }
    }

    public Color BackColor
    {
        get => m_backColor;
        set
        {
            m_backColor = value;
            MakeDirty();
        }
    }

    private void Awake()
    {
        m_tile = GetComponentInParent<Tile>();
        m_tile.OnDataAssigned.AddListener(OnDataAssigned);
        if(m_tile.Data != null) { OnDataAssigned(m_tile.Data); }
        
        m_renderer = GetComponent<Renderer>();
        
        m_propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if(!m_dirty) { return; }
        UpdateMaterials();
    }

    private void UpdateMaterials()
    {
        m_renderer.GetPropertyBlock(m_propertyBlock, 1);
        m_propertyBlock.SetColor(s_backColorId, m_backColor);
        m_renderer.SetPropertyBlock(m_propertyBlock, 1);
        
        m_renderer.GetPropertyBlock(m_propertyBlock, 2);
        m_propertyBlock.SetTexture(s_faceTextureId, m_faceTexture);
        m_renderer.SetPropertyBlock(m_propertyBlock, 2);

        m_dirty = false;
    }

    private void OnDataAssigned(TileData _data)
    {
        m_faceTexture = m_faceTextures.GetFaceTexture(m_tile.Data.ID);
        UpdateMaterials();
    }

    private void MakeDirty()
        => m_dirty = true;
}
