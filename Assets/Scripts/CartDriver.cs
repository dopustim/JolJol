using UnityEngine;

public class CartDriver : MonoBehaviour
{
    Vector3 cameFrom;
    Vector3 moveTo;
    Vector3 parkingTo;
    bool parkingMode;
    bool cameToMarketStall;
    float speed;
    Crossroad currentCrossroad;

    void Start()
    {
        cameFrom = Vector3.down;
        moveTo = Vector3.up;
        parkingMode = false;
        cameToMarketStall = false;
        speed = 0.8F;
    }

    void FixedUpdate()
    {
        if (parkingMode) {
            UpdateDirection();
            transform.position = Vector3.MoveTowards(transform.position, parkingTo, Time.deltaTime * speed);
            if (transform.position == parkingTo) {
                parkingMode = false;
            }
        } else {
            if (!cameToMarketStall) {
                transform.position+= moveTo * Time.deltaTime * speed;
            }
        }
    }

    void UpdateDirection()
    {
        switch (currentCrossroad.actualType)
        {
            case CrossroadType.NE:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.up ? (Vector3.right, Vector3.left) :
                    cameFrom == Vector3.right ? (Vector3.up, Vector3.down) :
                    (moveTo, cameFrom);
                break;
            case CrossroadType.NS:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.up ? (Vector3.down, Vector3.up) :
                    cameFrom == Vector3.down ? (Vector3.up, Vector3.down) :
                    (moveTo, cameFrom);
                break;
            case CrossroadType.NW:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.up ? (Vector3.left, Vector3.right) :
                    cameFrom == Vector3.left ? (Vector3.up, Vector3.down) :
                    (moveTo, cameFrom);
                break;
            case CrossroadType.SE:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.down ? (Vector3.right, Vector3.left) :
                    cameFrom == Vector3.right ? (Vector3.down, Vector3.up) :
                    (moveTo, cameFrom);
                break;
            case CrossroadType.SW:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.down ? (Vector3.left, Vector3.right) :
                    cameFrom == Vector3.left ? (Vector3.down, Vector3.up) :
                    (moveTo, cameFrom);
                break;
            case CrossroadType.WE:
                (moveTo, cameFrom) =
                    cameFrom == Vector3.left ? (Vector3.right, Vector3.left) :
                    cameFrom == Vector3.right ? (Vector3.left, Vector3.right) :
                    (moveTo, cameFrom);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "MarketStall":

                Vector3 marketStallPosition = other.gameObject.transform.position;

                parkingMode = true;
                parkingTo = marketStallPosition;
                moveTo = new Vector3(0, 0, 0);

                cameToMarketStall = true;

                break;

            case "Crossroad":

                currentCrossroad = other.gameObject.GetComponent<Crossroad>();

                CrossroadType crossroadType = currentCrossroad.actualType;
                Vector3 crossroadPosition = other.gameObject.transform.position;

                parkingMode = true;
                parkingTo = crossroadPosition;
                moveTo = new Vector3(0, 0, 0);

                UpdateDirection();

                break;
        }
    }
}
