using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    private Rigidbody2D rb;
    private bool isDashing;
    public float dashDuration;
    public float dashCooldown;
    private float dashCooldownTimer;
    public int maxDashes;
    private int currentDashes;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0f)
        {

            if (currentDashes < maxDashes)
            {
                isDashing = true;
                currentDashes++;
                StartCoroutine(EndDash());
            }
            else
            {
                dashCooldownTimer = dashCooldown;
            }
        }
        if (isDashing)
        {
            rb.velocity = new Vector2(horizontalInput * dashSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.fixedDeltaTime;
        }

        //Sprite flipper
        if (horizontalInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontalInput > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }
}