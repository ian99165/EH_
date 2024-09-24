using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public string spriteName = "MySprite"; // 這裡輸入你的 Sprite 名稱，不含副檔名
    public string spriteFolder = "picture"; // 這裡輸入存放 Sprite 的子資料夾名稱

    void Start()
    {
        // 在 Resources 中指定的子資料夾中尋找 Sprite
        Sprite newSprite = Resources.Load<Sprite>(spriteFolder + "/" + spriteName);

        if (newSprite != null)
        {
            // 獲取當前 GameObject 的 Image 組件
            Image imageComponent = gameObject.GetComponent<Image>();

            if (imageComponent != null)
            {
                // 將新的 Sprite 設定為 Image 組件的 Sprite
                imageComponent.sprite = newSprite;
            }
            else
            {
                Debug.LogError("Image 組件未找到！");
            }
        }
        else
        {
            Debug.LogError("找不到名為 " + spriteName + " 的 Sprite！");
        }
    }
}
