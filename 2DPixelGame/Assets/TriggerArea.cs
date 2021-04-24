
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [Header("要關閉的障礙物")]
    public GameObject wall;
    public GameObject yee;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "推")
        {
            wall.SetActive(false);
            Destroy(yee);
        }
    }
}
