using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IRestartableObject
{
    public Canvas GUIObject;
    public float playerSpeed = 3.0f;
    public float maxVelocityVerticalValue = 5;
    public float addForce = 600;
    private GUIController GUIController;
    private Rigidbody2D m_rigidbody2D;
    private PlayerAnimation playerAnimation;
    private Vector3 originalPosition;

    void Start()
    {
        GameplayManager.Instance.isPlaying += DoPlay;
        GameplayManager.Instance.isPause += DoPause;

        m_rigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        originalPosition = transform.position;
        GUIController = GUIObject.GetComponent<GUIController>();
    }

    private void Update()
    {
        PlayerRotation();

        if (InputController.Instance.IsPressed)
        {
            AddUpVectorForce();
            FlyAnimation();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
        VerticalVelocityControl();
    }

    private void AddUpVectorForce()
    {
        m_rigidbody2D.AddForce(Vector2.up * addForce);
    }

    private void PlayerMove()
    {
        m_rigidbody2D.velocity = new Vector2(playerSpeed, m_rigidbody2D.velocity.y);
    }

    private void VerticalVelocityControl()
    {
        if (m_rigidbody2D.velocity.y > maxVelocityVerticalValue)
            m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, maxVelocityVerticalValue);
    }

    private void PlayerRotation()
    {
        transform.rotation = Quaternion.Euler(0,0,m_rigidbody2D.velocity.y * 6);
    }

    private void FlyAnimation()
    {
        playerAnimation.PlayFlyAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Column") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            m_rigidbody2D.simulated = false;
            GameplayManager.Instance.PauseManager();
            GameplayManager.Instance.SetEndGaming(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ScoreArea"))
        {
            GameplayManager.Instance.AddScore(1);
        }
            
    }

    private void DoPause()
    {
        m_rigidbody2D.simulated = false;
    }

    private void DoPlay()
    {
        m_rigidbody2D.simulated = true;
    }

    public void DoRestart()
    {
        m_rigidbody2D.velocity = Vector3.zero;
        m_rigidbody2D.angularVelocity = 0f;
        m_rigidbody2D.simulated = true;
        transform.position = originalPosition;
    }
}
