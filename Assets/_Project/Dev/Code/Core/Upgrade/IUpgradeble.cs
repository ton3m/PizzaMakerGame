namespace PizzaMaker.Code.Core.Upgrade
{
    public interface IUpgradeble
    {
         int Level { get; }
         float Multiplier  { get; }
         int UpgradeCost { get; }
         void Upgrade();
    }
}