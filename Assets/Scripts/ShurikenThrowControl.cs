using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenThrowControl : MonoBehaviour {

    private Animator m_Anim;

    [Header("References")]
    public GameObject shuriken;
    public Transform throwPoint;
    public Collider2D attackBox;
    public GameObject blueSparkles;
    public GameObject smokePoof;

    [Header("Physics References")]
    public PhysicsMaterial2D stickyPhysicMat;
    public PhysicsMaterial2D slippyPhysicMat;

    [Header("Knife Status")]
    public bool hasKnife;
    public bool callRecall;
    public bool attacking;
    public bool canThrow;
    public bool canRecall;

    [Header("Knife Variables")]
    public float throwSpeed;
    public float recallSpeed;
    public float rotateSpeed;
    public float teleportOffset;


    // Use this for initialization
    void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO is this hacky?
        m_Anim.SetBool("HasKnife", hasKnife);
        m_Anim.SetBool("Attacking", attacking);

        //Input button checks
        AttackCheck();
        ThrowKnifeCheck();
        RecallKnifeCheck();
    }

    private void FixedUpdate()
    {
        if (callRecall == true)
        {
            Vector2 shurikenPos2D = shuriken.transform.position;
            Vector2 homingDirection = (Vector2)gameObject.transform.position - shurikenPos2D;

            homingDirection.Normalize();

            float rotateAmount = Vector3.Cross(homingDirection, shuriken.transform.right).z;

            shuriken.GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

            shuriken.GetComponent<Rigidbody2D>().velocity = shuriken.transform.right * recallSpeed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Shuriken")
        {
            shuriken.SetActive(false);
            hasKnife = true;
        }
    }

    private void AttackCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (hasKnife)
            {
                attacking = true;
                StartCoroutine(MeleeAttack(attackBox));
            }
            else
            {
                attacking = true;
                StartCoroutine(SwingAndMiss());
            }
        }
    }

    private void ThrowKnifeCheck()
    {
        if (canThrow)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (hasKnife)
                {
                    StopAllCoroutines();
                    StartCoroutine(ThrowShuriken());
                }
                else
                {
                    Teleport();
                }
            }
        }

        else
        {

        }
    }

    private void RecallKnifeCheck()
    {
        if (canRecall)
        {
            if (Input.GetMouseButton(3))
            {
                callRecall = true;
                shuriken.GetComponent<Rigidbody2D>().sharedMaterial = slippyPhysicMat;
            }
            else
            {
                callRecall = false;
                shuriken.GetComponent<Rigidbody2D>().sharedMaterial = stickyPhysicMat;
            }
        }

        else
        {

        }

    }

    private void Teleport()
    {
        //Offset to prevent 'porting through walls
        Vector3 offset = new Vector3(0f, teleportOffset, 0f);

        //Create smoke poof -MINUS- offset
        smokePoof.transform.position = gameObject.transform.position - offset;
        smokePoof.SetActive(false);
        smokePoof.SetActive(true);

        //Teleport Player
        gameObject.transform.position = shuriken.transform.position + offset;
        shuriken.SetActive(false);

        //Create blue sparks
        blueSparkles.transform.position = shuriken.transform.position;
        blueSparkles.SetActive(false);
        blueSparkles.SetActive(true);

        hasKnife = true;
    }

    private IEnumerator ThrowShuriken()
    {
        //Ignore overlap
        Physics2D.IgnoreLayerCollision(9, 10, true);

        //Get positions
        Vector2 cursorInWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwPoint2D = throwPoint.position;    

        //Get direction to cursor, and rotation
        Vector2 direction = cursorInWorldPos - throwPoint2D;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        //Spawn Shuriken
        hasKnife = false;
        shuriken.SetActive(true);
        shuriken.transform.position = throwPoint.position;
        shuriken.transform.rotation = rotation;

        //Give it velocity
        shuriken.GetComponent<Rigidbody2D>().velocity = direction * throwSpeed;

        //Reset Overlap
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    private IEnumerator MeleeAttack(Collider2D col)
    {
        //Quaternion.Angle(col.transform.rotation, col.transform.rotation)
        Collider2D[] cols = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.extents, 0f, LayerMask.GetMask("Baddie"));
        foreach (Collider2D c in cols)
        {
            c.GetComponentInParent<BadGuy>().TakeDamage(10);
        }

        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

    private IEnumerator SwingAndMiss()
    {
        yield return new WaitForSeconds(0.2f);
        attacking = false;
    }


}


