using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenThrowControl : MonoBehaviour {

    private Animator m_Anim;

    public GameObject shuriken;
    public Transform throwPoint;
    public Collider2D attackBox;

    public bool hasKnife;
    public bool callRecall;
    public bool attacking;
    public PhysicsMaterial2D stickyPhysicMat;
    public PhysicsMaterial2D slippyPhysicMat;

    public float throwSpeed;
    public float recallSpeed;
    public float rotateSpeed;


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

        if (Input.GetMouseButtonUp(0))
        {
            if (hasKnife)
            {
                attacking = true;
                StartCoroutine ( MeleeAttack(attackBox) );
            }
            else
            {

            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (hasKnife)
            {
                StopAllCoroutines();
                StartCoroutine ( ThrowShuriken() );
            }
            else
            {
                Vector3 offset = new Vector3(0f, 0.1f, 0f);
                gameObject.transform.position = shuriken.transform.position + offset;
                shuriken.SetActive(false);
                hasKnife = true;
            }
        }

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Shuriken")
        {
            shuriken.SetActive(false);
            hasKnife = true;
        }
    }
}


