using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Systems.Building;

namespace Game.Core.UI.Windows
{
    public class WindowShop : Window
    {
        [System.Serializable]
        public class TabContent
        {
            [field: SerializeField] public Button Button { get; private set; }
            [field: SerializeField] public StructureData[] Content { get; private set; }
        }

        [SerializeField] UIShopItem itemPrefab;
        [SerializeField] ScrollRect tabPrefab;
        [SerializeField] Transform tabRoot;
        [Space]
        [SerializeField] List<TabContent> tabsContent;

        [Header("Events")]
        [SerializeField] GameEvent eventStartBuild;

        int currentActive = 0;
        List<ScrollRect> tabs = new List<ScrollRect>();

        private void Awake()
        {
            UpdateShop();
        }

        private void OnEnable()
        {
            eventStartBuild.RegisterListener(OnStartBuild);
        }

        private void OnDisable()
        {
            eventStartBuild.UnregisterListener(OnStartBuild);
        }

        void OnStartBuild(Component sender, object data)
        {
            Hide();
        }

        public void UpdateShop()
        {
            for (var i = 0; i < tabsContent.Count; i++)
            {
                var tab = Instantiate(tabPrefab, tabRoot);
                tab.gameObject.SetActive(false);
                tabs.Add(tab);

                foreach (var item in tabsContent[i].Content)
                    Instantiate(itemPrefab, tab.content).Setup(item);

                int index = i;
                tabsContent[i].Button.onClick.AddListener(() => Enable(index));
            }

            tabs[currentActive].gameObject.SetActive(true);
            tabsContent = null;
        }

        public void Enable(int index)
        {
            if (currentActive == index)
                return;

            tabs[currentActive].gameObject.SetActive(false);
            currentActive = index;
            tabs[index].gameObject.SetActive(true);
        }
    }
}