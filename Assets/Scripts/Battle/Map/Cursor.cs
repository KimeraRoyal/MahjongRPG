using UnityEngine;

namespace Map
{
    public class Cursor : MonoBehaviour
    {
        private Renderer m_renderer;

        private void Awake()
        {
            m_renderer = GetComponentInChildren<Renderer>();
        }
    }
}
