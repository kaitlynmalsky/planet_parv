using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 1f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 1f;
        Mimic myMimic;

        public Transform player; //player for the mimic to follow around
        public float mimicSpeedMultiplier = 0.3f; //how much slower the mimic is in comparison to the player
        public MonoBehaviour astronautMovementScript; //reference to astronaut mover script
        public bool following; //true when mimic is following the player

        public GameObject SpiderFirstEncounterCanvas;
        public Text SpiderFirstEncounterText;

        private void Start()
        {
            myMimic = GetComponent<Mimic>();
        }

        void Update()
        {
            //only make mimic active if player sees the mimic (it is on screen)
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && !following)
            {
                Debug.Log("Saw Spider AHJH");
                following = true;
                SpiderFirstEncounterCanvas.SetActive(true);
            }

            if (following && player != null)
            {   
                //find velocity vector that moves towards player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                velocity = Vector3.Lerp(velocity, directionToPlayer * speed * mimicSpeedMultiplier, velocityLerpCoef * Time.deltaTime);
                myMimic.velocity = velocity;

                //move according to the velocity
                transform.position = transform.position + velocity * Time.deltaTime;

                //original code to adjust height from the prefab
                RaycastHit hit;
                Vector3 destHeight = transform.position;
                if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                    destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);

                //lerp to smooth transition
                transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);

                //if mimic reaches player, destroy it and deal damage (TODO)
                //i set to 0.7 because the distance is never < 0.6 because of the height of the mimic
                if (Vector3.Distance(transform.position, player.position) <= 0.7f)
                {
                    Debug.Log("DIE");
                    //cute lil attack animation (just bounce up and down lol)
                    StartCoroutine(Attack());
                }
            }
        }

        //coroutine for attacking
        private IEnumerator Attack()
        {
            Vector3 originalPosition = transform.position;
            float bounceHeight = 1f;
            float bounceDuration = 0.5f;

            //freeze the player so the attack may happen
            if (astronautMovementScript != null) {
                astronautMovementScript.enabled = false;
            }

            //bouncy
            for (float t = 0; t < bounceDuration; t += Time.deltaTime) {
                float lerpValue = Mathf.PingPong(t * 10f, bounceHeight); 
                transform.position = new Vector3(originalPosition.x, originalPosition.y + lerpValue, originalPosition.z);
                yield return null;
            }

            //destroy the mimic after attack
            Destroy(gameObject);

            //unfreeze the player after the attack
            if (astronautMovementScript != null) {
                astronautMovementScript.enabled = true;
            }
        }
    }
}