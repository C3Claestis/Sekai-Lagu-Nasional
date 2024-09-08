using UnityEngine;

public class LinePath : MonoBehaviour
{
    [SerializeField] Color colors = Color.black;
    [SerializeField] RectTransform startPoint;  // Titik awal garis
    [SerializeField] RectTransform endPoint;    // Titik akhir garis
    [SerializeField] RectTransform[] points;    // Array titik-titik di antara startPoint dan endPoint

    public RectTransform GetStartPoint => startPoint;
    public RectTransform GetEndPoint => endPoint;
    public RectTransform[] GetPoints => points;
    void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            // Set warna Gizmos
            Gizmos.color = colors;

            // Gambar garis dari startPoint ke titik pertama, atau langsung ke endPoint jika tidak ada titik tambahan
            Vector3 previousPoint = startPoint.position;
            
            if (points != null && points.Length > 0)
            {
                foreach (RectTransform point in points)
                {
                    Gizmos.DrawLine(previousPoint, point.position);
                    previousPoint = point.position;
                }
            }
            
            // Gambar garis dari titik terakhir ke endPoint
            Gizmos.DrawLine(previousPoint, endPoint.position);
        }
    }
}
