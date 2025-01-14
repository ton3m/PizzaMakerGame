using System.Collections.Generic;

namespace PizzaMaker.Code.Utils.ECB
{
    public class Behaviours
    {
        private readonly List<object> _behaviours = new();

        public Behaviours Add(object behaviour)
        {
            _behaviours.Add(behaviour);
            return this;
        }
        
        public List<object> List => new(_behaviours);
    }
}