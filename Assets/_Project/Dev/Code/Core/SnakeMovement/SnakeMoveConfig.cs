namespace PizzaMaker.Code.Core.SnakeMovement
{
    public struct SnakeMoveConfig
    {
        public readonly float SideFollowSpeed;
        public readonly float ForwardFollowSpeed;
        public readonly float ForwardFollowDistance;
        public readonly bool HasHeadOffset;
        
        public SnakeMoveConfig(
            float sideFollowSpeed,
            float forwardFollowSpeed,
            float forwardFollowDistance,
            bool hasHeadOffset)
        {
            SideFollowSpeed = sideFollowSpeed;
            ForwardFollowSpeed = forwardFollowSpeed;
            ForwardFollowDistance = forwardFollowDistance;
            HasHeadOffset = hasHeadOffset;
        }

        public static SnakeMoveConfig Default => 
            new(8f, 5f, 3f, false);
    }
}