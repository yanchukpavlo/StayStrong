using UnityEngine;
using UnityEngine.EventSystems;
using HighlightPlus;

namespace Game.Core
{
    [RequireComponent(typeof(HighlightEffect))]
    public class BaseMapObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] HighlightEffect outline;

        private void OnValidate()
        {
            if (!outline)
                outline = GetComponent<HighlightEffect>();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {

        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            outline.enabled = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            outline.enabled = false;
        }
    }
}