using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImpulseConsole : MonoBehaviour
{
    public GameObject[] buttons;//список кнопок
    public GameObject Sand1, telega1;
    public GameObject Sand2, telega2;
    public GameObject Model;
    public Transform[] TelegaPos;//место появления телег
    public float Force1, Force2;
    public Vector3[] frs;
    public TextMeshPro[] massInfo;

    public void ButtonPush(GameObject button)
    {
        for (int i = 0; i < buttons.Length; i++)
        {

            if (button == buttons[i])//если есть соответствие по кнопке
            {//для всех телег поднимаем или опускаем уровень песка и массу
                if (i == 0)
                {

                    ChangeMass(false, 1);

                }
                if (i == 1)
                {
                    if (telega1.GetComponent<Rigidbody>().mass > 1)
                    {
                        ChangeMass(false, -1);
                    }
                }
                if (i == 2)
                {

                    ChangeMass(true, 1);

                }
                if (i == 3)
                {
                    if (telega2.GetComponent<Rigidbody>().mass > 1)
                    {
                        ChangeMass(true, -1);
                    }
                }
                if (i == 4)
                {
                    telega1.GetComponent<Rigidbody>().AddForce(frs[0] * Force1, ForceMode.VelocityChange);
                    telega2.GetComponent<Rigidbody>().AddForce(frs[1] * Force2, ForceMode.VelocityChange);
                }//соударяем
                if (i == 5)
                {
                    Restart();
                }
                if (i == 6)
                {
                    ChangeImpulse1(1);
                }
                if (i == 7)
                {
                    ChangeImpulse2(1);
                }
                if (i == 8)
                {
                    ChangeImpulse1(-1);
                }
                if (i == 9)
                {
                    ChangeImpulse2(-1);
                }
                if (i == 10)
                {
                    if (Model.activeInHierarchy)
                    {
                        Model.SetActive(false);
                    }else
                        Model.SetActive(true);
                }
            }
        }
    }

    public void ChangeImpulse1(float Vol)
    {
        Force1 += Vol;
    }

    public void ChangeImpulse2(float Vol)
    {
        Force2 += Vol;
    }

    public void ChangeMass(bool whichOne, float vol)
    {
        if (!whichOne)
        {
            telega1.GetComponent<Rigidbody>().mass += 10 * vol;
            if (telega1.GetComponent<Rigidbody>().mass < 30)
                Sand1.transform.localPosition = new Vector3(Sand1.transform.localPosition.x, Sand1.transform.localPosition.y + vol, Sand1.transform.localPosition.z);
        }
        else
        {
            telega2.GetComponent<Rigidbody>().mass += 10 * vol;
            if (telega2.GetComponent<Rigidbody>().mass < 30)
                Sand2.transform.localPosition = new Vector3(Sand2.transform.localPosition.x, Sand2.transform.localPosition.y + vol, Sand2.transform.localPosition.z);
        }

    }

    public void Restart()
    {
        telega1.GetComponent<Rigidbody>().isKinematic = true;
        telega1.transform.position = TelegaPos[0].position;
        telega1.transform.rotation = TelegaPos[0].rotation;
        telega1.GetComponent<Rigidbody>().isKinematic = false;
        telega2.GetComponent<Rigidbody>().isKinematic = true;
        telega2.transform.position = TelegaPos[1].position;
        telega2.transform.rotation = TelegaPos[1].rotation;
        telega2.GetComponent<Rigidbody>().isKinematic = false;
    }

    void Update()
    {
        massInfo[0].text = "Масса телеги №1 = " + telega1.GetComponent<Rigidbody>().mass;
        massInfo[1].text = "Масса телеги №2 = " + telega2.GetComponent<Rigidbody>().mass;
        massInfo[2].text = "Нач. скорость телеги №1 = " + Force1;
        massInfo[3].text = "Нач. скорость телеги №2 = " + Force2;
    }
}
