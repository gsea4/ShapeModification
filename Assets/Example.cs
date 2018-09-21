using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example : MonoBehaviour{

    public Camera cam;
    public List<Vector3> listOfPoints = new List<Vector3>();
    public List<int> vertexPositions = new List<int>();
    public Vector3[] vertices1;
    public Vector3[] vertices2;
    private Vector3 normalVector;
    private Vector3[] vert;
    private Mesh meshOfCollider;
    private bool isSelected = false;

    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject[] listOfV;

    void Start(){
        //cam = GetComponent<Camera>();
        //listOfV = GetComponent<Modify>().listOfV;
    }

    void Update() {

        if(!isSelected) {
            RaycastHit hit;
            if(!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            MeshCollider meshCollider = hit.collider as MeshCollider;
            if(meshCollider == null || meshCollider.sharedMesh == null)
                return;


            var mesh = meshCollider.sharedMesh;
            vertices1 = mesh.vertices;
            vertices2 = GetComponent<MeshFilter>().mesh.vertices;
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;

            var hitIndex = hit.triangleIndex * 3 + 0;
            var hitNormal = hit.normal;
            normalVector = hitNormal;
            vert = vertices;
            meshOfCollider = mesh;

            var backwardAvailable = true;
            var forwardAvailable = true;

            //Debug.Log("Normal" + hitNormal);

            listOfPoints.Clear();
            vertexPositions.Clear();

            Vector3 p0 = vertices[triangles[hitIndex]];
            Vector3 p1 = vertices[triangles[hitIndex + 1]];
            Vector3 p2 = vertices[triangles[hitIndex + 2]];
            listOfPoints.Add(p0);
            listOfPoints.Add(p1);
            listOfPoints.Add(p2);
            vertexPositions.Add(triangles[hitIndex]);
            vertexPositions.Add(triangles[hitIndex + 1]);
            vertexPositions.Add(triangles[hitIndex + 2]);

            var offset1 = 3;
            var offset2 = 2;
            var offset3 = 1;
            while(backwardAvailable) {
                if((hitIndex - offset1) >= 0 && (hitIndex - offset2) >= 0 && (hitIndex - offset3) >= 0) {
                    var point1 = vertices[triangles[hitIndex - offset1]];
                    var point2 = vertices[triangles[hitIndex - offset2]];
                    var point3 = vertices[triangles[hitIndex - offset3]];
                    var side1 = point2 - point1;
                    var side2 = point3 - point1;
                    var tempNormal = Vector3.Cross(side1, side2) / (Vector3.Cross(side1, side2).magnitude);

                    if(tempNormal == hitNormal) {
                        listOfPoints.Add(point1);
                        listOfPoints.Add(point2);
                        listOfPoints.Add(point3);
                        vertexPositions.Add(triangles[hitIndex - offset1]);
                        vertexPositions.Add(triangles[hitIndex - offset2]);
                        vertexPositions.Add(triangles[hitIndex - offset3]);
                    } else {
                        backwardAvailable = false;
                    }
                } else {
                    backwardAvailable = false;
                }
                offset1 += 3;
                offset2 += 3;
                offset3 += 3;
            }

            offset1 = 3;
            offset2 = 4;
            offset3 = 5;
            while(forwardAvailable) {
                if((hitIndex + offset1) < triangles.Length && (hitIndex + offset2) < triangles.Length && (hitIndex + offset3) < triangles.Length) {
                    var point1 = vertices[triangles[hitIndex + offset1]];
                    var point2 = vertices[triangles[hitIndex + offset2]];
                    var point3 = vertices[triangles[hitIndex + offset3]];
                    var side1 = point2 - point1;
                    var side2 = point3 - point1;
                    var tempNormal = Vector3.Cross(side1, side2) / (Vector3.Cross(side1, side2).magnitude);

                    if(tempNormal == hitNormal) {
                        listOfPoints.Add(point1);
                        listOfPoints.Add(point2);
                        listOfPoints.Add(point3);
                        vertexPositions.Add(triangles[hitIndex + offset1]);
                        vertexPositions.Add(triangles[hitIndex + offset2]);
                        vertexPositions.Add(triangles[hitIndex + offset3]);
                    } else {
                        forwardAvailable = false;
                    }
                } else {
                    forwardAvailable = false;
                }
                offset1 += 3;
                offset2 += 3;
                offset3 += 3;
            }

            Transform hitTransform = hit.collider.transform;

            for(var i = 0; i < listOfPoints.Count; i++) {
                listOfPoints[i] = hitTransform.TransformPoint(listOfPoints[i]);
            }

            for(var i = 0; i < listOfPoints.Count; i += 3) {
                if(i < listOfPoints.Count && (i + 1) < listOfPoints.Count && (i + 2) < listOfPoints.Count) {
                    var pt1 = listOfPoints[i];
                    var pt2 = listOfPoints[i + 1];
                    var pt3 = listOfPoints[i + 2];
                    Debug.DrawLine(pt1, pt2, Color.red);
                    Debug.DrawLine(pt2, pt3, Color.red);
                    Debug.DrawLine(pt3, pt1, Color.red);
                }
            }
        }
    }

    public void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    public void OnMouseUp() {
        isSelected = false;
    }

    void OnMouseDrag() {
        listOfV = GameObject.FindGameObjectsWithTag("V");

        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        //Debug.Log(cursorPosition);
        var mesh = GetComponent<MeshFilter>().mesh;
        var anotherMesh = meshOfCollider;
        var newVertices = mesh.vertices;
        var anotherVertices = anotherMesh.vertices;

        var tempPositions = vertexPositions.Distinct().ToList();

        isSelected = true;
        // Debug.Log(normalVector);
        // Go through all of the vertex positions and move them in the direction of the normal vector
        for(var i = 0; i < tempPositions.Count(); i++) {
            var newVector = new Vector3(normalVector.x * cursorPosition.x, normalVector.y * cursorPosition.y, normalVector.z * cursorPosition.z);
            var anotherVector = cursorPosition - normalVector;
            var destinationVector = new Vector3(anotherVector.x * normalVector.x, anotherVector.y * normalVector.y, anotherVector.z * normalVector.z);

            // Trying to move the other vertices that share the same coordinates with the vertices on the face
            foreach(var vertex in listOfV) {
                if(vertex.transform.position == newVertices[tempPositions[i]]) {
                    Debug.Log("Before: " + vertex.transform.position);
                    var xDirection = vertex.transform.position.x + newVector.x * (-0.3f);
                    var yDirection = vertex.transform.position.y + newVector.y * (-0.3f);
                    var zDirection = vertex.transform.position.z + newVector.z * (-0.3f);
                    //vertex.transform.position += newVector * (-0.3f);
                    vertex.transform.position = new Vector3(xDirection, yDirection, zDirection);
                    Debug.Log("After: " + vertex.transform.position);
                    //newVertices[tempPositions[i]] += newVector * (-0.3f);
                    //newVertices[tempPositions[i]] += newVector * (-0.3f);
                    //vertex.transform.Translate(destinationVector);
                }
            }


            newVertices[tempPositions[i]] += newVector * (-0.3f);
            // listOfV[tempPositions[i]].transform.position += newVector * (-0.3f);

            // Debug.Log("Distance: " + Vector3.Distance(newVertices[tempPositions[i]], newVector));
            if(Vector3.Distance(newVertices[tempPositions[i]], destinationVector) > 0) {
                
            }

            
            //anotherVertices[tempPositions[i]] += normalVector;
        }
        mesh.vertices = newVertices;
        //anotherMesh.vertices = anotherVertices;
        ///mesh.RecalculateBounds();
        //anotherMesh.RecalculateBounds();
    }
}
