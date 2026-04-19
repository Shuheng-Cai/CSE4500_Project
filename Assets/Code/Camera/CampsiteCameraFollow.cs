using UnityEngine;

public class CampsiteCameraFollow : MonoBehaviour {
    public Transform _Follow;
    public float smoothSpeed = 8f;
    public Collider2D cameraBounds;

    Camera cam;

    void Awake() {
        cam = GetComponent<Camera>();
        if (_Follow == null) {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) _Follow = p.transform;
        }
    }

    void LateUpdate() {
        if (_Follow == null || !PlayerManager.instance.playerAlive) return;

        Vector3 target = new Vector3(_Follow.position.x, _Follow.position.y, transform.position.z);
        Vector3 next = Vector3.Lerp(transform.position, target, smoothSpeed * Time.deltaTime);

        if (cameraBounds != null) {
            Bounds b = cameraBounds.bounds;
            float halfH = cam.orthographicSize;
            float halfW = halfH * cam.aspect;
            float minX = b.min.x + halfW, maxX = b.max.x - halfW;
            float minY = b.min.y + halfH, maxY = b.max.y - halfH;
            next.x = (minX > maxX) ? b.center.x : Mathf.Clamp(next.x, minX, maxX);
            next.y = (minY > maxY) ? b.center.y : Mathf.Clamp(next.y, minY, maxY);
        }

        transform.position = next;
    }
}
