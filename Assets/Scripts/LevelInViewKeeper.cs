using UnityEngine;
using Zenject;

public class LevelInViewKeeper : MonoBehaviour
{
    private LevelBounds level;
    [SerializeField]
    private float CameraHeight;

    [Inject]
    public void Constuct(LevelBounds level)
    {
        this.level = level;
    }

    void Update()
    {
        KeepInView();
    }

    private void KeepInView()
    {
        var xDist = level.Max.x - level.Min.x;
        var zDist = level.Max.z - level.Min.z;
        var camera = Camera.main;
        float desiredAngle; 

        if (camera.aspect <= (xDist / zDist))
        {
            var horizontalAngle = Mathf.Atan((xDist / 2.0f) / CameraHeight) * Mathf.Rad2Deg * 2.0f;
            desiredAngle = Camera.HorizontalToVerticalFieldOfView(horizontalAngle, camera.aspect);
        }
        else
        {
            desiredAngle = Mathf.Atan((zDist / 2.0f) / CameraHeight) * Mathf.Rad2Deg * 2.0f;
        }

        camera.fieldOfView = desiredAngle;

        var xPos = level.Min.x + (xDist / 2.0f);
        var zPos = level.Min.z + (zDist / 2.0f);
        camera.transform.position = new Vector3(xPos, CameraHeight, zPos);
    }
}
