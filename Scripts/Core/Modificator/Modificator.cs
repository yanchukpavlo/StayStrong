using UnityEngine;

namespace Game.Core
{
    public abstract class Modificator : ScriptableObject
    {
        public static void SetupAll(Modificator[] modificators)
        {
            foreach (var item in modificators)
                item.Setup();
        }

        public static void EnableAll(Modificator[] modificators)
        {
            foreach (var item in modificators)
                item.Enable();
        }

        public static void DisableAll(Modificator[] modificators)
        {
            foreach (var item in modificators)
                item.Disable();
        }

        public abstract void Setup();
        public abstract void Enable();
        public abstract void Disable();

        public abstract void ManualUpdate(int amount);
    }
}