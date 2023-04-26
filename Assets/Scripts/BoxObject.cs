using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoxObject : MonoBehaviour
{
    [HideInInspector] public List<GameObject> BoxPoints = new List<GameObject>();
    List<RectTransform> Handles = new List<RectTransform>(), Anchors = new List<RectTransform>();
    public GameObject LineObject, Anchor, HandlePoint;
    [HideInInspector] public BoxManager boxManager;
    LineRenderer lr;
    void Start() {
        for (int i = 0; i <= 3; i++) {
            GameObject tmp;
            if (i % 2 == 0) {
                tmp = Instantiate(HandlePoint, transform);
                Handles.Add(tmp.GetComponent<RectTransform>());
            } else {
                tmp = Instantiate(Anchor, transform);
                Anchors.Add(tmp.GetComponent<RectTransform>());
            }
            BoxPoints.Add(tmp);
        }
        Handles[0].position = new Vector2(Screen.width / 2 - 50, Screen.height / 2 + 50);
        Handles[1].position = new Vector2(Screen.width / 2 + 50, Screen.height / 2 - 50);

        Anchors[0].position = new Vector2(Screen.width / 2 + 50, Screen.height / 2 + 50);
        Anchors[1].position = new Vector2(Screen.width / 2 - 50, Screen.height / 2 - 50);

        var lo = Instantiate(LineObject);
        lr = lo.GetComponent<LineRenderer>();
        lr.positionCount = BoxPoints.Count();

        List<Vector2> pointsAsVector2 = BoxPoints.Select(x => (Vector2)Camera.main.ScreenToWorldPoint((Vector2)x.transform.position)).ToList();

        lr.positionCount = pointsAsVector2.Count();
        lr.SetPositions(pointsAsVector2.Select(x => new Vector3(x.x, x.y, Camera.main.nearClipPlane)).ToArray());

        Handles[0].GetComponent<BoxPoint>().boxObject = this;
        Handles[1].GetComponent<BoxPoint>().boxObject = this;
    }

    public List<Vector2> WorldSpacePoints() {
        var flipVecotr = new Vector2(1, -1);
        return BoxPoints.Select(x => (Vector2)Camera.main.ScreenToWorldPoint(Vector3.Scale(flipVecotr, x.transform.position))).ToList();
    }

    public void UpdatePoints() {
        Anchors[0].position = new Vector3(Handles[1].position.x, Handles[0].position.y, Anchors[0].position.z);
        Anchors[1].position = new Vector3(Handles[0].position.x, Handles[1].position.y, Anchors[1].position.z);

        List<Vector2> pointsAsVector2 = BoxPoints.Select(x => (Vector2)Camera.main.ScreenToWorldPoint(x.transform.position)).ToList();

        lr.positionCount = pointsAsVector2.Count();
        lr.SetPositions(pointsAsVector2.Select(x => new Vector3(x.x, x.y, Camera.main.nearClipPlane)).ToArray());
    }
    public void DeleteBox() {
        Destroy(lr.gameObject);
        boxManager.RemoveBox(this);
    }
    private void OnDestroy() {
        if (lr != null) {
            Destroy(lr.gameObject);
        }
    }
}
