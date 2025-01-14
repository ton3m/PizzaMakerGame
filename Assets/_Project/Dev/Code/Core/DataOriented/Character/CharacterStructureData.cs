using UnityEngine;

namespace PizzaMaker.Code.Core.Character
{
    public struct CharacterStructureData
    {
        public readonly Transform Origin;
        public readonly Transform Body;
        
        public CharacterStructureData(Transform characterOrigin, Transform characterBody)
        {
            Origin = characterOrigin;
            Body = characterBody;
        }
    }
}