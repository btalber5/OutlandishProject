using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion


    public delegate void OnEquipmentChanged(Equipment newItem);
    public OnEquipmentChanged onEquipmentChanged;

    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;

    SkinnedMeshRenderer[] currentMeshes;

    private void Start()
    {
        int numOfSLots = System.Enum.GetNames(typeof(EquipSlot)).Length;

        currentEquipment = new Equipment[numOfSLots];
        currentMeshes = new SkinnedMeshRenderer[numOfSLots];
    }

    public void Equip(Equipment newItem) {

        
        int slotIndex = (int)newItem.equipSlot;

        currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
        if (onEquipmentChanged != null) {

            onEquipmentChanged.Invoke(newItem);
        
        }
        

    }

    void SetEquipmentBlendShapes(Equipment item, int weight) {

        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions) {

            targetMesh.SetBlendShapeWeight((int)blendShape,weight);
        
        }
    
    }



}
