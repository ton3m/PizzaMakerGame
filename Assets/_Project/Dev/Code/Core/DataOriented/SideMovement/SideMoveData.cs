namespace PizzaMaker.Code.Core.SideMovement
{
    public struct SideMoveData
    {
        public readonly float Speed;
        public readonly float Limit;

        public SideMoveData(float speed, float limit)
        {
            Speed = speed;
            Limit = limit;
        }
    
        public static SideMoveData Default => 
            new(5f, 3f);
    }
}