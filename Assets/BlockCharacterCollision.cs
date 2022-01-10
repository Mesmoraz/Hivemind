using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    [SerializeField] private Collider2D characterCollider = default(Collider2D);
    [SerializeField] private Collider2D characterBlockerCollider = default(Collider2D);
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }

}
