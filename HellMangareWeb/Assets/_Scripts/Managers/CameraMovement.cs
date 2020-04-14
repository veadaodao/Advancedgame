using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpend = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;
    private float scroll;
    public float scrollSpeed = 2f;
    private bool IsLeft = false;
    private bool IsRight = false;
    private bool IsUp = false;
    private bool IsDown = false;
    private bool IsBehind = false;
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        Screen.SetResolution(1280, 768, false);
    }
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && !IsUp)
        {
            pos.y += panSpend * Time.deltaTime;

        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness && !IsDown)
        {
            pos.y -= panSpend * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness && !IsRight)
        {
            pos.x += panSpend * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness && !IsLeft)
        {
            pos.x -= panSpend * Time.deltaTime;
        }
        if (!IsBehind)
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
        }
        else
        {
            pos = startPosition;
        }

        pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "InvisiableWallLeft")
        {
            IsLeft = true;
        }
        if (other.gameObject.name == "InvisiableWallRight")
        {
            IsRight = true;
        }
        if (other.gameObject.name == "InvisiableWallUp")
        {
            IsUp = true;
        }
        if (other.gameObject.name == "InvisiableWallDown")
        {
            IsDown = true;
        }
        if (other.gameObject.name == "InvisiableWallBehind")
        {
            IsBehind = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "InvisiableWallLeft")
        {
            IsLeft = false;
        }
        if (other.gameObject.name == "InvisiableWallRight")
        {
            IsRight = false;
        }
        if (other.gameObject.name == "InvisiableWallUp")
        {
            IsUp = false;
        }
        if (other.gameObject.name == "InvisiableWallDown")
        {
            IsDown = false;
        }
        if (other.gameObject.name == "InvisiableWallBehind")
        {
            IsBehind = false;
        }
    }
    
}
