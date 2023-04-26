using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool roll = false;
    public int rolledNumber = 0;
    public List<GameObject> diceCanvases;

    private Rigidbody rb;
    private bool hasBeenHitted = false;
    private float force = 4f;
    private (float, float) torqueForce = (1f, 3f);

    private Dictionary<int, int> diceMap = new Dictionary<int, int> {
        { 1, 6 },
        { 2, 5 },
        { 3, 4 },
        { 4, 3 },
        { 5, 2 },
        { 6, 1 }
    };

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity == Vector3.zero && hasBeenHitted && rb.isKinematic == false)
        {
            ActivateDiceCanvas(rolledNumber);
            hasBeenHitted = false;
        }

        if (roll)
        {   
            diceCanvases.ForEach(go => go.SetActive(false));

            rb.isKinematic = false;
            rb.AddForce(force * Vector3.up, ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(0, 10),
                                    Random.Range(0, 10),
                                    Random.Range(0, 10)),
                                    ForceMode.Impulse);
            roll = false;
            hasBeenHitted = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            rb.isKinematic = false;
            rb.AddForce(force * collision.relativeVelocity, ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(torqueForce.Item1, torqueForce.Item2),
                                    Random.Range(torqueForce.Item1, torqueForce.Item2),
                                    Random.Range(torqueForce.Item1, torqueForce.Item2)),
                                    ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Tags"))
        {
            Debug.Log(other.gameObject.name);
        }
    }
    public void SetRolledNumber(int number)
    {
        rolledNumber = diceMap[number];
    }
    private void ActivateDiceCanvas(int rolledNumber)
    {
        Debug.Log($"Activating canvas nr : {rolledNumber - 1}");
        diceCanvases.ElementAt(rolledNumber - 1).SetActive(true);
    }
}
