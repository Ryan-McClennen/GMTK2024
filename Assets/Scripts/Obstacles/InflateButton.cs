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

    [SerializeField]
    SpriteRenderer render;

    [SerializeField]
    Sprite[] buttons;

    void FixedUpdate()
    {
        if (hitbox.IsTouchingLayers(interactLayer))
        {
            box.isGrowing = true;
            render.sprite = buttons[1];
        }
        else
        {
            box.isGrowing = false;
            render.sprite = buttons[0];
        }
    }
}
