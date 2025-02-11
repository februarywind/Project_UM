using System;
using System.Collections;
using UnityEngine;

public class U_BattleSkill : BattleSkillBase
{
    [SerializeField] float dashDagame;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    private PlayerController playerContoller;
    private CharacterController characterController;

    private void Awake()
    {
        playerContoller = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
    }

    public override void BattleSkillActivate()
    {
        StartCoroutine(DashSkill());
        Debug.Log(1212);
    }

    IEnumerator DashSkill()
    {
        float d = 0;
        playerContoller.IsControl = false;

        while (d < dashTime)
        {
            characterController.Move((transform.forward * dashSpeed) * Time.deltaTime);
            d += 1 / dashTime * Time.deltaTime;
            yield return null;
        }

        playerContoller.IsControl = true;
    }
}
