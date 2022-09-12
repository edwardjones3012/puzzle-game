using edw.Events;
using edw.Grids.Items;
using edw.Grids.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionWatcher : MonoBehaviour
{
    List<SolutionElement> activeSolution;
    
    public void SetActiveSolution(List<SolutionElement> activeSolution)
    {
        this.activeSolution = activeSolution;
    }

    private void OnEnable()
    {
        GameEvents.Instance.PillarLayoutChanged.AddDelegate(OnPillarLayoutChanged);
    }

    private void OnDisable()
    {
        GameEvents.Instance.PillarLayoutChanged.RemoveDelegate(OnPillarLayoutChanged);
    }

    private void OnPillarLayoutChanged(List<Pillar> pillars)
    {
        StartCoroutine(ValidateForSolution(pillars));
    }

    private IEnumerator ValidateForSolution(List<Pillar> pillars)
    {
        yield return new WaitForEndOfFrame();
        if (ValidatePillarConfiguration(pillars))
        {
            GameEvents.Instance.CorrectConfigurationMade.Invoke();
        }
    }

    private bool ValidatePillarConfiguration(List<Pillar> pillars)
    {
        foreach (SolutionElement se in activeSolution)
        {
            bool valid = false;
            foreach (Pillar pillar in pillars)
            {
                if (valid) continue;
                if (se.RequireType)
                {
                    if (pillar.PillarType != se.PillarType) continue;
                }
                if (se.GridPosition == pillar.Position)
                {
                    valid = true;
                    continue;
                }
            }
            if (!valid)
            {
                return false;
            }
        }

        return true;
    }
}
