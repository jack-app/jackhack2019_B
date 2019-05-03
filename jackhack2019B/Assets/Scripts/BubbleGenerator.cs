using UnityEngine;

public class BubbleGenerator : MonoBehaviour {
  [SerializeField] Bubble bubblePrefab;

  void Update () {
    if (Input.GetButtonDown("Fire1"))
    {
      Instantiate(bubblePrefab, transform.position, Quaternion.identity);
    }
  }
}