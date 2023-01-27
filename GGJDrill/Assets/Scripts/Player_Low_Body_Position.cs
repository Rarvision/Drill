using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Low_Body_Position : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector2 boxColliderSizeChange;
    private Vector2 boxColliderOffsetChange;
    private Vector2 orignalSize;
    private Vector2 orignalOffset;

    [SerializeField] private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();

        orignalSize = boxCollider.size;
        orignalOffset = boxCollider.offset;

        boxColliderSizeChange = new Vector2(boxCollider.size.x, boxCollider.size.y / 2 - 0.2f);
        boxColliderOffsetChange = new Vector2(boxCollider.offset.x, boxCollider.offset.y - boxCollider.size.y / 4 - 0.2f/2);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.S))
        {
            boxCollider.size = boxColliderSizeChange;
            boxCollider.offset = boxColliderOffsetChange;

        }
        else if (!CanNotStand())
        {
            boxCollider.size = orignalSize;
            boxCollider.offset = orignalOffset;
        }
    }

    private bool CanNotStand()
    {
        Vector2 leftStart = new Vector2((transform.position.x - boxCollider.size.x/2), boxCollider.offset.y);

        Vector2 rightStart = new Vector2((transform.position.x + boxCollider.size.x / 2), boxCollider.offset.y);


        RaycastHit2D leftHitResult =  Physics2D.Raycast(leftStart, Vector2.up, 10f, layerMask);

        RaycastHit2D rightHitResult = Physics2D.Raycast(rightStart, Vector2.up, 10f, layerMask);

       /* Debug.DrawRay(leftStart, Vector2.up,Color.yellow);
        Debug.DrawRay(rightStart, Vector2.up,Color.yellow);*/

        bool result = leftHitResult || rightHitResult;

        return result;
    }

}


