using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DeflateButton : MonoBehaviour
{
    [SerializeField]
    private BoxEnlargement box;
    
    [SerializeField]
    private LayerMask interactLayer;

    [SerializeField]
    private Collider2D hitbox;

    void Update()
    {
        box.isShrinking = hitbox.IsTouchingLayers(interactLayer);
    }
}
