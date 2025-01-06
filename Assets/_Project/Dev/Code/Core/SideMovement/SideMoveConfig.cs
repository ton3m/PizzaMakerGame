namespace PizzaMaker.Code.Core.SideMovement
{
    public struct SideMoveConfig
    {
        public readonly float Speed;
        public readonly float Limit;

        public SideMoveConfig(float speed, float limit)
        {
            Speed = speed;
            Limit = limit;
        }
    
        public static SideMoveConfig Default => 
            new(5f, 3f);
    }
}