using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InflateButton : MonoBehaviour
{
    [SerializeField]
    private BoxEnlargement box;
    
    [SerializeField]
    private LayerMask interactLayer;

    [SerializeField]
    private Collider2D hitbox;

    void FixedUpdate()
    {
        box.isGrowing = hitbox.IsTouchingLayers(interactLayer);
    }
}
