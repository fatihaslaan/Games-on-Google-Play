using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    float first = 100; //To move
    float second = 0; //between blendshapes
    float speed = 1f; //Volume of that move

    Mesh mesh;
    SkinnedMeshRenderer meshrenderer;
    int shapecount;
    int tmp = 0;
    
    void Start()
    {        
        meshrenderer = GetComponent<SkinnedMeshRenderer>();
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        shapecount = mesh.blendShapeCount;
        InvokeRepeating("WaveRepeat", 0.0f, 0.01f); //Set speed at InvokeRepeating with repeatRate
    }

    void WaveRepeat()
    {
        meshrenderer.SetBlendShapeWeight(tmp % shapecount, first); //Move between blendshapes
        meshrenderer.SetBlendShapeWeight((tmp + 1) % shapecount, second);
         if (first <= 0 && second >= 100) //Change values of blendshapes
        {
            tmp++;
            first = 100;
            second = 0;
        }
        first -= speed;
        second += speed;
    }
}