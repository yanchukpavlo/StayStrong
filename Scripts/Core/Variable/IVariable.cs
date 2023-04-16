using UnityEngine;

namespace Game.Core
{
    public interface IVariable
    {
        public string Name { get; }
        public Sprite Icon { get; }
        public string ValueString { get; }
        public string Prefix { get; }
    }
}