using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TeleportAction : MonoBehaviour
{
    public SteamVR_Input_Sources handType;//выбранная рука
    public SteamVR_Behaviour_Pose controllerPose;//используемый контроллер
    public SteamVR_Action_Boolean grabAction;//кнопка телепортации

    public GameObject Laser;//глобальный объект лазера
    GameObject LaserTMP;//временный объект лазера на сцене  

    public GameObject Player;//игрок
    RaycastHit hit; //точка телепортации
    void Update()
    {
        
        if (grabAction.GetLastStateDown(handType))//если нажат захват
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit))//если луч косается предмета
            {
                if (hit.transform.tag == "Floor") {//если предметом оказался пол, рисовать луч и перемещать игрока
                    CreateLaser();
                    LaserTMP.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    LaserTMP.GetComponent<LineRenderer>().SetPosition(1, hit.point);               
                }
            }
        }

        if (grabAction.GetLastStateUp(handType))//если захват отпущен
        {
            if (hit.transform.tag == "Floor")
            {
                Player.transform.position = hit.point;//перемещение игрокак
            }
            Destroy(LaserTMP);//удалить лазер
        }
    }
    
    void CreateLaser()//создание лазера
    {
        if (LaserTMP == null)
        {
            LaserTMP = Instantiate(Laser);
        }
    }
}
