using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpManager : MonoBehaviour
{
    [Header("血條")]
    public Image bar;
    [Header("傷害數值")]
    public RectTransform rectDamage;
    
    /// <summary>
    /// 輸入血量與最大值並更新血條
    /// </summary>
    /// <param name="hp">當前血量</param>
    /// <param name="hpMax">血量最大值</param>
    public void UpdateHpBar(float hp, float hpMax)
    {
        bar.fillAmount = hp / hpMax;
    }

    public IEnumerator ShowDamage(float damage)
    {
       RectTransform rect = Instantiate(rectDamage, transform);
        rect.anchoredPosition = new Vector2(0, 10);
        rect.GetComponent<Text>().text = damage.ToString();
        float y = rect.anchoredPosition.y;

        while(y < 80)
        {
            y += 20;
            rect.anchoredPosition = new Vector2(0, y);
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(rect.gameObject, 0.5f);
    }
}
