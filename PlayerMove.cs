using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 150;

    private Rigidbody2D body;

    public enum PlayerIndex
    {
        one,
        two
    }

    public PlayerIndex playerNumber;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

	void Update()
    {
        float pressLeft = 0;
        float pressRight = 0;

        switch (playerNumber)
        {
            case PlayerIndex.one:
                pressLeft  = Input.GetKey(KeyCode.A) ? 1 : 0;
                pressRight = Input.GetKey(KeyCode.D) ? 1 : 0;
                break;
            case PlayerIndex.two:
                pressLeft = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
                pressRight = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
                break;
        }

        body.velocity = new Vector3(pressRight - pressLeft, 0, 0) * speed * Time.deltaTime;
    }
}
