namespace PizzaMaker.Code.Core.Movement
{
    public class CharacterMovement : ICharacterMovement
    {
        private SideMovement _sideMovement; 
        private PathFollower _pathFollower;

        public CharacterMovement(PathFollower pathFollower, SideMovement sideMovement)
        {
            _pathFollower = pathFollower;
            _sideMovement = sideMovement;
            
            Disable();
        }

        public float PathProgress => _pathFollower?.PathProgress ?? 0;

        public void Enable()
        {
            _sideMovement.enabled = true;
            _pathFollower.enabled = true;
        }
        
        public void Disable()
        {
            _sideMovement.enabled = false;
            _pathFollower.enabled = false;
        }
    }

    public interface ICharacterMovement
    {
        float PathProgress { get; }
        void Enable();
        void Disable();
    }
}