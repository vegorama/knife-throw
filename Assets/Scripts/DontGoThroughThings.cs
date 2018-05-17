using UnityEngine;
using System.Collections;

public class DontGoThroughThings : MonoBehaviour
{
    public delegate void CollidedDelegate(Collider2D collider);
    public event CollidedDelegate Collided;

    public LayerMask layerMask; //make sure we aren't in this layer 
    public float skinWidth = 0.1f; //probably doesn't need to be changed 

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector2 previousPosition;
    private Rigidbody2D myRigidbody;



    //initialize values 
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        previousPosition = myRigidbody.transform.position;
        minimumExtent = Mathf.Min(BoundsOf(GetComponent<Collider2D>()).extents.x, BoundsOf(GetComponent<Collider2D>()).extents.y);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector2 movementThisStep = (Vector2)myRigidbody.transform.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);

            //check for obstructions we might have missed 
            RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(previousPosition, movementThisStep, movementMagnitude, layerMask.value);

            //Going backward because we want to look at the first collisions first. Because we want to destroy the once that are closer to previous position
            for (int i = hitsInfo.Length - 1; i >= 0; i--)
            {
                var hitInfo = hitsInfo[i];
                if (hitInfo && hitInfo.rigidbody != GetComponent<Rigidbody2D>())
                {
                    if (Collided != null)
                    {
                        Collided(hitInfo.collider);
                    }
                }
            }
        }

        previousPosition = myRigidbody.transform.position;
    }

    // compute bounds in local space
    public static Bounds BoundsOf(Collider2D collider)
    {
        var bounds = new Bounds();

        var bc = collider as BoxCollider2D;
        if (bc)
        {
            var ext = bc.size * 0.5f;
            bounds.Encapsulate(new Vector3(-ext.x, -ext.y, 0f));
            bounds.Encapsulate(new Vector3(ext.x, ext.y, 0f));
            return bounds;
        }

        var cc = collider as CircleCollider2D;
        if (cc)
        {
            var r = cc.radius;
            bounds.Encapsulate(new Vector3(-r, -r, 0f));
            bounds.Encapsulate(new Vector3(r, r, 0f));
            return bounds;
        }


        // others :P
        //Debug.LogWarning("Unknown type "+bounds);

        return bounds;
    }

    // return bounds in world space
    public static Bounds BoundsColliders(GameObject obj)
    {
        var bounds = new Bounds(obj.transform.position, Vector3.zero);

        var colliders = obj.GetComponentsInChildren<Collider2D>();
        foreach (var c in colliders)
        {
            var blocal = BoundsOf(c);
            var t = c.transform;
            var max = t.TransformPoint(blocal.max);
            bounds.Encapsulate(max);
            var min = t.TransformPoint(blocal.min);
            bounds.Encapsulate(min);
        }

        return bounds;
    }


}