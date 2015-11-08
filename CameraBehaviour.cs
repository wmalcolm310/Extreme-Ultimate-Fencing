using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public enum CameraState
    {
        ViewBoth,
        ViewP1,
        ViewP2,
        Stationary
    }

    public CameraState state = CameraState.ViewBoth;

    public Transform player1;
    public Transform player2;

    public float min = -5; //left most x value of the allowed view field
    public float max = 5; //right most x value of the allowed view field

    private float myMin;
    private float myMax;

    private SpriteRenderer sprRenderer1;
    private SpriteRenderer sprRenderer2;

    void Start()
    {
        myMin = min + ((Camera.main.orthographicSize * Screen.width) / Screen.height);
        myMax = max - ((Camera.main.orthographicSize * Screen.width) / Screen.height);

        sprRenderer1 = player1.GetComponent<SpriteRenderer>();
        sprRenderer2 = player2.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch(state)
        {
            case CameraState.ViewBoth:
                float middle = (player1.position.x + player2.position.x) / 2;
                transform.position = new Vector3(middle, transform.position.y, transform.position.z);
                ClampView();
                break;

            case CameraState.ViewP1:
                transform.position = new Vector3(player1.position.x, transform.position.y, transform.position.z);
                ClampView();
                break;

            case CameraState.ViewP2:
                transform.position = new Vector3(player2.position.x, transform.position.y, transform.position.z);
                ClampView();
                break;

            case CameraState.Stationary:
                //nothing
                ClampView();
                break;
        }
    }

    private void ClampView()
    {
        //if the camera won't be zooming and screensize won't be changing, its betters to find these values only once in the start function
        //myMin = min + ((Camera.main.orthographicSize * Screen.width) / Screen.height);
        //myMax = max - ((Camera.main.orthographicSize * Screen.width) / Screen.height);
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, myMin, myMax), transform.position.y, transform.position.z);

        if (state == CameraState.ViewP1)
        {
            float newMinimum = (player2.position.x) - (Camera.main.orthographicSize * Screen.width / Screen.height) + (sprRenderer2.bounds.size.x/2);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, newMinimum, myMax), transform.position.y, transform.position.z);
        }
        else if (state == CameraState.ViewP2)
        {
            float newMaximum = (player1.position.x) + (Camera.main.orthographicSize * Screen.width / Screen.height) - (sprRenderer1.bounds.size.x / 2);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, myMin, newMaximum), transform.position.y, transform.position.z);
        }
    }
}
