using edw.Grids.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace edw.Grids.Visuals
{
    public class GridElementOccupierVisualiser : MonoBehaviour
    {
        [SerializeField]
        GridLogic gridLogic;

        [SerializeField]
        PillarTypeObject[] referencePillarObjects;

        private Dictionary<Pillar, GameObject> pillarObjectInstances = new Dictionary<Pillar, GameObject>();

        [SerializeField]
        GameObject referencePlayerObject;
        GameObject playerInstance;
        bool playerSpawned;

        private void Start()
        {
            gridLogic.OnPillarMoved += MovePillar;
            gridLogic.OnPillarInitialised += VisualisePillar;

            gridLogic.OnPlayerMoved += MovePlayerObject;
            gridLogic.OnPlayerInitialised += VisualisePlayer;
        }

        #region Pillars
        public void VisualisePillar(Pillar pillar, Vector3 worldPos)
        {
            Vector3 heightAdjusted = new Vector3(worldPos.x, worldPos.y + 2.25f, worldPos.z);
            if (pillarObjectInstances.ContainsKey(pillar))
            {
                Debug.LogError("Cannot register pillar instance more than once!");
                return;
            }
            GameObject obj = Instantiate(GetPillarToInstantiate(pillar.PillarType), heightAdjusted, Quaternion.identity);
            pillarObjectInstances.Add(pillar, obj);
        }

        public void MovePillar(Pillar pillar, Vector3 gridPos, Vector3 worldPos)
        {
            Vector3 heightAdjusted = new Vector3(worldPos.x, worldPos.y + 2.25f, worldPos.z);

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
        public void VisualisePlayer(Vector3 gridPos, Vector3 worldPos)
        {
            if (playerSpawned)
            {
                Debug.LogError("Cannot register pillar instance more than once!");
                return;
            }
            playerSpawned = true;
            Vector3 heightAdjusted = new Vector3(worldPos.x, worldPos.y + 2.25f, worldPos.z);
            playerInstance = Instantiate(referencePlayerObject, heightAdjusted, Quaternion.identity);
        }

        public void MovePlayerObject(Vector3 gridPos, Vector3 worldPos)
        {
            Vector3 heightAdjusted = new Vector3(worldPos.x, worldPos.y + 2.25f, worldPos.z);
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
