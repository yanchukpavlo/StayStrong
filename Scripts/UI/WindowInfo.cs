using AYellowpaper;
using UnityEngine;

namespace Game.Core.UI.Windows
{
    public class WindowInfo : Window
    {
        [SerializeField] UIVariableVisual variableVisualPrefab;
        [SerializeField] Transform visualsRoot;
        [SerializeField] InterfaceReference<IVariable>[] variables;

        private void Awake()
        {
            foreach (var item in variables)
            {
                Instantiate(variableVisualPrefab, visualsRoot).Setup(item.Value);
            }
        }
    }
}