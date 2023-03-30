using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public class Annotatinator : MonoBehaviour
{
    //public GameObject point, line, segmentPrefab;
    //private GameObject newLine, segment;

    //private int lineIndex = 0, order = 0;

    //private Vector3 pos, currMousePos;

    //private bool drawingSegement = false;

    //private void Start() {
    //    line.GetComponent<LineRenderer>().startWidth = 0.05f;

    //}

    //private void Update() {

        
    //    if (Input.GetMouseButtonDown(0)) {
    //        if (!drawingSegement) {
    //            segment = Instantiate(segmentPrefab);
    //            segment.name = "Segment";
    //            newLine = Instantiate(line, segment.transform);
    //            lineIndex = 0;
    //            drawingSegement = true;
    //            segment.GetComponent<SpriteShapeRenderer>().enabled = false;
    //            segment.GetComponent<SegmentContainer>().displayText.enabled = false;
    //        }
    //        pos = CurrentMousePos();
    //        pos.z = 0;
    //        MakePoint(pos);
    //        segment.GetComponent<SegmentContainer>().points.Add(new Vector3(pos.x, pos.y, 0));
    //        newLine.GetComponent<LineRenderer>().positionCount++;
    //        newLine.GetComponent<LineRenderer>().SetPosition(lineIndex, pos);

    //        lineIndex++;
    //    }
    //    if (drawingSegement) {
    //        currMousePos = CurrentMousePos();
    //        currMousePos.z = 0;
    //        newLine.GetComponent<LineRenderer>().SetPosition(lineIndex, currMousePos);
    //        newLine.GetComponent<LineRenderer>().sortingOrder = order+2;
    //    }
    //    if (Input.GetMouseButtonDown(1) && drawingSegement) {
    //        newLine.GetComponent<LineRenderer>().positionCount--;
    //        newLine.GetComponent<LineRenderer>().loop = true;

    //        drawingSegement = false;
            
    //        Color tmpColor = Color.HSVToRGB(Random.Range(0,1f), 1, 1);
    //        tmpColor.a = .9f;
    //        newLine.GetComponent<LineRenderer>().SetColors(tmpColor, tmpColor);
    //        newLine.GetComponent<LineRenderer>().sortingOrder = order;
    //        foreach (var item in segment.GetComponentsInChildren<SpriteRenderer>()) {
    //            item.color = tmpColor;
    //        }
    //        tmpColor.a = .8f;
    //        DrawShape();
    //        segment.GetComponent<SpriteShapeRenderer>().color = tmpColor;
    //        segment.GetComponent<SegmentContainer>().displayText.enabled = true;
    //        segment.GetComponent<SpriteShapeRenderer>().sortingOrder = order;
    //        order++;
    //        //foreach ( Transform child in segment.transform) {
    //        //    child.gameObject.SetActive(false);
    //        //}
    //    }
    }

    //void MakePoint(Vector3 pos) {
    //    Instantiate(point, segment.transform).transform.position = pos;
    //}

    //Vector3 CurrentMousePos() {
    //    return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //}

    //void DrawShape() {
    //    int index = 0;
    //    segment.GetComponent<SpriteShapeController>().spline.Clear();
    //    foreach (var point in segment.GetComponent<SegmentContainer>().points) {
    //        segment.GetComponent<SpriteShapeController>().spline.InsertPointAt(index, point);
    //        index++;
    //    }
    //    segment.GetComponent<SpriteShapeRenderer>().enabled = true;
    //}
//}
