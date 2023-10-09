using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillsController : MonoBehaviour
{

    SelectSkillsController selectSkills;
    private void OnEnable()
    {
        selectSkills = CreateButtonManager.Instance.selectSkills;
    }
    public void Active()
    {
        //PlayerController player = GetComponent<PlayerController>();
        //player.SelectSkills();

        selectSkills.gameObject.SetActive(true);

        Destroy(this);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, CreatePlayer.Instance.transform.position) <= 1f)
        {
            Active();
            Destroy(this.gameObject);
        }
    }
}
