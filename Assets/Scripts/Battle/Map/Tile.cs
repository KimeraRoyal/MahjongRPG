namespace Map
{
    public class Tile : MapElement
    {
        private void Start()
        {
            gameObject.name = $"Tile: ({Position.x}, {Position.y})";
        }
    }
}
