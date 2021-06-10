using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer; 
    private Vector3 target; 
    private float startTime; 
    private float journeyLength;
    public float speed = 0.01F;
    private Vector3 size; 
    private Color newColor;
    private Material material;

    void Start()
    {
        //sets the starting position and target to (0, 0, 0)
        target = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);

        //sets the scale
        transform.localScale = Vector3.one * 1.3f;

        //sets the color and the opacity of the cube
        material = Renderer.material;
        material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);

        //sets start time to current time
        startTime = Time.time;
    }

    public float fraction(float length){
        //calculates distance travelled
        float distCovered = (Time.time - startTime) * speed;

        //calculates the proportion of the distance done
        float fractionOfJourney = distCovered / length;

        //I added this (might be useless) but it makes sure that the proportion is at most 1
        if(fractionOfJourney > 1){
            fractionOfJourney = 1.0f;
        }
        return fractionOfJourney;
    }

    //this method gradually changes the cube's position, scale, color and rotation 
    IEnumerator modifier(){
        //this line checks to see if the cube is 'close enough' to the target -- it never makes it exactly to the target position
        if(Vector3.Distance(transform.position, target) <= 1f){
            //this line will set a position target
            target = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

            //these lines will set a new scale target
            int random = Random.Range(1, 20);
            size = new Vector3(random, random, random);

            //this line will set a new color/opacity
            newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            //this line will note the start time
            startTime = Time.time;

            //this line calculates the distance between the current position and the target
            journeyLength = Vector3.Distance(transform.position, target);
        }
        //calculates what proportion of the journey the cube should have completed by now
        float frac = fraction(journeyLength);

        //sets position, scale, color and opacity according to target and how far the cube is from reaching it
        transform.position = Vector3.Lerp(transform.position, target, frac); //position
        transform.localScale = Vector3.Lerp(transform.localScale, size, frac); //scale
        material.color = Color.Lerp(material.color, newColor, frac); //color and opacity

        //returns IEnumerator -- have to do this for a Coroutine
        yield return new WaitForSeconds(2f);
    }

    void Update(){
        StartCoroutine(modifier());
    }
}