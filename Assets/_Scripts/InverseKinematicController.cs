using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class InverseKinematicController : MonoBehaviour
{
    [SerializeField] private InverseKinematicTarget _target;
    [SerializeField] private Transform _pole;
    [SerializeField] private int _iterationCount = 10;
    [SerializeField] private bool _debug = true;



    [SerializeField] private List<Transform> _bones;
    private List<float> _boneDistances;
    private Vector3[] _startDirections;
    private Quaternion[] _startRotations;

    private Transform _targetBone = null;
    private float _chainLenght = 0;

    #region Init
    private void Awake()
    {
        //GetAllBones(transform, ref _bones);
        GetBonesUntilTargetBone(_target.TargetBone, this.gameObject.transform, ref _bones);
        CalculateBoneDistances(_bones, ref _boneDistances);
        CalculateChainLenght(ref _chainLenght);
        FindTargetBoneInOurBones(_target.TargetBone);
        GetStartDirections();
        GetStartRotations();
    }
    private void GetAllBones(Transform parentTransform, ref List<Transform> transforms)
    {
        if (transforms == null)
            transforms = new List<Transform>();

        foreach (Transform child in parentTransform)
        {
            transforms.Add(child);

            GetAllBones(child, ref transforms);
        }
    }
    private void GetBonesUntilTargetBone(Transform targetChild, Transform parentTransform, ref List<Transform> transforms)
    {
        if (transforms == null)
            transforms = new List<Transform>();

        foreach (Transform child in parentTransform)
        {
            transforms.Add(child);

            if (child != targetChild)
                GetBonesUntilTargetBone(targetChild, child, ref transforms);
            else
                break;
        }
    }
    private void CalculateBoneDistances(List<Transform> bones, ref List<float> boneDistances)
    {
        if (boneDistances == null)
            boneDistances = new List<float>();

        foreach (var bone in bones)
            boneDistances.Add(Vector3.Distance(bone.position, bone.parent.position));
    }
    private void CalculateChainLenght(ref float chainLenght)
    {
        for (int i = 1; i < _boneDistances.Count; i++)
            chainLenght += _boneDistances[i];
    }
    private void FindTargetBoneInOurBones(Transform targetBone)
    {
        for (int i = 0; i < _bones.Count; i++)
        {
            if(_bones[i] == targetBone)
            {
                _targetBone = _bones[i];

                return;
            }
        }
        Debug.LogError("TargetBone isn't in _bones List");
    }
    private void GetStartDirections()
    {
        _startDirections = new Vector3[_bones.Count];

        for (int i = 0; i < _startDirections.Length; i++)
        {
            
            Vector3 direction = _bones[i].position - _bones[i].parent.position;
            direction.Normalize();

            _startDirections[i] = _bones[i].up;
        }
    }
    private void GetStartRotations()
    {
        _startRotations = new Quaternion[_bones.Count];

        for (int i = 0; i < _startDirections.Length; i++)
        {
            
            _startRotations[i] = _bones[i].rotation;
        }
    }
    #endregion

    private void LateUpdate()
    {
        RunIK();
    }

    private void RunIK()
    {
        float distanceFromTargetPointToRootBone = Vector3.Distance(_target.transform.position, _bones[0].position);
        
        //Position
        if (distanceFromTargetPointToRootBone >= _chainLenght) //if target's position far from targetBone
        {
            Vector3 direction = _target.transform.position - _bones[0].position;
            direction.Normalize();

            for (int i = 0; i < _bones.Count; i++)
                _bones[i].position = _bones[i].parent.position + (direction * _boneDistances[i]);
        }
        else
        {
            for (int k = 0; k < _iterationCount; k++)
            {
                _targetBone.position = _target.transform.position;

                //run revers loop for bones
                for (int i = _bones.Count - 1; i > 0; i--)
                {
                    Vector3 direction = _bones[i].parent.position - _bones[i].position;
                    direction.Normalize();

                    _bones[i].parent.position = _bones[i].position + (direction * _boneDistances[i]);
                }
                //run forward for fixing bugs
                for (int i = 0; i < _bones.Count; i++)
                {
                    Vector3 direction = _bones[i].position - _bones[i].parent.position;
                    direction.Normalize();

                    _bones[i].position = _bones[i].parent.position + (direction * _boneDistances[i]);
                }
            }

        }
        
        //Rotation
        for (int i = 0; i < _bones.Count - 1; i++)
        {
            if(_bones[i].childCount > 0)
            {
                Vector3 direction = _bones[i].GetChild(0).position - _bones[i].position;
                //Vector3 direction = _bones[i].position - _bones[i].parent.position;
                direction.Normalize();

                //_bones[i].up = direction;

                _bones[i].rotation = Quaternion.FromToRotation(_startDirections[i], direction) * _startRotations[i];//adding diff rotation to start rotation
            }
            
        }
        
    }

    private void OnDrawGizmos()
    {
        if(_debug)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < _bones.Count; i++)
            {
                Gizmos.DrawLine(_bones[i].position, _bones[i].parent.position);
                Gizmos.DrawSphere(_bones[i].position, 0.1f);
            }
        }
    }
}
