using System.IO;
using UnityEngine;

namespace Game.Utility
{
    public class IconCreator : MonoBehaviour
    {
        [SerializeField] Camera iconCamera;
        [Space]
        [SerializeField] string iconName = "Icon_";
        [SerializeField] string savePath = "Assets/Visual/UI/Icons/";
        [SerializeField] bool check = false;

        [Header("Render texture")]
        [SerializeField] Vector2 resolution = new Vector2(512, 512);

        void SaveIcon()
        {
            string path = $"{savePath}{iconName}.png";
            SaveIcon(resolution, iconCamera, path, check);
        }

        public bool SaveIcon(Vector2 resolution, Camera camera, string path, bool checkExist)
        {

            RenderTexture renderTexture = new RenderTexture((int)resolution.x, (int)resolution.y, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
            RenderTexture oldRenderTexture = camera.targetTexture;
            camera.targetTexture = renderTexture;
            camera.Render();
            camera.targetTexture = oldRenderTexture;

            RenderTexture.active = renderTexture;
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes = tex.EncodeToPNG();

            if (checkExist && File.Exists(path))
            {
                Debug.LogError($"Image exists.");
                return false;
            }

            File.WriteAllBytes(path, bytes);

#if (UNITY_EDITOR)
            UnityEditor.AssetDatabase.ImportAsset(path);
#endif

            Debug.Log($"Image saved to - {path}");
            return true;
        }
    }
}