using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  [SerializeField]
  Color catchColor = Color.cyan;

  Renderer render;

  void Awake()
  {
    render = GetComponent<Renderer>();
  }

  public void HandleColor() => render.material.color = catchColor;
}
