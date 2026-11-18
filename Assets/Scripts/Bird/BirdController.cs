using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private GameObject LeaderBoardPanel;
    [SerializeField] private float flapVelocity = 10f;
    private bool flapRequested;
    private Rigidbody2D rigidBody2D;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.simulated = false;
    }
    private void Update()
    {
        if (PlayerNamePanel.activeSelf || LeaderBoardPanel.activeSelf)
        {
            return;
        }
        bool mousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool spacePressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        bool touchPressed = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;

        if (mousePressed || spacePressed || touchPressed)
        {
           if(GameManager.Instance==null)
            {
                return;
            }
           if(!GameManager.Instance.isGameStarted)
            {
                GameManager.Instance.StartGame();
                rigidBody2D.simulated = true;
            }
           if(!GameManager.Instance.isGameOver)
            {
                flapRequested = true;
                AudioManager.Instance.PlayFlap();
            }
        }
    }
    private void FixedUpdate()
    {
        if(GameManager.Instance!=null && GameManager.Instance.isGameOver)
        {
            return;
        }
        if(!flapRequested)
        {
            return;
        }
        rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocity.x, flapVelocity);
        flapRequested = false;
    }
}
