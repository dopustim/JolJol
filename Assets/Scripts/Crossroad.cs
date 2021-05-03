using UnityEngine;

public enum CrossroadType
{
    NE, NS, NW, SE, SW, WE
}

public class Crossroad : MonoBehaviour
{
    [SerializeField]
    Sprite[] _crossroadSkins;

    [SerializeField]
    CrossroadType _actualType;

    void OnMouseDown()
    {
        _actualType+= 1;
        _actualType = (int)_actualType > 5 ? 0 : _actualType;
        SetSkin((int)_actualType);
    }

    void SetSkin(int id)
    {
        GetComponent<SpriteRenderer>().sprite = _crossroadSkins[id];
    }

    public CrossroadType GetActualType()
    {
        return _actualType;
    }
}
