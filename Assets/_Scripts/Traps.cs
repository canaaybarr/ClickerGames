using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] public List<Phase> Phases;
    public float startDelay;
    private int phaseCount = 0;

    // Use this for initialization
    void Start ()
    {
        transform.DOScale(transform.localScale, startDelay).OnComplete(() =>
        {
            if (Phases.Count != 0)
            {
                PhaseController();
            }
        });
    }


    public void PhaseController()
    {

        if (phaseCount == Phases.Count)
        {
            phaseCount = 0;
        }
        Phase currentPhase = Phases[phaseCount];
        RunPhase(currentPhase);

        phaseCount++;
    }

    private void RunPhase(Phase phase)
    {
        if (phase.mode == mode.move)
        {
            transform.DOMove(transform.position+ phase.vector, phase.time).SetEase(phase.easeType).OnComplete(() =>
            {
                transform.DOMoveX(transform.position.x, phase.delay).OnComplete(PhaseController);
            });
        }
        else if (phase.mode == mode.rotate)
        {
            transform.DORotate(phase.vector, phase.time,RotateMode.WorldAxisAdd).SetEase(phase.easeType).OnComplete(() =>
            {
                transform.DOMoveX(transform.position.x, phase.delay).OnComplete(PhaseController);
            });
        }
    }
}


[System.Serializable]
public class Phase
{
    public mode mode;
    public Vector3 vector;
    public float time;
    public Ease easeType;
    public float delay;
}

public enum mode
{
    rotate,
    move,
    explode
}