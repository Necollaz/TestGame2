namespace BaseGame.Scripts.Data
{
    public struct SpawnInfo
    {
        public FigureData Template { get; private set; }
        public ShapeType Shape { get; private set; }
        
        public SpawnInfo(FigureData template, ShapeType shape)
        {
            Template = template;
            Shape = shape;
        }
    }
}