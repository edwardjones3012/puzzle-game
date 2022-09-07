using DG.Tweening;
using edw.Events;
using edw.Grids.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace edw.Grids.Visuals
{
    public class GridElementOccupierVisualiser : MonoBehaviour
    {
        [SerializeField]
        PillarTypeObject[] referencePillarObjects;
        private Dictionary<Pillar, GameObject> pillarObjectInstances = new Dictionary<Pillar, GameObject>();
        private bool pillarsSpawned { get { return pillarObjectInstances.Count > 0; } }

        [SerializeField] 
        GameObject referencePlayerObject;
        GameObject playerInstance;
        bool playerSpawned { get { return playerInstance != null; } }

        #region Pillars
        public void VisualisePillar(Pillar pillar, Vector3 spawnPoint, bool animateIn = true, float delay = 0)
        {
            Vector3 heightAdjusted = new Vector3(spawnPoint.x, spawnPoint.y + 2.25f, spawnPoint.z);
            if (pillarObjectInstances.ContainsKey(pillar))
            {
                Debug.LogError("Cannot register pillar instance more than once!");
                return;
            }
            if (animateIn)
            {
                GameObject obj = Instantiate(GetPillarToInstantiate(pillar.PillarType), heightAdjusted + new Vector3(0, 25, 0), Quaternion.identity);
                pillarObjectInstances.Add(pillar, obj);
                StartCoroutine(AnimateObjectInFromAbove(obj, heightAdjusted, delay));
            }
            else
            {
                GameObject obj = Instantiate(GetPillarToInstantiate(pillar.PillarType), heightAdjusted, Quaternion.identity);
                pillarObjectInstances.Add(pillar, obj);
            }
        }

        public void MovePillar(Pillar pillar, Vector3 destination)
        {
            Vector3 destinationWithYOffset = new Vector3(destination.x, destination.y + 2.25f, destination.z);

            GameObject instance = GetPillarInstance(pillar);
            if (instance == null) return;
            instance.transform.DOMove(destinationWithYOffset, .25f);
        }

        private GameObject GetPillarInstance(Pillar pillar)
        {
            return pillarObjectInstances[pillar];
        }

        private GameObject GetPillarToInstantiate(PillarType type)
        {
            foreach(PillarTypeObject po in referencePillarObjects)
            {
                if (po.type == type) return po.GameObject;
            }
            return null;
        }

        public void ResetVisuals()
        {
            DestroyAllPillarInstances();
            DestroyPlayer();
        }

        private void DestroyAllPillarInstances()
        {
            foreach (KeyValuePair<Pillar, GameObject> pillar in pillarObjectInstances.ToArray())
            {
                Destroy(pillarObjectInstances[pillar.Key]);
                pillarObjectInstances.Remove(pillar.Key);
            }
        }

        private void DestroyPlayer()
        {
            if (playerSpawned) Destroy(playerInstance);
            playerInstance = null;
        }

        #endregion

        #region Player
        public void VisualisePlayer(Vector3 spawnPoint, bool animateIn = true, float animationTime = 1.25f, float delay = 0)
        {
            if (playerSpawned)
            {
                Debug.LogError("Cannot register player instance more than once!");
                return;
            }
            Vector3 heightAdjusted = new Vector3(spawnPoint.x, spawnPoint.y + 2.25f, spawnPoint.z);
            if (animateIn)
            {
                playerInstance = Instantiate(referencePlayerObject, heightAdjusted + Vector3.up * 25, Quaternion.identity);
                StartCoroutine(AnimateObjectInFromAbove(playerInstance, heightAdjusted, delay));
                // playerInstance.transform.DOMove(heightAdjusted, .5f);
                // playerInstance.transform.DORotate(new Vector3(0, 270, 0), .5f, RotateMode.LocalAxisAdd);
            }
            else
            {
                playerInstance = Instantiate(referencePlayerObject, heightAdjusted, Quaternion.identity);
            }
        }

        private IEnumerator AnimateObjectInFromAbove(GameObject obj, Vector3 destination, float animationTime, float delay = 0)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);
            obj.transform.DOMove(destination, animationTime);
            obj.transform.DORotate(new Vector3(0, 90, 0), animationTime, RotateMode.LocalAxisAdd);
        }

        public void MovePlayerObject(Vector3 destination)
        {
            Vector3 destinationWithYOffset = new Vector3(destination.x, destination.y + 2.25f, destination.z);
            playerInstance.transform.DOMove(destinationWithYOffset, .25f);
        }
        #endregion

        private void Start()
        {
            // GameEvents.Instance.NextLevelTriggerHit.
        }
    }
    
    [System.Serializable]
    public class PillarTypeObject
    {
        public PillarType type;
        public GameObject GameObject;
    }

}
