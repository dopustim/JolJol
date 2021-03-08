using UnityEngine;

public enum CrossroadType
{
    NE, NS, NW, SE, SW, WE
}

public class Crossroad : MonoBehaviour
{
    public CrossroadType actualType;
    public Sprite[] crossroadSkins;

    void OnMouseDown()
    {
        actualType+= 1;
        actualType = (int)actualType > 5 ? 0 : actualType;
        SetSkin((int)actualType);
    }

    void SetSkin(int id)
    {
        GetComponent<SpriteRenderer>().sprite = crossroadSkins[id];
    }
}
