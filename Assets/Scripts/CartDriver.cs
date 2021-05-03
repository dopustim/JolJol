using UnityEngine;

public class CartDriver : MonoBehaviour
{
    Vector3 _moveFrom;
    Vector3 _moveTo;
    Vector3 _parkingTo;
    bool _parkingMode;
    bool _cameToMarketStall;
    float _speed;
    Crossroad _currentCrossroad;

    void Start()
    {
        _moveFrom = Vector3.down;
        _moveTo = Vector3.up;
        _parkingMode = false;
        _cameToMarketStall = false;
        _speed = 0.8F;
    }

    void FixedUpdate()
    {
        if (_parkingMode) {
            if (!_cameToMarketStall) {
                UpdateDirection();
            }
            transform.position = Vector3.MoveTowards(transform.position, _parkingTo, Time.deltaTime * _speed);
            if (transform.position == _parkingTo) {
                _parkingMode = false;
            }
        } else {
            if (!_cameToMarketStall) {
                transform.position+= _moveTo * Time.deltaTime * _speed;
            }
        }
    }

    void UpdateDirection()
    {
        switch (_currentCrossroad.GetActualType())
        {
            case CrossroadType.NE:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.up ? (Vector3.right, Vector3.left) :
                    _moveFrom == Vector3.right ? (Vector3.up, Vector3.down) :
                    (_moveTo, _moveFrom);
                break;
            case CrossroadType.NS:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.up ? (Vector3.down, Vector3.up) :
                    _moveFrom == Vector3.down ? (Vector3.up, Vector3.down) :
                    (_moveTo, _moveFrom);
                break;
            case CrossroadType.NW:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.up ? (Vector3.left, Vector3.right) :
                    _moveFrom == Vector3.left ? (Vector3.up, Vector3.down) :
                    (_moveTo, _moveFrom);
                break;
            case CrossroadType.SE:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.down ? (Vector3.right, Vector3.left) :
                    _moveFrom == Vector3.right ? (Vector3.down, Vector3.up) :
                    (_moveTo, _moveFrom);
                break;
            case CrossroadType.SW:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.down ? (Vector3.left, Vector3.right) :
                    _moveFrom == Vector3.left ? (Vector3.down, Vector3.up) :
                    (_moveTo, _moveFrom);
                break;
            case CrossroadType.WE:
                (_moveTo, _moveFrom) =
                    _moveFrom == Vector3.left ? (Vector3.right, Vector3.left) :
                    _moveFrom == Vector3.right ? (Vector3.left, Vector3.right) :
                    (_moveTo, _moveFrom);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "MarketStall":

                Vector3 marketStallPosition = other.gameObject.transform.position;

                _parkingMode = true;
                _parkingTo = marketStallPosition;
                _moveTo = new Vector3(0, 0, 0);

                _cameToMarketStall = true;

                break;

            case "Crossroad":

                _currentCrossroad = other.gameObject.GetComponent<Crossroad>();

                CrossroadType crossroadType = _currentCrossroad.GetActualType();
                Vector3 crossroadPosition = other.gameObject.transform.position;

                _parkingMode = true;
                _parkingTo = crossroadPosition;
                _moveTo = new Vector3(0, 0, 0);

                UpdateDirection();

                break;
        }
    }
}
