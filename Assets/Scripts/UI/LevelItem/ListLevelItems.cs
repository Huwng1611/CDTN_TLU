using UnityEngine;

public class ListLevelItems : MonoBehaviour
{
    public LevelItem[] levelItems;

    private void Start()
    {
        //this.levelItems = new LevelItem[transform.childCount];
        //for (int i = 0; i < this.levelItems.Length; i++)
        //{
        //    this.levelItems[i] = this.transform.GetChild(i).GetComponent<LevelItem>();
        //}
    }
}
