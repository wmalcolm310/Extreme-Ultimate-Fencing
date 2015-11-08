using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 150;

    private Rigidbody2D body;

    private SpriteRenderer sprRenderer;

    public enum PlayerIndex
    {
        one,
        two
    }

    public PlayerIndex playerNumber;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprRenderer = GetComponent<SpriteRenderer>();
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

        float max_x = Camera.main.ViewportToWorldPoint(Vector3.right).x - sprRenderer.bounds.size.x / 2;
        float min_x = Camera.main.ViewportToWorldPoint(Vector3.zero).x + sprRenderer.bounds.size.x / 2;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, min_x, max_x);
        transform.position = clampedPosition;
    }
}
