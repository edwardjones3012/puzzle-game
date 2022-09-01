using edw.Grids.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Visuals
{
    public class GridElementOccupierVisualiser : MonoBehaviour
    {
        [SerializeField]
        PillarTypeObject[] referencePillarObjects;

        private Dictionary<Pillar, GameObject> pillarObjectInstances = new Dictionary<Pillar, GameObject>();

        [SerializeField] 
        GameObject referencePlayerObject;
        GameObject playerInstance;
        bool playerSpawned;

        #region Pillars
        public void VisualisePillar(Pillar pillar, Vector3 spawnPoint)
        {
            Vector3 heightAdjusted = new Vector3(spawnPoint.x, spawnPoint.y + 2.25f, spawnPoint.z);
            if (pillarObjectInstances.ContainsKey(pillar))
            {
                Debug.LogError("Cannot register pillar instance more than once!");
                return;
            }
            GameObject obj = Instantiate(GetPillarToInstantiate(pillar.PillarType), heightAdjusted, Quaternion.identity);
            pillarObjectInstances.Add(pillar, obj);
        }

        public void MovePillar(Pillar pillar, Vector3 destination)
        {
            Vector3 heightAdjusted = new Vector3(destination.x, destination.y + 2.25f, destination.z);

            GameObject instance = GetPillarInstance(pillar);
            if (instance == null) return;
            instance.transform.position = heightAdjusted;
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

        #endregion

        #region Player
        public void VisualisePlayer(Vector3 spawnPoint)
        {
            if (playerSpawned)
            {
                Debug.LogError("Cannot register pillar instance more than once!");
                return;
            }
            playerSpawned = true;
            Vector3 heightAdjusted = new Vector3(spawnPoint.x, spawnPoint.y + 2.25f, spawnPoint.z);
            playerInstance = Instantiate(referencePlayerObject, heightAdjusted, Quaternion.identity);
        }

        public void MovePlayerObject(Vector3 destination)
        {
            Vector3 heightAdjusted = new Vector3(destination.x, destination.y + 2.25f, destination.z);
            playerInstance.transform.position = heightAdjusted;
        }
        #endregion
    }
    
    [System.Serializable]
    public class PillarTypeObject
    {
        public PillarType type;
        public GameObject GameObject;
    }

}
